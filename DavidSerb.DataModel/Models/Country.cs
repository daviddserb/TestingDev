using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        [ForeignKey("Depot")]
        public int DepotId { get; set; }
        // navigation property
        public Depot Depot { get; set; }

        public ICollection<Site> Sites { get; set; }

        public Country(int countryId, string countryName)
        {
            CountryId = countryId;
            CountryName = countryName;
            Sites = new List<Site>();
        }
    }
}
