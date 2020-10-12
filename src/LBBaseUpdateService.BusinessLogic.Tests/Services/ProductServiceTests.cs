using System.Linq;
using LBBaseUpdateService.BusinessLogic.Services.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Exceptions;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LBBaseUpdateService.BusinessLogic.Tests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        [TestMethod]
        public void ChangeFieldVendorIdAndVendorCountryTests()
        {
            // Arrange
            ProductService productService = new ProductService();
            var products = new Product[]
            {
                new Product(){VendorId = 1}, 
                new Product(){VendorId = 12}, 
                new Product(){VendorId = 12}
            };
            var vendors = new VendorId[]
            {
                new VendorId(){ExternalId = 1, InternalId = 24}, 
                new VendorId(){ExternalId = 12, InternalId = 25}
            };
            
            // Act
            productService.ChangeFieldVendorIdAndVendorCountry(products, vendors);
            
            // Assert
            Assert.AreEqual(products.Count(p=>p.VendorId == 24), 1);
            Assert.AreEqual(products.Count(p=>p.VendorId.Equals(25)), 2);
            Assert.AreEqual(products.Count(p=>p.VendorCountry.Equals(1)), 1);
            Assert.AreEqual(products.Count(p=>p.VendorCountry.Equals(12)), 2);
        }

        [TestMethod]
        public void VendorNotFoundExcChangeFieldVendorIdAndVendorCountryTests()
        {
            //Arrange
            var productService = new ProductService();
            var products = new[]
            {
                new Product() { VendorId = 1}
            };
            var vendors = new[]
            {
                new VendorId() {ExternalId = 2, InternalId = 3}
            };
            
            // Act & Assert
            Assert.ThrowsException<VendorIdNotFoundException>(() =>
                productService.ChangeFieldVendorIdAndVendorCountry(products, vendors));
        }

        [TestMethod]
        public void ChangeFieldVibrationTests()
        {
            // Arrange
            var productService = new ProductService();
            var products = new[]
            {
                new Product() {Vibration = "0"},
                new Product() {Vibration = "1"},
                new Product() {Vibration = ""},
                new Product() {Vibration = "0"}
            };
            
            // Act
            productService.ChangeFieldVibration(products);
            
            // Assert
            Assert.AreEqual(products.Count(p=>p.Vibration.Equals("Нет")), 2);
            Assert.AreEqual(products.Count(p=>p.Vibration.Equals("Есть")), 1);
        }
    }
}