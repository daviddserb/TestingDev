using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel
{
    public class DrugUnit
    {
        public string DrugUnitId { get; set; } // numeric/alphanumeric (ex. ABC123)
        public int PickNumber { get; set; }

        [ForeignKey("Depot")]
        public int DepotId { get; set; }
        public Depot Depot { get; set; }

        [ForeignKey("DrugType")]
        public int DrugTypeId { get; set; }
        public DrugType DrugType { get; set; }

        public DrugUnit(string drugUnitId, int pickNumber)
        {
            DrugUnitId = drugUnitId;
            PickNumber = pickNumber;
        }
    }
}
