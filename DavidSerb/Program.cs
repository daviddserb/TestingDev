using DavidSerb.DataModel;
using DavidSerb.DataModel.Models;
using DavidSerb.Domain;
using DavidSerb.Domain.CorrelationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb
{
    public class Program
    {
        public static SystemDataSet DataSet = new SystemDataSet();
        public static DepotInventoryService depotInventoryService = new DepotInventoryService();
        public static DepotCorrelationService depotCorrelationService = new DepotCorrelationService(DataSet);
        public static SiteDistributionService siteDistributionService = new SiteDistributionService();

        static void Main(string[] args)
        {
            Console.WriteLine("Start:");

            Console.WriteLine("\nCountries:");
            foreach(Country country in DataSet.Countries)
            {
                Console.WriteLine($"Country.ID '{country.CountryId}' - Country.Name '{country.CountryName}' - Depot.Id '{country.Depot.DepotId}' - Site.Id/s '{String.Join(", ", country.Sites.Select(site => site.SiteId))}'");
            }

            Console.WriteLine("\nDepots:");
            foreach (Depot depot in DataSet.Depots)
            {
                Console.WriteLine($"Depot.ID '{depot.DepotId}' - Depot.Name '{depot.DepotName}' - Country.Name/s '{String.Join(", ", depot.Countries.Select(country => country.CountryName))}'");
            }

            Console.WriteLine("\nDrugUnits:");
            var drugUnits = DataSet.DrugUnits;
            foreach (DrugUnit drugUnit in drugUnits)
            {
                Console.WriteLine($"DrugUnit.ID '{drugUnit.DrugUnitId}' - DrugUnit.PickNumber '{drugUnit.PickNumber}' - Depot.Name: {drugUnit.Depot?.DepotName}");
            }

            Console.WriteLine("\nDrugTypes:");
            foreach (DrugType drugType in DataSet.DrugTypes)
            {
                Console.WriteLine($"DrugType.ID '{drugType.DrugTypeId}' - DrugType.Name '{drugType.DrugTypeName}'");
            }

            Console.WriteLine("\nAssociations:");
            depotInventoryService.AssociateDrugs(ref drugUnits, "3", 60, 90); // depot not found
            depotInventoryService.AssociateDrugs(ref drugUnits, "1", 300, 400); // value interval not found
            depotInventoryService.AssociateDrugs(ref drugUnits, "1", 10, 50); // depot already associated
            depotInventoryService.AssociateDrugs(ref drugUnits, "2", 220, 270);

            Console.WriteLine("\nDrugUnits After Associations:");
            foreach (DrugUnit drugUnit in DataSet.DrugUnits)
            {
                Console.WriteLine($"ID: {drugUnit.DrugUnitId}. value: {drugUnit.PickNumber} - depot: {drugUnit.Depot?.DepotName}");
            }

            Console.WriteLine("\nDisassociations:");
            depotInventoryService.DisassociateDrugs(ref drugUnits, 300, 350); // value interval not found
            depotInventoryService.DisassociateDrugs(ref drugUnits, 190, 250); // some depot are not associated
            depotInventoryService.DisassociateDrugs(ref drugUnits, 10, 30);

            Console.WriteLine("\nDrugUnits After Disassociations:");
            foreach (DrugUnit drugUnit in DataSet.DrugUnits)
            {
                Console.WriteLine($"ID: {drugUnit.DrugUnitId}. value: {drugUnit.PickNumber} - depot: {drugUnit.Depot?.DepotName}");
            }

            Console.WriteLine("\nDepotCorrelationService - CorrelateData():");
            List<CorrelateData> correlateData = depotCorrelationService.CorrelateData();
            int count = 0;
            foreach (CorrelateData cData in correlateData)
            {
                Console.WriteLine($"{++count}. DepotName '{cData.DepotName}' - CountryName '{cData.CountryName}' - DrugTypeName '{cData.DrugTypeName}' - DrugUnitId '{cData.DrugUnitId}' - PickNumber '{cData.PickNumber}'");
            }

            Console.WriteLine("\n DrugUnitDictionary:");
            IList<DrugUnit> drugUnitsDictTest = drugUnits;
            foreach (var kvp in drugUnitsDictTest.ToGroupedDrugUnits())
            {
                Console.WriteLine($"Drug Type: {kvp.Key}");

                foreach (DrugUnit drugUnit in kvp.Value)
                {
                    Console.WriteLine($"DrugUnit: Id {drugUnit.DrugUnitId}. {drugUnit.PickNumber} value");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nSiteDistributionService:");
            siteDistributionService.GetRequestedDrugUnits("0", "DrugType1", 3); // siteId doesn't exist
            siteDistributionService.GetRequestedDrugUnits("SiteId1", "Drug XILOF", 3); // drugCode doesn't exist
            //siteDistributionService.GetRequestedDrugUnits("SiteId1", "DrugType1", 100); //quantity too big
            var requestedDrugUnits = siteDistributionService.GetRequestedDrugUnits("SiteId1", "DrugType1", 3);
            foreach (var requestedDrugUnit in requestedDrugUnits)
            {
                Console.WriteLine($"DrugUnit.Id '{requestedDrugUnit.DrugUnitId}' - DrugType.Name '{requestedDrugUnit.DrugType.DrugTypeName}' - Depot.Id '{requestedDrugUnit.Depot.DepotId}'.");
            }

            Console.WriteLine("Stop.");
            Console.ReadKey(); // keep Console open
        }
    }
}
