using DavidSerb.DataModel;
using DavidSerb.DataModel.Data;
using DavidSerb.DataModel.Models;
using DavidSerb.Domain.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain
{
    public class SiteDistributionService : ISiteDistributionService
    {
        AppDbContext dbContext = new AppDbContext();

        public async Task<List<DrugUnit>> GetRequestedDrugUnits(string siteId, string drugCode, int quantity)
        {
            Site selectedSite = dbContext.Sites
                .Include(site => site.Country)
                .FirstOrDefault(site => site.SiteId == siteId);
            if (selectedSite == null) throw new NotFoundException($"Site with id {siteId} not found.");

            DrugType selectedDrugType = dbContext.DrugTypes.FirstOrDefault(drugType => drugType.DrugTypeName == drugCode);
            if (selectedDrugType == null) throw new NotFoundException($"DrugType with name '{drugCode}' not found.");

            var availableDrugUnits = await dbContext.DrugUnits
                .Where(drugUnit => selectedSite.Country.DepotId == drugUnit.Depot.DepotId && drugUnit.DrugType.DrugTypeName == drugCode)
                .Include(drugUnit => drugUnit.Depot)
                .ToListAsync();

            if (availableDrugUnits.Count() >= quantity) return availableDrugUnits.Take(quantity).ToList();
            else throw new NotFoundException("Not enough DrugUnits for the required quantity.");
        }
    }
}
