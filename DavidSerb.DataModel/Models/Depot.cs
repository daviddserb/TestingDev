using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class Depot
    {
        [Required]
        public string DepotId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MinLength(5, ErrorMessage = "Depot Name must be at least {1} characters.")]
        public string DepotName { get; set; }

        public ICollection<Country> Countries { get; set; }

        public Depot() {}

        public Depot(string depotId, string depotName)
        {
            DepotId = depotId;
            DepotName = depotName;
            Countries = new List<Country>();
        }
    }
}
