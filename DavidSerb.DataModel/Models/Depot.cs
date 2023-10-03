using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class Depot
    {
        public string DepotId { get; set; }
        public string DepotName { get; set; }

        public ICollection<Country> Countries { get; set; }

        public Depot(string depotId, string depotName)
        {
            DepotId = depotId;
            DepotName = depotName;
            Countries = new List<Country>();
        }
    }
}
