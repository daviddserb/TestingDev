using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class Site
    {
        [Required]
        public string SiteId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MinLength(5, ErrorMessage = "Site Name must be at least {1} characters.")]
        public string SiteName { get; set; }

        [ForeignKey("Country")]
        [Column("CountryCode")]
        public string CountryCode { get; set; }
        public Country Country { get; set; }

        public Site() {}

        public Site(string siteId, string siteName, string countryCode)
        {
            SiteId = siteId;
            SiteName = siteName;
            CountryCode = countryCode;
        }
    }
}
