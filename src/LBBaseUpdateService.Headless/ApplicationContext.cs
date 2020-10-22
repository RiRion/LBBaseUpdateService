using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using BitrixService.Clients.Loveberi.Interfaces;
using BitrixService.Clients.Stripmag.Interfaces;
using BitrixService.Models.ApiModels;
using LBBaseUpdateService.BusinessLogic.Services.Models;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Comparators;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Models;

namespace LBBaseUpdateService.Headless
{
	internal sealed class ApplicationContext : IDisposable
	{
		private readonly ILifetimeScope _lifetimeScope;
		private readonly ILoveberiClient _loveberiClient;
		private readonly IStripmagClient _stripmagClient;
		private readonly IVendorService _vendorService;
		private readonly IOfferService _offerService;
		private readonly IProductService _productService;
		private readonly IMapper _mapper;


		public ApplicationContext(
			ILifetimeScope lifetimeScope,
			ILoveberiClient loveberiClient,
			IStripmagClient stripmagClient,
			IVendorService vendorService,
			IOfferService offerService,
			IProductService productService,
			IMapper mapper
			)
		{
			_lifetimeScope = lifetimeScope;
			_loveberiClient = loveberiClient;
			_stripmagClient = stripmagClient;
			_vendorService = vendorService;
			_offerService = offerService;
			_mapper = mapper;
			_productService = productService;
		}


		// FUNCTIONS ////////////////////////////////////////////////////////////////////////////////////
		public async Task RunAsync()
		{
			try
			{
				_loveberiClient.Login();
				await UpdateVendors();
				// TODO: Require update categories.
				await UpdateProducts();
				await UpdateOffers();
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: {e.Message}");
			}
		}

		private async Task UpdateVendors()
		{
			var vendorsFromSupplier = _mapper.Map<Vendor[]>(await _stripmagClient.GetVendorsFromSupplierAsync());
			var vendorsFromSite = _mapper.Map<Vendor[]>(await _loveberiClient.GetVendorsAsync());

			var addSheet = _vendorService.GetSheetToAddAsync(vendorsFromSupplier, vendorsFromSite);

			if (addSheet.Length > 0) await SendToSite(_mapper.Map<VendorAto[]>(addSheet), _loveberiClient.AddVendorsAsync);
		}
		
		private async Task UpdateProducts()
		{
			var productsFromSupplier = _mapper.Map<Product[]>(await _stripmagClient.GetProductsFromSupplierAsync());
			var productsFromSite = _mapper.Map<Product[]>(await _loveberiClient.GetAllProductsAsync());
			var prodIdWithIeId = _mapper.Map<ProductIdWithInternalId[]>(await _loveberiClient.GetProductIdWithIeIdAsync());
			var vendorsFromSite  = _mapper.Map<VendorId[]>(await _loveberiClient.GetVendorsInternalIdWithExternalIdAsync());
			var categoriesFromSite = _mapper.Map<Category[]>(await _loveberiClient.GetCategoriesAsync());

			_productService.ChangeFieldVibration(productsFromSupplier);
			_productService.ChangeFieldNewAndBest(productsFromSupplier);
			_productService.ChangeFieldIeId(productsFromSupplier, prodIdWithIeId);
			_productService.SetMainCategoryId(productsFromSupplier, categoriesFromSite);
			_productService.ChangeFieldVendorIdAndVendorCountry(productsFromSupplier, vendorsFromSite);

			// TODO: delete repeating products id. Need to add several categories.
			var withoutRepeatingProdId = productsFromSupplier.Distinct(new ProductIdComparer()).ToArray();

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
			
			_offerService.DeleteOffersWithoutProduct(offersFromSupplier, prodIdWithIeId);
			_offerService.ReplaceVendorProductIdWithInternalId(offersFromSupplier, prodIdWithIeId);
			
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
			var methodName = action.Method.Name;
			var i = 0;
			var step = 100;
			var watch = new Stopwatch();
			do
			{
				watch.Start();
				if (list.Length - i < step)
				{
					step = list.Length - i;
					i = list.Length;
				}
				else i += step;
				await action(list.Skip(i - step).Take(step).ToArray());
				watch.Stop();
				Console.WriteLine($"{methodName}: Completed {i}. Count {list.Length}. Iteration time: {watch.ElapsedMilliseconds/1000} s.");
				watch.Reset();
			} while (i < list.Length);
		}

		// IDisposable ////////////////////////////////////////////////////////////////////////////
		public void Dispose()
		{
			_lifetimeScope?.Dispose();
		}
	}
}