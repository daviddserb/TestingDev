using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel
{
    public class SystemDataSet
    {
        public List<Country> Countries { get; } = new List<Country>();
        public List<Depot> Depots { get; } = new List<Depot>();
        public List<DrugUnit> DrugUnits { get; } = new List<DrugUnit>();
        public List<DrugType> DrugTypes { get; } = new List<DrugType>();

        public SystemDataSet()
        {
            // Set Values (declaration + initialization)
            Country c1 = new Country(1, "Country A");
            Country c2 = new Country(2, "Country B");

            Depot d1 = new Depot("1", "Depot A");
            Depot d2 = new Depot("2", "Depot B");

            //NOTE: Do not associate any of the drug units to a depot
            DrugUnit du1 = new DrugUnit("ABC111", 10);
            DrugUnit du2 = new DrugUnit("ABC112", 20);
            DrugUnit du3 = new DrugUnit("ABC113", 30);
            DrugUnit du4 = new DrugUnit("ABC114", 40);
            DrugUnit du5 = new DrugUnit("ABC115", 50);

            DrugUnit du6 = new DrugUnit("ERT123", 60);
            DrugUnit du7 = new DrugUnit("ERT124", 70);
            DrugUnit du8 = new DrugUnit("ERT125", 80);
            DrugUnit du9 = new DrugUnit("ERT126", 90);
            DrugUnit du10 = new DrugUnit("ERT127", 100);

            DrugUnit du11 = new DrugUnit("IOP987", 200);
            DrugUnit du12 = new DrugUnit("IOP986", 210);
            DrugUnit du13 = new DrugUnit("IOP985", 220);
            DrugUnit du14 = new DrugUnit("IOP984", 230);
            DrugUnit du15 = new DrugUnit("IOP983", 240);

            DrugUnit du16 = new DrugUnit("DEV999", 250);
            DrugUnit du17 = new DrugUnit("DEV888", 260);
            DrugUnit du18 = new DrugUnit("DEV777", 270);
            DrugUnit du19 = new DrugUnit("DEV666", 280);
            DrugUnit du20 = new DrugUnit("DEV555", 290);

            DrugType dt1 = new DrugType(1, "DrugType 1");
            DrugType dt2 = new DrugType(2, "DrugType 2");

            // Set Relationships
            c1.Depot = d1;
            c2.Depot = d2;

            d1.Countries.Add(c1);
            d1.Countries.Add(c2);

            d2.Countries.Add(c1);

            du1.Depot = d1;
            du1.DrugType = dt1;

            du2.Depot = d2;
            du2.DrugType = dt2;

            du3.Depot = d1;
            du3.DrugType = dt2;

            du4.Depot = d1;
            du4.DrugType = dt2;

            du5.Depot = d2;
            du5.DrugType = dt1;

            //Add the objects to the properties
            Countries.Add(c1);
            Countries.Add(c2);

            Depots.Add(d1);
            Depots.Add(d2);

            DrugUnits.Add(du1);
            DrugUnits.Add(du2);
            DrugUnits.Add(du3);
            DrugUnits.Add(du4);
            DrugUnits.Add(du5);
            DrugUnits.Add(du6);
            DrugUnits.Add(du7);
            DrugUnits.Add(du8);
            DrugUnits.Add(du9);
            DrugUnits.Add(du10);
            DrugUnits.Add(du11);
            DrugUnits.Add(du12);
            DrugUnits.Add(du13);
            DrugUnits.Add(du14);
            DrugUnits.Add(du15);
            DrugUnits.Add(du16);
            DrugUnits.Add(du17);
            DrugUnits.Add(du18);
            DrugUnits.Add(du19);
            DrugUnits.Add(du20);

            DrugTypes.Add(dt1);
            DrugTypes.Add(dt2);
        }

        public void AssociateDrugs(string depotId, int startPickNumber, int endPickNumber)
        {
            Depot depot = Depots.FirstOrDefault(dep => dep.DepotId == depotId);
            if (depot != null)
            {
                List<DrugUnit> drugUnitsToAssociate = DrugUnits
                    .Where(drugUnit => drugUnit.PickNumber >= startPickNumber && drugUnit.PickNumber <= endPickNumber)
                    .ToList();

                if (drugUnitsToAssociate.Count == 0) Console.WriteLine($"There are no DrugUnits with the PickNumber between ({startPickNumber}, {endPickNumber}).");
                else
                {
                    foreach (DrugUnit drugUnit in drugUnitsToAssociate)
                    {
                        if (drugUnit.Depot != null) Console.WriteLine($"DrugUnit {drugUnit.DrugUnitId} is already associated to Depot.");
                        else drugUnit.Depot = depot;
                    }
                }
            }
            else Console.WriteLine($"Depot with id {depotId} not found.");
        }
        public void DisassociateDrugs(int startPickNumber, int endPickNumber)
        {
            List<DrugUnit> drugUnitsToDisassociate = DrugUnits
                .Where(drugUnit => drugUnit.PickNumber >= startPickNumber && drugUnit.PickNumber <= endPickNumber)
                .ToList();

            if (drugUnitsToDisassociate.Count == 0) Console.WriteLine($"There are no DrugUnits with the PickNumber between ({startPickNumber}, {endPickNumber}).");
            else
            {
                foreach (DrugUnit drugUnit in drugUnitsToDisassociate)
                {
                    if (drugUnit.Depot != null) drugUnit.Depot = null;
                    else Console.WriteLine($"DrugUnit {drugUnit.DrugUnitId} is not associated with Depot.");
                }
            }
        }
    }
}
