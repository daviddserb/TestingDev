using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class CorrelateData
    {
        public string DepotName { get; set; }

        public string CountryName { get; set; }

        public string DrugTypeName { get; set; }

        public string DrugUnitId { get; set; }
        public int PickNumber { get; set; }

        public CorrelateData(string depotName, string countryName, string drugTypeName, string drugUnitId, int pickNumber)
        {
            DepotName = depotName;
            CountryName = countryName;
            DrugTypeName = drugTypeName;
            DrugUnitId = drugUnitId;
            PickNumber = pickNumber;
        }
    }
}
