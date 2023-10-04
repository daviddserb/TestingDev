using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class Country
    {
        [Required]
        public string CountryId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(5, ErrorMessage = "Country Name must be at least {1} characters.")]
        public string CountryName { get; set; }

        [ForeignKey("Depot")]
        public string DepotId { get; set; }
        public Depot Depot { get; set; } // navigation property

        public ICollection<Site> Sites { get; set; }

        public Country() {} // ??? TODO: sa vad daca am nevoie

        public Country(string countryId, string countryName)
        {
            CountryId = countryId;
            CountryName = countryName;
            Sites = new List<Site>();
        }
    }
}
