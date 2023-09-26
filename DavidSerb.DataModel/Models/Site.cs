using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class Site
    {
        public string SiteId { get; set; }
        public string SiteName { get; set; }

        [ForeignKey("Country")]
        // !!! Daca la mutarea claselor in tabele, se face shadow key, atunci fac rename la CountryCode in CountryId
        public int CountryCode { get; set; }
        public Country Country { get; set; }

        public Site(string siteId, string siteName, int countryCode)
        {
            SiteId = siteId;
            SiteName = siteName;
            CountryCode = countryCode;
        }
    }
}
