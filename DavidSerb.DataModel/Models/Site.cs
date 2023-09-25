using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class Site
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        // ??? CountryCode ar trb. sa fie FK spre Country.Name/Id? After: int/string if Id/Name CountryCode 
        public int CountryCode { get; set; }

        public Site(int siteId, string siteName, int countryCode)
        {
            SiteId = siteId;
            SiteName = siteName;
            CountryCode = countryCode;
        }
    }
}
