using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class DrugUnit
    {
        [Required]
        [DataType(DataType.Text)]
        public string DrugUnitId { get; set; } // numeric/alphanumeric (ex. ABC123)
        [Required]
        [Range(10, 500, ErrorMessage = "Values only between [{0}, {1}]")]
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

        public DrugUnit() {}

        public DrugUnit(string drugUnitId, int pickNumber)
        {
            DrugUnitId = drugUnitId;
            PickNumber = pickNumber;
        }
    }
}
