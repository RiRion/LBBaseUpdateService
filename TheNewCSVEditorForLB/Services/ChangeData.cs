using System;
using System.Collections.Generic;
using System.Data;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Data.Repositories;
using TheNewCSVEditorForLB.Services.Interfaces;

namespace TheNewCSVEditorForLB.Services
{
    public class ChangeData : IChangeData
    {
        public IProductRepository Product { get; set; }
        public IVendorDictionaryRepository Vendors { get; set; }
        public IIeIdDictionaryRepository IeId { get; set; }
        public List<VendorDictionary> NewVendors { get; set; }
        public List<Product> NewProductId { get; set; }

        public ChangeData(IProductRepository productRepository, IVendorDictionaryRepository vendorDictionary, IIeIdDictionaryRepository ieIdDictionary)
        {
            Product = productRepository;
            Vendors = vendorDictionary;
            IeId = ieIdDictionary;
            NewVendors = new List<VendorDictionary>();
            NewProductId = new List<Product>();
        }
        public void ChangeFieldVendorIdAndVendorCountry()
        {
            try
            {
                foreach (var item in Product.AllProducts)
                {
                    item.VendorCountry = item.VendorId;
                    if (Vendors.VendorDictionary.Exists(x => x.ImportVendorId == item.VendorId))
                        item.VendorId = Vendors.VendorDictionary
                            .Find(x => x.ImportVendorId.Equals(item.VendorId)).CorrectVendorId;
                    else NewVendors.Add(new VendorDictionary {CorrectVendorId = 0, ImportVendorId = item.VendorId});
                }

                StatusCode.StatusOk("Change field VendorId and VendorCountry.");
            }
            catch (Exception exc)
            {
                StatusCode.StatusFalse("Not change VendorID and VendorCountry: " + exc.Message);
            }
            
        }

        public void ChangeFieldVibration()
        {
            try
            {
                foreach (var item in Product.AllProducts)
                {
                    if (item.Vibration == "1") item.Vibration = "Есть";
                    if (item.Vibration == "0") item.Vibration = "Нет";
                }

                StatusCode.StatusOk("Change Vibration field.");
            }
            catch (Exception exc)
            {
                StatusCode.StatusFalse("Not change Vibration: " + exc.Message);
            }
        }

        public void ChangeFieldNewAndBest()
        {
            try
            {
                foreach (var item in Product.AllProducts)
                {
                    if (item.New == "1" & item.Bestseller == "1") item.NewAndBestseller = "Новинка Хит продаж";
                    else if (item.New == "1") item.NewAndBestseller = "Новинка";
                    else if (item.Bestseller == "1") item.NewAndBestseller = "Хит продаж";
                }
                StatusCode.StatusOk("Change NewAndBest field.");
            }
            catch (Exception exc)
            {
                StatusCode.StatusFalse("Not change NewAndBest field: " + exc.Message);
            }
        }

        public void ChangeFieldIeId()
        {
            try
            {
                foreach (var item in Product.AllProducts)
                {
                    if (IeId.DictionaryID.Exists(x => x.ProductId == item.ProductId))
                    {
                        item.IeId = IeId.DictionaryID.Find(x => x.ProductId.Equals(item.ProductId)).IeId.ToString();
                    }
                    else NewProductId.Add(item);
                }
                StatusCode.StatusOk("Change IeId field.");
            }
            catch (Exception exc)
            {
                StatusCode.StatusFalse("Not change IeId field: " + exc.Message);
            }
        }
    }
}