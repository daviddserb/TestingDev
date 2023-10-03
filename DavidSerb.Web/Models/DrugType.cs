using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DavidSerb.Web.Models
{
    public class DrugType
    {
        [Required]
        public string DrugTypeId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MinLength(5, ErrorMessage = "DrugType Name must be at least {1} characters.")]
        public string DrugTypeName { get; set; }

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