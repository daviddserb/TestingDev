using DavidSerb.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain
{
    public class DepotInventoryService : IDepotInventoryService
    {
        SystemDataSet systemDataSet = new SystemDataSet();

        public void AssociateDrugs(ref List<DrugUnit> drugUnits, string depotId, int startPickNumber, int endPickNumber)
        {
            Depot depot = systemDataSet.Depots.FirstOrDefault(dep => dep.DepotId == depotId);

            if (depot == null) Console.WriteLine($"Depot with id {depotId} not found.");
            else
            {
                List<DrugUnit> drugUnitsToAssociate = drugUnits
                    .Where(drugUnit => drugUnit.PickNumber >= startPickNumber && drugUnit.PickNumber <= endPickNumber)
                    .ToList();

                if (drugUnitsToAssociate.Count == 0) Console.WriteLine($"There are no DrugUnits with the PickNumber between [{startPickNumber}, {endPickNumber}].");
                else
                {
                    foreach (DrugUnit drugUnit in drugUnitsToAssociate)
                    {
                        if (drugUnit.Depot != null) Console.WriteLine($"DrugUnit with id {drugUnit.DrugUnitId} is already associated to a Depot with id {drugUnit.Depot.DepotId}.");
                        else drugUnit.Depot = depot;
                    }
                }
            }
        }

        public void DisassociateDrugs(ref List<DrugUnit> drugUnits, int startPickNumber, int endPickNumber)
        {
            List<DrugUnit> drugUnitsToDisassociate = drugUnits
                .Where(drugUnit => drugUnit.PickNumber >= startPickNumber && drugUnit.PickNumber <= endPickNumber)
                .ToList();

            if (drugUnitsToDisassociate.Count == 0) Console.WriteLine($"There are no DrugUnits with the PickNumber between [{startPickNumber}, {endPickNumber}].");
            else
            {
                foreach (DrugUnit drugUnit in drugUnitsToDisassociate)
                {
                    if (drugUnit.Depot == null) Console.WriteLine($"DrugUnit with id {drugUnit.DrugUnitId} is not associated with a Depot.");
                    else drugUnit.Depot = null;
                }
            }
        }
    }
}
