using System.Threading.Tasks;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.VendorService.Interfaces
{
    public interface IVendorService
    {
        Vendor[] GetSheetToAddAsync(Vendor[] externalVendors, Vendor[] internalVendors);
        int[] GetVendorIdSheetToDelete(Vendor[] externalVendors, Vendor[] internalVendors);
    }
}