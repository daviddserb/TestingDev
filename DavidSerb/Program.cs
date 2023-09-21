using DavidSerb.DataModel;
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
        public static DepotCorrelationService depotCorrelationService = new DepotCorrelationService(DataSet); // ??? nu stiu ce sa trimit exact

        static void Main(string[] args)
        {
            Console.WriteLine("Start:");

            Console.WriteLine("\nCountries:");
            foreach(Country country in DataSet.Countries)
            {
                Console.WriteLine($"ID: {country.CountryId}. name: {country.CountryName}");
            }

            Console.WriteLine("\nDepots:");
            foreach (Depot depot in DataSet.Depots)
            {
                Console.WriteLine($"ID: {depot.DepotId}. name: {depot.DepotName}");
            }

            Console.WriteLine("\nDrugUnits:");
            var drugUnits = DataSet.DrugUnits;
            foreach (DrugUnit drugUnit in drugUnits)
            {
                Console.WriteLine($"ID: {drugUnit.DrugUnitId}. value: {drugUnit.PickNumber} - depot: {drugUnit.Depot?.DepotName}");
            }

            Console.WriteLine("\nDrugTypes:");
            foreach (DrugType drugType in DataSet.DrugTypes)
            {
                Console.WriteLine($"ID: {drugType.DrugTypeId}. name: {drugType.DrugTypeName}");
            }

            Console.WriteLine("\nAssociations:");
            // Old Associations
            //DataSet.AssociateDrugs("3", 60, 90); // depot not found
            //DataSet.AssociateDrugs("1", 300, 400); // value interval not found
            //DataSet.AssociateDrugs("1", 10, 50); // depot already associated
            //DataSet.AssociateDrugs("1", 60, 90);
            //DataSet.AssociateDrugs("2", 220, 270);
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
            // Old Disassociations
            //DataSet.DisassociateDrugs(300, 350); // value interval not found
            //DataSet.DisassociateDrugs(190, 250); // only some depot already disassociated
            //DataSet.DisassociateDrugs(10, 40);
            depotInventoryService.DisassociateDrugs(ref drugUnits, 300, 350); // value interval not found
            depotInventoryService.DisassociateDrugs(ref drugUnits, 190, 250); // some depot are not associated
            depotInventoryService.DisassociateDrugs(ref drugUnits, 10, 40);

            Console.WriteLine("\nDrugUnits After Disassociations:");
            foreach (DrugUnit drugUnit in DataSet.DrugUnits)
            {
                Console.WriteLine($"ID: {drugUnit.DrugUnitId}. value: {drugUnit.PickNumber} - depot: {drugUnit.Depot?.DepotName}");
            }

            Console.WriteLine("\nDepotCorrelationService - CorrelateData():");
            var result = depotCorrelationService.CorrelateData();

            Console.WriteLine("\n DrugUnitDictionary:");
            // Old
            //Dictionary<string, List<DrugUnit>> drugUnitsDict = DrugUnitsGroupedByType();
            IList<DrugUnit> drugUnitsDictTest = new List<DrugUnit>();
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
