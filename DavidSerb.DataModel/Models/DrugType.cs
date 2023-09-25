using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class DrugType
    {
        public int DrugTypeId { get; set; }
        public string DrugTypeName { get; set; }

        public DrugUnit DrugUnit { get; set; }

        public DrugType(int drugTypeId, string drupgTypeName)
        {
            DrugTypeId = drugTypeId;
            DrugTypeName = drupgTypeName;
        }
    }
}
