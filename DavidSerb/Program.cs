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

        static void Main(string[] args)
        {
            Console.WriteLine("Start:");

            Console.WriteLine("\nCountries:");
            foreach(Country country in DataSet.Countries)
            {
                Console.WriteLine($"Country.ID '{country.CountryId}' - Country.Name '{country.CountryName}' - Depot.Id '{country.Depot.DepotId}'");
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
            depotInventoryService.AssociateDrugs(ref drugUnits, "1", 60, 90);
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
            //Dictionary<string, List<DrugUnit>> drugUnitsDict = DrugUnitsGroupedByType(); // Old
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

            Console.WriteLine("Stop.");
            Console.ReadKey(); // keep Console open
        }

        // Old
        //public static Dictionary<string, List<DrugUnit>> DrugUnitsGroupedByType()
        //{
        //    Dictionary<string, List<DrugUnit>> drugUnitsDict = new Dictionary<string, List<DrugUnit>>();

        //    foreach (DrugUnit drugUnit in DataSet.DrugUnits)
        //    {
        //        string drugTypeName = drugUnit.DrugType?.DrugTypeName;

        //        if (drugTypeName != null)
        //        {
        //            if (!drugUnitsDict.ContainsKey(drugTypeName)) drugUnitsDict[drugTypeName] = new List<DrugUnit>();

        //            drugUnitsDict[drugTypeName].Add(drugUnit);
        //        }
        //    }

        //    return drugUnitsDict;
        //}
    }
}
