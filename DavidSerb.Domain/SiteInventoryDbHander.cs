using DavidSerb.DataModel.Data;
using DavidSerb.DataModel.Models;
using DavidSerb.Domain.CustomExceptions;
using DavidSerb.Domain.DbService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain
{
    public class SiteInventoryDbHander : ISiteInventoryDbHandler
    {
        AppDbContext dbContext = new AppDbContext();
        SiteDistributionService siteDistributionService = new SiteDistributionService();

        public async void UpdateSiteInventory(string destinationSiteId, string requestedDrugCode, int requestedQuantity)
        {
            List<DrugUnit> siteDrugUnits = await siteDistributionService.GetRequestedDrugUnits(destinationSiteId, requestedDrugCode, requestedQuantity);

            // Mark the movements of Drugs from Depot to Site: Disassociate Depot from DrugUnit and Associate Site to DrugUnit
            foreach (DrugUnit drugUnit in siteDrugUnits)
            {
                if (drugUnit.DepotId == null) throw new ClientException($"DrugUnit with id {drugUnit.DrugUnitId} is not associated with a Depot.");

                drugUnit.DepotId = null;
                drugUnit.SiteId = destinationSiteId;
                // Mark the entity (drugUnit) as changed => EFC knows to don't create a new one but to update the existing one
                dbContext.Entry(drugUnit).State = EntityState.Modified;
            }
            dbContext.SaveChanges();
        }
    }
}
