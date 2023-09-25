using DavidSerb.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain
{
    public class SiteDistributionService : ISiteDistributionService
    {
        public IEnumerable<DrugUnit> GetRequestedDrugUnits(string siteId, string drugCode, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
