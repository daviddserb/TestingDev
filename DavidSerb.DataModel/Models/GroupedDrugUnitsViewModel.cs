using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class GroupedDrugUnitsViewModel
    {
        public Dictionary<string, List<DrugUnit>> DrugUnitsDict { get; set; }
    }
}
