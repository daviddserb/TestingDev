using DavidSerb.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain
{
    interface ISiteDistributionService
    {
        IEnumerable<DrugUnit> GetRequestedDrugUnits(string siteId, string drugCode, int quantity);
    }
}
