using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel.Models
{
    public class DrugType
    {
        public string DrugTypeId { get; set; }
        public string DrugTypeName { get; set; }

        //public DrugUnit DrugUnit { get; set; }

        /// <summary>
        /// Add a parameterless constructor because of error when add-migration with EFCore (No suitable constructor found for entity type 'DrugType'. The following constructors had parameters that could not be bound to properties of the entity type: cannot bind 'drupgTypeName' in 'DrugType(int drugTypeId, string drupgTypeName)')
        /// </summary>
        public DrugType() { }

        public DrugType(string drugTypeId, string drupgTypeName)
        {
            DrugTypeId = drugTypeId;
            DrugTypeName = drupgTypeName;
        }
    }
}
