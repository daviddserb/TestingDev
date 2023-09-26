using DavidSerb.DataModel;
using DavidSerb.DataModel.Models;
using DavidSerb.Domain.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain
{
    public class SiteDistributionService : ISiteDistributionService
    {
        SystemDataSet systemDataSet = new SystemDataSet();

        public IEnumerable<DrugUnit> GetRequestedDrugUnits(string siteId, string drugCode, int quantity)
        {
            Site selectedSite = systemDataSet.Sites.FirstOrDefault(site => site.SiteId == siteId);
            if (selectedSite == null) throw new NotFoundException($"Site with id {siteId} not found.");
            else
            {
                DrugType selectedDrugType = systemDataSet.DrugTypes.FirstOrDefault(drugType => drugType.DrugTypeName == drugCode);
                if (selectedDrugType == null) Console.WriteLine($"DrugType with name '{drugCode}' not found.");
                else
                {
                    Country countryOfSite = systemDataSet.Countries
                        .FirstOrDefault(country => country.Sites.Contains(selectedSite));

                    List<Depot> availableDepots = systemDataSet.Depots
                        .Where(depot => depot.Countries.Contains(countryOfSite))
                        .ToList();

                    // BEFORE:
                    //List<DrugUnit> availableDrugUnits = new List<DrugUnit>();
                    //foreach (var availableDepot in availableDepots)
                    //{
                    //    foreach (DrugUnit drugUnit in systemDataSet.DrugUnits)
                    //    {
                    //        if (drugUnit.Depot?.DepotId == availableDepot.DepotId && drugUnit.DrugType?.DrugTypeName == drugCode)
                    //        {
                    //            availableDrugUnits.Add(drugUnit);
                    //        }
                    //    }
                    //}

                    // AFTER:
                    List<DrugUnit> availableDrugUnits = systemDataSet.DrugUnits
                        .Where(drugUnit =>
                            availableDepots.Any(availableDepot => availableDepot.DepotId == drugUnit.Depot?.DepotId)
                            && drugUnit.DrugType?.DrugTypeName == drugCode)
                        .ToList();

                    if (availableDrugUnits.Count >= quantity) return availableDrugUnits.Take(quantity);
                    else throw new NotFoundException("Not enough DrugUnits for the required quantity.");
                }
            }
            return new List<DrugUnit>(); // ??? (ma pot scapa de asta? eu vad doar cu posibilitatea ca in cazurile de if/else, sa arunc exceptie in cel rau => else dispare)
        }
    }
}
