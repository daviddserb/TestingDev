using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel
{
    public class CorrelateData
    {
        public string DepotName { get; set; }

        public string CountryName { get; set; }

        public string DrugTypeName { get; set; }

        public string DrugUnitId { get; set; }
        public int PickNumber { get; set; }

        // TODO: sa fac constructor cu toate proprietatile
    }
}
