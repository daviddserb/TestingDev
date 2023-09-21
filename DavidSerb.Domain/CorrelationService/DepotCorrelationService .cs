
using DavidSerb.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain.CorrelationService
{
    public class DepotCorrelationService : BaseCorrelationService<List<Depot>>
    {
        // ??? dc trebuie sa fac constructor? adica de ce am eroare daca il comentez
        public DepotCorrelationService(SystemDataSet dataSet) : base(dataSet) { }

        public override List<Depot> CorrelateData()
        {
            // Depot Name, Country Name, Drug Type Name, Drug Unit Id and Pick Number.
            // NOTE: Include all depots in the result, regardless of the association with other data 
            SystemDataSet dataSet = this.DataSet;

            throw new NotImplementedException();
        }
    }
}
