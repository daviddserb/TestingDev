
using DavidSerb.DataModel;
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

            List<CorrelateData> correlateData = new List<CorrelateData>();

            foreach (Depot depot in depots)
            {
                foreach(DrugUnit drugUnit in drugUnits)
                {
                    if (drugUnit.Depot?.DepotId == depot.DepotId)
                    {
                        foreach(Country country in depot.Countries)
                        {
                            CorrelateData data = new CorrelateData
                            {
                                DepotName = depot.DepotName,
                                CountryName = country.CountryName,
                                DrugTypeName = drugUnit.DrugType?.DrugTypeName,
                                DrugUnitId = drugUnit.DrugUnitId,
                                PickNumber = drugUnit.PickNumber
                            };

                            correlateData.Add(data);
                        }
                    }
                }
            }
            return correlateData;
        }
    }
}
