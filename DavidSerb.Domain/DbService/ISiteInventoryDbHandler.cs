using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain.DbService
{
    interface ISiteInventoryDbHandler
    {
        void UpdateSiteInventory(string destinationSiteId, string requestedDrugCode, int requestedQuantity);
    }
}
