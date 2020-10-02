using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BitrixService.ApiClients.Loveberi.Interfaces;
using BitrixService.ApiClients.Stripmag.Interfaces;
using BitrixService.Contracts.ApiModels;
using TheNewCSVEditorForLB.BusinessLogic.Services.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models;
using TheNewCSVEditorForLB.BusinessLogic.Services.Models.Config;
using TheNewCSVEditorForLB.BusinessLogic.Services.OfferService.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.OfferService.Models;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Comparators;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Interfaces;
using TheNewCSVEditorForLB.BusinessLogic.Services.ProductService.Models;

namespace TheNewCSVEditorForLB.Headless
{
	internal sealed class ApplicationContext : IDisposable
	{
		private readonly ILifetimeScope _lifetimeScope;
		private readonly ApplicationConfig _config;
		private readonly IDataStorageService _dataStorageService;
		private readonly IEntityUpdater _entityUpdater;
		private readonly ILoveberiClient _loveberiClient;
		private readonly IStripmagClient _stripmagClient;
		private readonly IOfferService _offerService;
		private readonly IProductService _productService;
		private readonly IMapper _mapper;


		public ApplicationContext(
			ILifetimeScope lifetimeScope,
			ApplicationConfig config,
			IDataStorageService dataStorageService,
			IEntityUpdater entityUpdater,
			ILoveberiClient loveberiClient,
			IStripmagClient stripmagClient,
			IOfferService offerService,
			IProductService productService,
			IMapper mapper
			)
		{
			_lifetimeScope = lifetimeScope;
			_config = config;
			_dataStorageService = dataStorageService;
			_entityUpdater = entityUpdater;
			_loveberiClient = loveberiClient;
			_stripmagClient = stripmagClient;
			_offerService = offerService;
			_mapper = mapper;
			_productService = productService;
		}


		// FUNCTIONS ////////////////////////////////////////////////////////////////////////////////////
		public async Task RunAsync()
		{
			_loveberiClient.Login();
			
			await UpdateProducts();
			await UpdateOffers();
		}
		
		private async Task UpdateProducts()
		{
			var productsFromSupplier = _mapper.Map<Product[]>(await _stripmagClient.GetProductsFromSupplierAsync());
			var productsFromSite = _mapper.Map<Product[]>(await _loveberiClient.GetAllProductsAsync());
			var prodIdWithIeId = _mapper.Map<ProductIdWithInternalId[]>(await _loveberiClient.GetProductIdWithIeIdAsync());
			var vendorsFromSite  = _mapper.Map<VendorId[]>(await _loveberiClient.GetVendorsAsync());
			var categoriesFromSite = _mapper.Map<Category[]>(await _loveberiClient.GetCategoriesAsync());

			_productService.ChangeFieldVibration(productsFromSupplier);
			_productService.ChangeFieldNewAndBest(productsFromSupplier);
			_productService.ChangeFieldIeId(productsFromSupplier, prodIdWithIeId);
			_productService.SetCategoryId(productsFromSupplier, categoriesFromSite);
			_productService.ChangeFieldVendorIdAndVendorCountry(productsFromSupplier, vendorsFromSite);

			// TODO: delete repeating products id. Need to add several categories.
			var withoutRepeatingProdId = productsFromSupplier.Distinct(new ProductIdComparer()).ToArray();
			// TODO: Require update vendorId.
			var newVendorsId = productsFromSupplier.Where(p => p.VendorId == 0).ToArray();
			// TODO: Require update categories.
			var productWithNewCategory = productsFromSupplier.Where(p => p.Categories.CategoryId == 0).ToArray();

			var addSheet = withoutRepeatingProdId.Except(productsFromSite, new ProductIdComparer()).ToArray();
			var updateSheet = _productService.GetProductSheetToUpdate(withoutRepeatingProdId, productsFromSite);
			var deleteSheet = productsFromSite.Except(withoutRepeatingProdId, new ProductIdComparer())
				.Select(p => p.IeId).ToArray();
			
			var watch = new Stopwatch();
			watch.Start();
			if (deleteSheet.Length > 0) await SendToSite(deleteSheet, _loveberiClient.DeleteProductsAsync);
			if (addSheet.Length > 0) await SendToSite(_mapper.Map<ProductAto[]>(addSheet), _loveberiClient.AddProductsRangeAsync);
			if (updateSheet.Length > 0) await SendToSite(_mapper.Map<ProductAto[]>(updateSheet), _loveberiClient.UpdateProductsAsync);
			watch.Stop();
			Console.WriteLine($"Products update is completed. Time to completed {watch.ElapsedMilliseconds/1000} s.");
		}

		private async Task UpdateOffers()
		{
			var offersFromSupplier = _mapper.Map<List<Offer>>(await _stripmagClient.GetOffersFromSupplierAsync());
			var offersFromSite = _mapper.Map<Offer[]>(await _loveberiClient.GetAllOffersAsync());
			var prodIdWithIeId = 
				_mapper.Map<ProductIdWithInternalId[]>(await _loveberiClient.GetProductIdWithIeIdAsync());
			
			_offerService.ReplaceVendorProductIdWithInternalId(offersFromSupplier, prodIdWithIeId);
			_offerService.SetDefaultFieldWeight(offersFromSupplier);
			
			var addSheet = _offerService.GetOfferSheetToAdd(offersFromSupplier.ToArray(), offersFromSite);
			var updateSheet = _offerService.GetOffersSheetToUpdate(offersFromSupplier.ToArray(), offersFromSite);
			var deleteIdSheet = _offerService.GetOffersIdToDelete(offersFromSupplier.ToArray(), offersFromSite);

			var watch = new Stopwatch();
			watch.Start();
			if (deleteIdSheet.Length > 0) await SendToSite(deleteIdSheet, _loveberiClient.DeleteOffersAsync);
			if (addSheet.Length > 0)
				await SendToSite(_mapper.Map<OfferAto[]>(addSheet), _loveberiClient.AddOffersRangeAsync);
			if (updateSheet.Length > 0)
				await SendToSite(_mapper.Map<OfferAto[]>(updateSheet), _loveberiClient.UpdateOffersAsync);
			watch.Stop();
			Console.WriteLine($"Offers update is completed. Time to completed {watch.ElapsedMilliseconds/1000} s.");
		}
		
		private async Task SendToSite<T>(T[] list, Func<T[], Task> action)
        		{
        			var s = action.Method.Name;
        			var i = 0;
        			var step = 100;
        			do
        			{
        				var watch = new Stopwatch();
        				watch.Start();
        				if (list.Length - i < step)
        				{
        					step = list.Length - i;
        					i = list.Length;
        				}
        				else i += 100;
        				await action(list.Skip(i - step).Take(step).ToArray());
        				watch.Stop();
        				Console.WriteLine($"{s}: Completed {i}. Count {list.Length}. Iteration time: {watch.ElapsedMilliseconds/1000} s.");
        			} while (i < list.Length);
        		}

		// IDisposable ////////////////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			_lifetimeScope?.Dispose();
		}
	}
}