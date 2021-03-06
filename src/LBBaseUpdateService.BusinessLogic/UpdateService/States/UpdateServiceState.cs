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
        
        private Product[] _productsFromSupplier;
        private Product[] _productsFromSite;
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
        
        public override async Task UpdateAsync()
        {
            _loveberiClient.Login();
                
            await InitVendorAsync();
            await InitProductAsync();
            await InitOfferAsync();
                
            UpdateVendorList();
            UpdateProductList();
            UpdateOfferList();
            
            _context.TransitionTo(_context._lifetimeScope.Resolve<VendorUpdateState>());
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
            _categoriesFromSite = _mapper.Map<List<Category>>(await _loveberiClient.GetCategoriesAsync());
            
            // TODO: delete repeating products id. Need to add product with several categories.
            _context.Products.Data.AddRange(_productsFromSupplier.Distinct(new ProductIdComparer()));
        }

        private async Task InitOfferAsync()
        {
            _offersFromSupplier = _mapper.Map<List<Offer>>(await _stripmagClient.GetOffersFromSupplierAsync());
            _offersFromSite = _mapper.Map<Offer[]>(await _loveberiClient.GetAllOffersAsync());
            
            _context.Offers.Data.AddRange(_offersFromSupplier);
        }

        private void UpdateVendorList()
        {
            var addList = _vendorService.GetListToAddAsync(_vendorsFromSupplier, _vendorsFromSite);

            foreach (var vendor in addList) _context.Vendors.ListToAdd.Enqueue(vendor);
        }

        private void UpdateProductList()
        {
            _productService.ChangeFieldVibration(_context.Products.Data);
            _productService.ChangeFieldOffers(_context.Products.Data);
            _productService.SetMainCategoryId(_context.Products.Data, _categoriesFromSite);
            _productService.SetDiscount(_context.Products.Data, _context.Offers.Data);

            var addList = _context.Products.Data.Except(_productsFromSite, new ProductIdComparer()).ToArray();
            var updateList = _productService.GetProductListToUpdate(_context.Products.Data, _productsFromSite.ToList());
            var deleteList = _productsFromSite.Except(_context.Products.Data, new ProductIdComparer())
            .Select(p => p.ProductIeId).ToArray();

            foreach (var product in addList) _context.Products.ListToAdd.Enqueue(product);
            foreach (var product in updateList) _context.Products.ListToUpdate.Enqueue(product);
            foreach (var product in deleteList) _context.Products.ListToDelete.Enqueue(product);
        }

        private void UpdateOfferList()
        {
            _offerService.DeleteOffersWithoutProduct(_context.Offers.Data, _context.Products.Data);
            
            var addList = _offerService.GetOfferListToAdd(_context.Offers.Data, _offersFromSite.ToList());
            var updateList = _offerService.GetOfferListToUpdate(_context.Offers.Data, _offersFromSite.ToList());
            var deleteIdList = _offerService.GetOffersIdToDelete(_context.Offers.Data, _offersFromSite.ToList());
            
            foreach (var offer in addList) _context.Offers.ListToAdd.Enqueue(offer);
            foreach (var offer in updateList) _context.Offers.ListToUpdate.Enqueue(offer);
            foreach (var id in deleteIdList) _context.Offers.ListToDelete.Enqueue(id);
        }
    }
}