using DavidSerb.DataModel;
using DavidSerb.DataModel.Data;
using DavidSerb.DataModel.Models;
using DavidSerb.Domain.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain
{
    public class DepotInventoryService : IDepotInventoryService
    {
        AppDbContext dbContext = new AppDbContext();

        /// <summary>
        /// Associate Drugs To Depot
        /// </summary>
        /// <param name="drugUnits"></param>
        /// <param name="depotId"></param>
        /// <param name="startPickNumber"></param>
        /// <param name="endPickNumber"></param>
        public async void AssociateDrugs(List<DrugUnit> drugUnits, string depotId, int startPickNumber, int endPickNumber)
        {
            Depot selectedDepot = dbContext.Depots.FirstOrDefault(depot => depot.DepotId == depotId);
            if (selectedDepot == null) throw new NotFoundException($"Depot with id {depotId} not found.");

            List<DrugUnit> drugUnitsToAssociate = drugUnits
                .Where(drugUnit => drugUnit.PickNumber >= startPickNumber && drugUnit.PickNumber <= endPickNumber)
                .ToList();
            if (drugUnitsToAssociate.Count == 0) throw new NotFoundException($"There are no DrugUnits with the PickNumber between [{startPickNumber}, {endPickNumber}].");
            
            foreach (DrugUnit drugUnit in drugUnitsToAssociate)
            {
                if (drugUnit.DepotId != null) throw new ClientException($"DrugUnit with id {drugUnit.DrugUnitId} is already associated to a Depot with id {drugUnit.DepotId}.");
                
                drugUnit.DepotId = selectedDepot.DepotId;
                // Mark the entity (drugUnit) as changed => EFC knows to don't create a new one but to update the existing one
                dbContext.Entry(drugUnit).State = EntityState.Modified;
            }
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Disassociate Drugs From Depot
        /// </summary>
        /// <param name="drugUnits"></param>
        /// <param name="startPickNumber"></param>
        /// <param name="endPickNumber"></param>
        public async void DisassociateDrugs(List<DrugUnit> drugUnits, int startPickNumber, int endPickNumber)
        {
            List<DrugUnit> drugUnitsToDisassociate = drugUnits
                .Where(drugUnit => drugUnit.PickNumber >= startPickNumber && drugUnit.PickNumber <= endPickNumber)
                .ToList();
            if (drugUnitsToDisassociate.Count == 0) throw new NotFoundException($"There are no DrugUnits with the PickNumber between [{startPickNumber}, {endPickNumber}].");

            foreach (DrugUnit drugUnit in drugUnitsToDisassociate)
            {
                if (drugUnit.DepotId == null) throw new ClientException($"DrugUnit with id {drugUnit.DrugUnitId} is not associated with a Depot.");
                
                drugUnit.DepotId = null;
                // Mark the entity (drugUnit) as changed => EFC knows to don't create a new one but to update the existing one
                dbContext.Entry(drugUnit).State = EntityState.Modified;
            }
            dbContext.SaveChanges();
        }
    }
}
