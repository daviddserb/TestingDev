
using DavidSerb.DataModel;
using DavidSerb.DataModel.Data;
using DavidSerb.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain.CorrelationService
{
    public class DepotCorrelationService : BaseCorrelationService<List<CorrelateData>>
    {
        public DepotCorrelationService(AppDbContext dbContext) : base(dbContext) { }

        public override List<CorrelateData> CorrelateData()
        {
            AppDbContext dbContext = DbContext;

            List<Depot> depots = dbContext.Depots
                .Include(depot => depot.Countries)
                .ToList();
            List<DrugUnit> drugUnits = dbContext.DrugUnits
                .Include(drugUnit => drugUnit.DrugType)
                .ToList();

            // BEFORE (foreach loops):
            //List<CorrelateData> correlateData = new List<CorrelateData>();
            //foreach (Depot depot in depots)
            //{
            //    foreach(DrugUnit drugUnit in drugUnits)
            //    {
            //        if (drugUnit.Depot?.DepotId == depot.DepotId)
            //        {
            //            foreach(Country country in depot.Countries)
            //            {
            //                correlateData.Add(new CorrelateData(
            //                    depot.DepotName,
            //                    country.CountryName,
            //                    drugUnit.DrugType?.DrugTypeName,
            //                    drugUnit.DrugUnitId,
            //                    drugUnit.PickNumber)
            //                );
            //            }
            //        }
            //    }
            //}

            // Testing
            //var x = depots.SelectMany(depot => depot.Countries);
            //var a = depots.Select(depot => drugUnits.Where(drugUnit => drugUnit.Depot?.DepotId == depot.DepotId));
            //var b = depots.SelectMany(depot => drugUnits.Where(drugUnit => drugUnit.Depot?.DepotId == depot.DepotId));
            //var c = depots.SelectMany(depot => drugUnits.Where(drugUnit => drugUnit.Depot?.DepotId == depot.DepotId).SelectMany(drugUnit => depot.Countries));

            // AFTER (LINQ):
            var correlateData = depots
                .SelectMany(depot => drugUnits
                    .Where(drugUnit => drugUnit.Depot?.DepotId == depot.DepotId)
                    .SelectMany(drugUnit => depot.Countries
                        .Select(country => new CorrelateData(
                            depot.DepotName,
                            country.CountryName,
                            drugUnit.DrugType?.DrugTypeName,
                            drugUnit.DrugUnitId,
                            drugUnit.PickNumber)
                        )
                    )
                )
                .ToList();

            return correlateData;
        }
    }
}
