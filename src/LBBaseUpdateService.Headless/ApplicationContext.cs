using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using BitrixService.Clients.Loveberi.Interfaces;
using BitrixService.Clients.Stripmag.Interfaces;
using BitrixService.Models.ApiModels;
using LBBaseUpdateService.BusinessLogic;
using LBBaseUpdateService.BusinessLogic.Services.Models;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Comparators;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Models;
using Newtonsoft.Json;

namespace LBBaseUpdateService.Headless
{
	internal sealed class ApplicationContext
	{
		private readonly ILifetimeScope _lifetimeScope;
		private readonly ILoveberiClient _loveberiClient;
		private readonly IStripmagClient _stripmagClient;
		private readonly IVendorService _vendorService;
		private readonly IOfferService _offerService;
		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		private Product[] _productsFromSupplier;
		private Product[] _productsFromSite;
		private ProductIdWithInternalId[] _productIdWithInternalId;
		private VendorId[] _vendorsFromSite;
		private Category[] _categoriesFromSite;

		private List<Offer> _offersFromSupplier;
		private Offer[] _offersFromSite;


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
		private async Task InitProducts()
		{
			_productsFromSupplier = _mapper.Map<Product[]>(await _stripmagClient.GetProductsFromSupplierAsync());
			_productsFromSite = _mapper.Map<Product[]>(await _loveberiClient.GetAllProductsAsync());
			_productIdWithInternalId = _mapper.Map<ProductIdWithInternalId[]>(await _loveberiClient.GetProductIdWithIeIdAsync());
			_vendorsFromSite  = _mapper.Map<VendorId[]>(await _loveberiClient.GetVendorsInternalIdWithExternalIdAsync());
			_categoriesFromSite = _mapper.Map<Category[]>(await _loveberiClient.GetCategoriesAsync());
		}
		public async Task RunAsync()
		{
			try
			{
				await _lifetimeScope.Resolve<UpdateContext>().UpdateAsync();
				// TODO: Require update categories.
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: {e.Message}");
			}
		}

		private void UpdateProductList()
		{
			// _productService.ChangeFieldVibration(_productsFromSupplier);
			// _productService.ChangeFieldOffers(_productsFromSupplier); 
			// _productService.ChangeFieldIeId(_productsFromSupplier, _productIdWithInternalId); 
			// _productService.SetMainCategoryId(_productsFromSupplier, _categoriesFromSite); 
			// _productService.ChangeFieldVendorIdAndVendorCountry(_productsFromSupplier, _vendorsFromSite);
			// _productService.SetDiscount(_productsFromSupplier, _offersFromSupplier.ToArray());
		}

		private void UpdateOfferList()
		{
			//_offerService.DeleteOffersWithoutProduct(_offersFromSupplier, _productIdWithInternalId);
		}

		private async void UpdateOffersBeforeLoadProduct()
		{
			_offerService.DeleteOffersWithoutProduct(_offersFromSupplier, _productIdWithInternalId);
		}

		private async Task UpdateProducts()
		{
			// TODO: delete repeating products id. Need to add product with several categories.
			// var withoutRepeatingProdId = _productsFromSupplier.Distinct(new ProductIdComparer()).ToArray();
			//
			// var addList = withoutRepeatingProdId.Except(_productsFromSite, new ProductIdComparer()).ToArray();
			// var updateList = _productService.GetProductListToUpdate(withoutRepeatingProdId, _productsFromSite);
			// var deleteList = _productsFromSite.Except(withoutRepeatingProdId, new ProductIdComparer())
				// .Select(p => p.ProductIeId).ToArray();
			
			// var watch = new Stopwatch();
			// watch.Start();
			// if (deleteList.Length > 0) await _loveberiClient.DeleteProductsWithStepAsync(deleteList);
			// if (addList.Length > 0)
			// 	await _loveberiClient.AddProductsRangeWithStepAsync(_mapper.Map<ProductAto[]>(addList));
			// if (updateList.Length > 0) await _loveberiClient.UpdateProductsWithStepAsync(_mapper.Map<ProductAto[]>(updateList));
			// watch.Stop();
			// Console.WriteLine($"Products update is completed. Time to completed {watch.ElapsedMilliseconds/1000} s.");
		}
	}
}