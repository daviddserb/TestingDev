using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DavidSerb.Web.Models
{
    public class Site
    {
        public string SiteId { get; set; }
        public string SiteName { get; set; }

        [ForeignKey("Country")]
        [Column("CountryCode")]
        public string CountryCode { get; set; }
        public Country Country { get; set; }

        public Site(string siteId, string siteName, string countryCode)
        {
            SiteId = siteId;
            SiteName = siteName;
            CountryCode = countryCode;
        }
    }
}