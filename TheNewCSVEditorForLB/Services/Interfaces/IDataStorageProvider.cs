using System.Collections.Generic;
using CsvHelper.Configuration;

namespace TheNewCSVEditorForLB.Services.Interfaces
{
    public interface IDataStorageProvider<T>
    {
        string ReadPath { get; set; }
        string WritePath { get; set; }
        List<T> ReadData<TMap>() where TMap : ClassMap;
        void WriteData(List<T> products, string name);
    }
}