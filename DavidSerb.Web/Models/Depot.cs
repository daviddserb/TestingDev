using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DavidSerb.Web.Models
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