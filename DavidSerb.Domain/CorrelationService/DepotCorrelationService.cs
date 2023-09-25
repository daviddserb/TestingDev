
using DavidSerb.DataModel;
using DavidSerb.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain.CorrelationService
{
    public class DepotCorrelationService : BaseCorrelationService<List<CorrelateData>>
    {
        public DepotCorrelationService(SystemDataSet dataSet) : base(dataSet) { }

        public override List<CorrelateData> CorrelateData()
        {
            SystemDataSet dataSet = this.DataSet;

            List<Depot> depots = dataSet.Depots;
            List<DrugUnit> drugUnits = dataSet.DrugUnits;

            // Old
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
            var x = depots.SelectMany(depot => depot.Countries);

            var a = depots.Select(depot => drugUnits.Where(drugUnit => drugUnit.Depot?.DepotId == depot.DepotId));
            var b = depots.SelectMany(depot => drugUnits.Where(drugUnit => drugUnit.Depot?.DepotId == depot.DepotId));
            var c = depots.SelectMany(depot => drugUnits.Where(drugUnit => drugUnit.Depot?.DepotId == depot.DepotId).SelectMany(drugUnit => depot.Countries));

            // New
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
