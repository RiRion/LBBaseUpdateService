using System.Collections.Generic;
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
        public void VendorNotFoundException_ChangeFieldVendorIdAndVendorCountryTests()
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

        [TestMethod]
        public void ChangeFieldNewAndBestTests()
        {
            // Arrange
            var productService = new ProductService();
            var products = new[]
            {
                new Product() {New = "1", Bestseller = "1"},
                new Product() {New = "1", Bestseller = "0"},
                new Product() {New = "0", Bestseller = "1"},
                new Product() {New = "0", Bestseller = "0"}
            };
            
            // Act
            productService.ChangeFieldNewAndBest(products);
            
            // Assert
            Assert.AreEqual(products.Count(p=>p.NewAndBestseller.Equals("Новинка Хит продаж")), 1);
            Assert.AreEqual(products.Count(p=>p.NewAndBestseller.Equals("Новинка")), 1);
            Assert.AreEqual(products.Count(p=>p.NewAndBestseller.Equals("Хит продаж")), 1);
            Assert.AreEqual(products.Count(p=>p.NewAndBestseller.Equals("")), 1);
        }

        [TestMethod]
        public void SetMainCategoryIdTests()
        {
            // Arrange
            var productService = new ProductService();
            var categories = new[]
            {
                new Category() {Id = 1, Name = "One", ParentId = 0},
                new Category() {Id = 2, Name = "Two", ParentId = 1},
                new Category() {Id = 3, Name = "Three", ParentId = 2},
                new Category() {Id = 11, Name = "OneOne", ParentId = 0},
                new Category() {Id = 12, Name = "OneTwo", ParentId = 11},
                new Category() {Id = 21, Name = "TwoOne", ParentId = 0}
            };
            var products = new[]
            {
                new Product() 
                    {
                        ProductId = 1,
                        Categories = new List<Category>()
                        {
                            new Category(){Name = "One"}, 
                            new Category(){Name = "Two"}, 
                            new Category(){Name = "Three"}
                        }
                    },
                new Product()
                    {
                        ProductId = 2,
                        Categories = new List<Category>()
                        {
                            new Category(){Name = "OneOne"},
                            new Category(){Name = "OneTwo"},
                            new Category(){Name = ""}
                        }
                    },
                new Product()
                    {
                        ProductId = 3,
                        Categories = new List<Category>()
                        {
                            new Category(){Name = "TwoOne"},
                            new Category(){Name = ""},
                            new Category(){Name = ""}
                        }
                    }
            };
            
            // Act
            productService.SetMainCategoryId(products, categories);
            
            // Assert
            Assert.AreEqual(products.ElementAt(0).CategoryId, 3);
            Assert.AreEqual(products.ElementAt(1).CategoryId, 12);
            Assert.AreEqual(products.ElementAt(2).CategoryId, 21);
        }

        [TestMethod]
        public void CategoryNotFoundException_SetMainCategoryIdTests()
        {
            // Arrange
            var productService = new ProductService();
            var products = new[]
            {
                new Product()
                {
                    ProductId = 1,
                    Categories = new List<Category>()
                    {
                        new Category() {Name = "One"},
                        new Category() {Name = "Two"},
                        new Category() {Name = "Three"}
                    }
                }
            };
            var categories = new[]
            {
                new Category() {Id = 1, Name = "One", ParentId = 0},
                new Category() {Id = 2, Name = "Two", ParentId = 1},
                new Category() {Id = 3, Name = "throw Exception", ParentId = 2}
            };
            
            // Assert
            Assert.ThrowsException<CategoryNotFoundException>(() =>
                productService.SetMainCategoryId(products, categories));
        }

        [TestMethod]
        public void ChangeFieldIeIdTests()
        {
            // Arrange
            var productService = new ProductService();
            var products = new[]
            {
                new Product(){ProductId = 1},
                new Product(){ProductId = 2},
                new Product(){ProductId = 3}
            };
            var internalWithExternalId = new[]
            {
                new ProductIdWithInternalId() {ProductId = 1, IeId = 201},
                new ProductIdWithInternalId() {ProductId = 2, IeId = 202}
            };
            
            // Act
            productService.ChangeFieldIeId(products, internalWithExternalId);
            
            // Assert
            Assert.AreEqual(products.Count(p=>p.IeId.Equals(201)), 1);
            Assert.AreEqual(products.Count(p=>p.IeId.Equals(202)), 1);
            Assert.AreEqual(products.Count(p=>p.IeId.Equals(0)), 1);
        }

        [TestMethod]
        public void GetProductSheetToUpdateTests()
        {
            // Arrange
            var productService = new ProductService();
            var externalProduct = new[]
            {
                new Product(){ProductId = 1, Name = "Rider", Material = "soft"},
                new Product(){ProductId = 2, Name = "Visual Studio", Material = "soft"},
                new Product(){ProductId = 3, Name = "VS Code", Material = "soft"}
            };
            var internalProduct = new[]
            {
                new Product(){ProductId = 1, Name = "Rider", Material = "metal"},
                new Product(){ProductId = 2, Name = "Visual studio", Material = "soft"},
                new Product(){ProductId = 3, Name = "VS Code", Material = "soft"}
            };
            
            // Act
            var updateSheet = productService.GetProductSheetToUpdate(externalProduct, internalProduct);
            
            // Assert
            Assert.AreEqual(updateSheet.Length, 2);
        }
    }
}