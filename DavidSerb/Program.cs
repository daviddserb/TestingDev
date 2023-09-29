using DavidSerb.DataModel;
using DavidSerb.DataModel.Data;
using DavidSerb.DataModel.Models;
using DavidSerb.Domain;
using DavidSerb.Domain.CorrelationService;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
        public static SiteInventoryDbHander siteInventoryDbHandler = new SiteInventoryDbHander();

        public static AppDbContext dbContext = new AppDbContext();

        static void Main(string[] args)
        {
            Console.WriteLine("Start:");

            Console.WriteLine("\nCountries:");
            // BEFORE (from In-Memory):
            foreach (Country country in DataSet.Countries)
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

            Console.WriteLine("\nDatabase:");
            //PopulateDatabase();
            //AssociateDrugs();
            //DisassociateDrugs();
            //SiteDistributionService();
            //UpdateSiteInventory();

            Console.WriteLine("Stop.");
            Console.ReadKey(); // keep Console open
        }

        public static async void PopulateDatabase()
        {
            Console.WriteLine("\nPopulateDatabase:");
            // Instantiate objects
            Country country1 = new Country("1", "CountryA");
            Country country2 = new Country("2", "CountryB");

            Depot depot1 = new Depot("1", "DepotA");
            Depot depot2 = new Depot("2", "DepotB");

            DrugUnit drugUnit1 = new DrugUnit("ABC111", 10);
            DrugUnit drugUnit2 = new DrugUnit("ABC112", 20);
            DrugUnit drugUnit3 = new DrugUnit("ABC113", 30);
            DrugUnit drugUnit4 = new DrugUnit("ABC114", 40);
            DrugUnit drugUnit5 = new DrugUnit("ABC115", 50);

            DrugUnit drugUnit6 = new DrugUnit("ERT123", 60);
            DrugUnit drugUnit7 = new DrugUnit("ERT124", 70);
            DrugUnit drugUnit8 = new DrugUnit("ERT125", 80);
            DrugUnit drugUnit9 = new DrugUnit("ERT126", 90);
            DrugUnit drugUnit10 = new DrugUnit("ERT127", 100);

            DrugUnit drugUnit11 = new DrugUnit("IOP987", 200);
            DrugUnit drugUnit12 = new DrugUnit("IOP986", 210);
            DrugUnit drugUnit13 = new DrugUnit("IOP985", 220);
            DrugUnit drugUnit14 = new DrugUnit("IOP984", 230);
            DrugUnit drugUnit15 = new DrugUnit("IOP983", 240);

            DrugUnit drugUnit16 = new DrugUnit("DEV999", 250);
            DrugUnit drugUnit17 = new DrugUnit("DEV888", 260);
            DrugUnit drugUnit18 = new DrugUnit("DEV777", 270);
            DrugUnit drugUnit19 = new DrugUnit("DEV666", 280);
            DrugUnit drugUnit20 = new DrugUnit("DEV555", 290);

            DrugType drugType1 = new DrugType("1", "DrugType1");
            DrugType drugType2 = new DrugType("2", "DrugType2");

            Site site1 = new Site("SiteId1", "SiteA", "1");
            Site site2 = new Site("SiteId2", "SiteB", "1");
            Site site3 = new Site("SiteId3", "SiteC", "2");

            // Set relationships
            country1.Depot = depot1;
            country1.Sites.Add(site1);
            country1.Sites.Add(site2);

            country2.Depot = depot2;
            country2.Sites.Add(site3);

            depot1.Countries.Add(country1);
            depot1.Countries.Add(country2);

            depot2.Countries.Add(country1);

            /*!!! Save values in the FK Property, not in the Navigational Property:
            * AFTER: drugUnit1.Depot = depot1;
            * BEFORE: drugUnit1.Depot = depot1;*/
            drugUnit1.DepotId = depot1.DepotId;
            drugUnit1.DrugTypeId = drugType1.DrugTypeId;

            drugUnit2.DepotId = depot2.DepotId;
            drugUnit2.DrugTypeId = drugType2.DrugTypeId;

            drugUnit3.DepotId = depot2.DepotId;
            drugUnit3.DrugTypeId = drugType2.DrugTypeId;

            drugUnit4.DepotId = depot1.DepotId;
            drugUnit4.DrugTypeId = drugType2.DrugTypeId;

            drugUnit5.DepotId = depot2.DepotId;
            drugUnit5.DrugTypeId = drugType1.DrugTypeId;

            drugUnit6.DepotId = depot2.DepotId;
            drugUnit6.DrugTypeId = drugType1.DrugTypeId;

            drugUnit7.DepotId = depot2.DepotId;
            drugUnit7.DrugTypeId = drugType1.DrugTypeId;

            drugUnit8.DepotId = depot1.DepotId;
            drugUnit8.DrugTypeId = drugType2.DrugTypeId;

            drugUnit9.DepotId = depot2.DepotId;
            drugUnit9.DrugTypeId = drugType2.DrugTypeId;

            drugUnit10.DepotId = depot1.DepotId;
            drugUnit10.DrugTypeId = drugType2.DrugTypeId;

            drugUnit11.DepotId = depot1.DepotId;

            drugUnit12.DrugTypeId = drugType2.DrugTypeId;

            drugUnit13.DepotId = depot1.DepotId;
            drugUnit13.DrugTypeId = drugType2.DrugTypeId;

            drugUnit16.DepotId = depot2.DepotId;
            drugUnit16.DrugTypeId = drugType2.DrugTypeId;

            drugUnit17.DepotId = depot1.DepotId;
            drugUnit17.DrugTypeId = drugType1.DrugTypeId;

            drugUnit18.DepotId = depot1.DepotId;
            drugUnit18.DrugTypeId = drugType1.DrugTypeId;

            drugUnit19.DepotId = depot2.DepotId;
            drugUnit19.DrugTypeId = drugType2.DrugTypeId;

            // Start tracking
            await dbContext.Countries.AddRangeAsync(country1, country2);
            await dbContext.Depots.AddRangeAsync(depot1, depot2);
            await dbContext.DrugUnits.AddRangeAsync(drugUnit1, drugUnit2, drugUnit3, drugUnit4, drugUnit5, drugUnit6, drugUnit7, drugUnit8, drugUnit9, drugUnit10, drugUnit11, drugUnit12, drugUnit13, drugUnit14, drugUnit15, drugUnit16, drugUnit17, drugUnit18, drugUnit19, drugUnit20);
            await dbContext.DrugTypes.AddRangeAsync(drugType1, drugType2);
            await dbContext.Sites.AddRangeAsync(site1, site2, site3);

            // Save to DB the tracking entities
            await dbContext.SaveChangesAsync();
        }

        public static async void SiteDistributionService()
        {
            Console.WriteLine("\nSiteDistributionService():");
            //await siteDistributionService.GetRequestedDrugUnits("0", "DrugType1", 3); // siteId doesn't exist
            //await siteDistributionService.GetRequestedDrugUnits("SiteId1", "Drug XILOF", 3); // drugCode doesn't exist
            //await siteDistributionService.GetRequestedDrugUnits("SiteId1", "DrugType2", 100); // quantity too big
            List<DrugUnit> requestedDrugUnits = await siteDistributionService.GetRequestedDrugUnits("SiteId1", "DrugType1", 3);
            foreach (var requestedDrugUnit in requestedDrugUnits)
            {
                Console.WriteLine($"DrugUnit.Id '{requestedDrugUnit.DrugUnitId}' - DrugType.Name '{requestedDrugUnit.DrugType.DrugTypeName}' - Depot.Id '{requestedDrugUnit.DepotId}'.");
            }
        }

        public static async void UpdateSiteInventory()
        {
            Console.WriteLine("\nUpdateSiteInventory():");
            siteInventoryDbHandler.UpdateSiteInventory("SiteId1", "DrugType1", 3);
        }

        public static async void AssociateDrugs()
        {
            Console.WriteLine("\nAssociateDrugs():");

            var allDrugUnits = dbContext.DrugUnits.ToList(); // ??? await

            //depotInventoryService.AssociateDrugs(allDrugUnits, "3", 60, 90); // depot not found
            //depotInventoryService.AssociateDrugs(allDrugUnits, "1", 300, 400); // value interval not found
            //depotInventoryService.AssociateDrugs(allDrugUnits, "1", 10, 30); // depot already associated
            depotInventoryService.AssociateDrugs(allDrugUnits, "2", 220, 240);

            Console.WriteLine("\nDrugUnits After Associations:");
            foreach (DrugUnit drugUnit in allDrugUnits)
            {
                Console.WriteLine($"ID: {drugUnit.DrugUnitId}. value: {drugUnit.PickNumber} - depot: {drugUnit.DepotId} - drugType: {drugUnit.DrugTypeId}");
            }
        }
        public static async void DisassociateDrugs()
        {
            Console.WriteLine("\nDisassociations:");

            var allDrugUnits = dbContext.DrugUnits.ToList(); // ??? await

            //depotInventoryService.DisassociateDrugs(allDrugUnits, 300, 350); // value interval not found
            //depotInventoryService.DisassociateDrugs(allDrugUnits, 250, 270); // depot is not associated
            depotInventoryService.DisassociateDrugs(allDrugUnits, 220, 240);

            Console.WriteLine("\nDrugUnits After Disassociations:");
            foreach (DrugUnit drugUnit in allDrugUnits)
            {
                Console.WriteLine($"ID: {drugUnit.DrugUnitId}. value: {drugUnit.PickNumber} - depot: {drugUnit.DepotId} - drugType: {drugUnit.DrugTypeId}");
            }
        }
    }
}
