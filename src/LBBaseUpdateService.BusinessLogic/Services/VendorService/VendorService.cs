using System.Linq;
using System.Threading.Tasks;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Comparators;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Interfaces;
using LBBaseUpdateService.BusinessLogic.Services.VendorService.Models;

namespace LBBaseUpdateService.BusinessLogic.Services.VendorService
{
    public class VendorService : IVendorService
    {
        public Vendor[] GetListToAddAsync(Vendor[] externalVendors, Vendor[] internalVendors)
        {
            return externalVendors.Except(internalVendors, new VendorByIdComparer())
                .ToArray();
        }

        public int[] GetVendorIdSheetToDelete(Vendor[] externalVendors, Vendor[] internalVendors)
        {
            return internalVendors.Except(externalVendors, new VendorByIdComparer())
                .Select(v => v.VendorId)
                .ToArray();
        }
    }
}