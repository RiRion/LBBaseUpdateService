using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using BitrixService.Clients.Loveberi.Interfaces;
using BitrixService.Clients.Stripmag.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Comparators;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Models;

namespace LBBaseUpdateService.BusinessLogic.UpdateService.States
{
    public class UpdateServiceState : StateBase
    {
        private readonly ILoveberiClient _loveberiClient;
        private readonly IStripmagClient _stripmagClient;
        private readonly IVendorService _vendorService;
        private readonly IOfferService _offerService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        private Vendor[] _vendorsFromSupplier;
        private Vendor[] _vendorsFromSite;
        private VendorId[] _vendorIdFromSite;
        
        private Product[] _productsFromSupplier;
        private Product[] _productsFromSite;
        private ProductIdWithInternalId[] _productIdWithInternalId;
        private List<Category> _categoriesFromSite;
        private List<Offer> _offersFromSupplier;
        private Offer[] _offersFromSite;

        public UpdateServiceState(
            ILoveberiClient loveberiClient,
            IStripmagClient stripmagClient,
            IVendorService vendorService,
            IOfferService offerService,
            IProductService productService,
            IMapper mapper
        )
        {
            _loveberiClient = loveberiClient;
            _stripmagClient = stripmagClient;
            _vendorService = vendorService;
            _offerService = offerService;
            _mapper = mapper;
            _productService = productService;
        }
        
        public override async void Update()
        {
            try
            {
                _loveberiClient.Login();
                
                //await InitVendorAsync();
                //await InitProductAsync();
                await InitOfferAsync();
                
                //UpdateVendorList();
                //UpdateProductList();
                UpdateOfferList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            _context.TransitionTo(_context._lifetimeScope.Resolve<VendorUpdateState>());
            _context._state.Update();
        }
        
        private async Task InitVendorAsync()
        {
            _vendorsFromSupplier = _mapper.Map<Vendor[]>(await _stripmagClient.GetVendorsFromSupplierAsync());
            _vendorsFromSite = _mapper.Map<Vendor[]>(await _loveberiClient.GetVendorsAsync());
        }

        private async Task InitProductAsync()
        {
            _productsFromSupplier = _mapper.Map<Product[]>(await _stripmagClient.GetProductsFromSupplierAsync());
            _productsFromSite = _mapper.Map<Product[]>(await _loveberiClient.GetAllProductsAsync());
            _productIdWithInternalId = _mapper.Map<ProductIdWithInternalId[]>(await _loveberiClient.GetProductIdWithIeIdAsync());
            _vendorIdFromSite  = _mapper.Map<VendorId[]>(await _loveberiClient.GetVendorsInternalIdWithExternalIdAsync());
            _categoriesFromSite = _mapper.Map<List<Category>>(await _loveberiClient.GetCategoriesAsync());
            
            // TODO: delete repeating products id. Need to add product with several categories.
            _context._products.Data.AddRange(_productsFromSupplier.Distinct(new ProductIdComparer()));
        }

        private async Task InitOfferAsync()
        {
            _offersFromSupplier = _mapper.Map<List<Offer>>(await _stripmagClient.GetOffersFromSupplierAsync());
            _offersFromSite = _mapper.Map<Offer[]>(await _loveberiClient.GetAllOffersAsync());
            
            _context._offers.Data.AddRange(_offersFromSupplier);
        }

        private void UpdateVendorList()
        {
            var addList = _vendorService.GetListToAddAsync(_vendorsFromSupplier, _vendorsFromSite);

            foreach (var vendor in addList) _context._vendors.ListToAdd.Enqueue(vendor);
        }

        private void UpdateProductList()
        {
            _productService.ChangeFieldVibration(_context._products.Data);
            _productService.ChangeFieldOffers(_context._products.Data); 
            _productService.ChangeFieldIeId(_context._products.Data, _productIdWithInternalId); 
            _productService.SetMainCategoryId(_context._products.Data, _categoriesFromSite); 
            _productService.ChangeFieldVendorIdAndVendorCountry(_context._products.Data, _vendorIdFromSite);
            _productService.SetDiscount(_context._products.Data, _context._offers.Data);
        }

        private void UpdateOfferList()
        {
            var addList = _offerService.GetOfferListToAdd(_offersFromSupplier.ToArray(), _offersFromSite);
            var updateList = _offerService.GetOfferListToUpdate(_offersFromSupplier.ToArray(), _offersFromSite);
            var deleteIdList = _offerService.GetOffersIdToDelete(_offersFromSupplier.ToArray(), _offersFromSite);
            
            foreach (var offer in addList) _context._offers.ListToAdd.Enqueue(offer);
            foreach (var offer in updateList) _context._offers.ListToUpdate.Enqueue(offer);
            foreach (var id in deleteIdList) _context._offers.ListToDelete.Enqueue(id);
        }
    }
}