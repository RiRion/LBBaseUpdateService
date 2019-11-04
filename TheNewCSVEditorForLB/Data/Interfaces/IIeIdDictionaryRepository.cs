using TheNewCSVEditorForLB.Data.Models;
using System.Collections.Generic;

namespace TheNewCSVEditorForLB.Data.Interfaces
{
    public interface IIeIdDictionaryRepository
    {
        List<IeIdDictionary> DictionaryID { get; }
        bool GetFromServer(string path);
    }
}