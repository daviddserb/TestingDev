using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DavidSerb.Web.Models
{
    public class DrugUnit
    {
        public string DrugUnitId { get; set; } // numeric/alphanumeric (ex. ABC123)
        public int PickNumber { get; set; }

        [ForeignKey("Depot")]
        public string DepotId { get; set; }
        public Depot Depot { get; set; }

        [ForeignKey("DrugType")]
        public string DrugTypeId { get; set; }
        public DrugType DrugType { get; set; }

        [ForeignKey("Site")]
        public string SiteId { get; set; }
        public Site Site { get; set; }

        public DrugUnit(string drugUnitId, int pickNumber)
        {
            DrugUnitId = drugUnitId;
            PickNumber = pickNumber;
        }
    }
}