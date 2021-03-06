using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using LBBaseUpdateService.BusinessLogic.Services.OfferService;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Exceptions;
using LBBaseUpdateService.BusinessLogic.Services.OfferService.Models;
using LBBaseUpdateService.BusinessLogic.Services.ProductService.Models;
using LBBaseUpdateService.BusinessLogic.Tests.TestData.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LBBaseUpdateService.BusinessLogic.Tests.Services
{
    [TestClass]
    public class OfferServiceTests
    {
        [TestMethod]
        public void DeleteOffersWithoutProductTests()
        {
            // Arrange
            var offerService = new OfferService();
            var offers = new List<Offer>();
            offers.Add(new Offer() {ProductIeId = 1});
            offers.Add(new Offer() {ProductIeId = 2});
            offers.Add(new Offer() {ProductIeId = 3});
            offers.Add(new Offer() {ProductIeId = 4});
                
            var ids = new List<Product>()
            {
                new Product() {ProductExId = 1, ProductIeId = 11},
                new Product() {ProductExId = 2, ProductIeId = 12}
            };
            
            // Act
            offerService.DeleteOffersWithoutProduct(offers, ids);
            
            // Assert
            Assert.AreEqual(offers.Count, 2);
        }

        [TestMethod]
        public void GetOfferSheetToAddTests()
        {
            // Arrange
            var offerService = new OfferService();
            var basePath = Directory.GetCurrentDirectory();
            var offerFromSupplier = GetOffersFromCsv(basePath + "/TestData/offersFromSupplier.csv").ToList();
            var offerFromSite = GetOffersFromCsv(basePath + "/TestData/offersFromSite.csv").ToList();
            // Act 
            var list = offerService.GetOfferListToAdd(offerFromSupplier, offerFromSite);
            // Assert
            Assert.AreEqual(list.Count(), 2);
        }

        [TestMethod]
        public void GetOffersSheetToUpdateTests()
        {
            // Arrange
            var offerService = new OfferService();
            var basePath = Directory.GetCurrentDirectory();
            var offerFromSupplier = GetOffersFromCsv(basePath + "/TestData/offersFromSupplier.csv").ToList();
            var offerFromSite = GetOffersFromCsv(basePath + "/TestData/offersFromSite.csv").ToList();
            // Act 
            var list = offerService.GetOfferListToUpdate(offerFromSupplier, offerFromSite);
            // Assert
            Assert.AreEqual(list.Count(), 10);
        }

        [TestMethod]
        public void GetOffersIdToDeleteTests()
        {
            // Arrange
            var offerService = new OfferService();
            var basePath = Directory.GetCurrentDirectory();
            var offerFromSupplier = GetOffersFromCsv(basePath + "/TestData/offersFromSupplier.csv").ToList();
            var offerFromSite = GetOffersFromCsv(basePath + "/TestData/offersFromSite.csv").ToList();
            // Act 
            var idToDelete = offerService.GetOffersIdToDelete(offerFromSupplier, offerFromSite);
            // Assert
            Assert.AreEqual(idToDelete.Length, 2);
        }

        private Offer[] GetOffersFromCsv(string path)
        {
            var configuration = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim,
                BadDataFound = null,
                Encoding = Encoding.UTF8
            };
            configuration.RegisterClassMap<OfferMap>();
            using (var stream = new StreamReader(path))
            using (var reader = new CsvReader(stream, configuration))
            {
                return reader.GetRecords<Offer>().ToArray();
            }
        }
    }
}