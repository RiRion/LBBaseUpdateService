using CsvHelper.Configuration;
using System;
using System.Collections.Generic;

namespace TheNewCSVEditorForLB.Services.Interfaces
{
	public interface IDataStorageProvider<T>
	{
		String ReadPath { get; set; }
		String WritePath { get; set; }
		List<T> ReadData<TMap>() where TMap : ClassMap;
		void WriteData(List<T> products, String name);
	}
}