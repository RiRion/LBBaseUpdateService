using System;
using System.Collections.Generic;
using TheNewCSVEditorForLB.Data.Models;
using TheNewCSVEditorForLB.Data.Interfaces;
using TheNewCSVEditorForLB.Services.Interfaces;

namespace TheNewCSVEditorForLB.Services
{
    public class ChangeData : IChangeData
    {
        public IProductRepository Product { get; set; }
        public IVendorsWithProductIdRepository Vendors { get; set; }
        public IProductIdWithInternalIdRepository IeId { get; set; }
        public List<VendorsWithProductId> NewVendors { get; set; }
        public List<Product> NewProductId { get; set; }

        public ChangeData(IProductRepository productRepository, IVendorsWithProductIdRepository vendorsWithProductId, IProductIdWithInternalIdRepository ieIdDictionary)
        {
            Product = productRepository;
            Vendors = vendorsWithProductId;
            IeId = ieIdDictionary;
            NewVendors = new List<VendorsWithProductId>();
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
                    else NewVendors.Add(new VendorsWithProductId {CorrectVendorId = 0, ImportVendorId = item.VendorId});
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
                    if (IeId.AllIntarnalId.Exists(x => x.ProductId == item.ProductId))
                    {
                        item.IeId = IeId.AllIntarnalId.Find(x => x.ProductId.Equals(item.ProductId)).IeId.ToString();
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