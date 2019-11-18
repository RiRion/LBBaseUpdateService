using System.Collections.Generic;
using CsvHelper.Configuration;
using TheNewCSVEditorForLB.Data.Models;

namespace TheNewCSVEditorForLB.Services.Interfaces
{
    public interface IDataStorageProvider
    {
        string ProductBasePath { get; }
        string VendorsWithProductIdPath { get; }
        string WritePath { get; }
        List<Product> ReadProductBase();
        List<VendorsWithProductId> ReadListVendorsWithProductId();
        void WriteData<T>(List<T> list, string name);
    }
}