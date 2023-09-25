using DavidSerb.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain
{
    public static class Extensions
    {
        /// <summary>
        /// Add extension method to object of IList<DrugUnit> data type
        /// </summary>
        /// <param name="drugUnits"></param>
        /// <returns>A dictionary of all DrugUnits grouped by their associated DrugTypeName</returns>
        public static Dictionary<string, List<DrugUnit>> ToGroupedDrugUnits(this IList<DrugUnit> drugUnits)
        {
            // Old
            //Dictionary<string, List<DrugUnit>> drugUnitsDict = new Dictionary<string, List<DrugUnit>>();

            //foreach (DrugUnit drugUnit in drugUnits)
            //{
            //    string drugTypeName = drugUnit.DrugType?.DrugTypeName;

            //    if (drugTypeName != null)
            //    {
            //        if (!drugUnitsDict.ContainsKey(drugTypeName)) drugUnitsDict[drugTypeName] = new List<DrugUnit>();

            //        drugUnitsDict[drugTypeName].Add(drugUnit);
            //    }
            //}

            // New
            var drugUnitsDict = drugUnits
                .Where(drugUnit => drugUnit.DrugType?.DrugTypeName != null)
                .GroupBy(drugUnit => drugUnit.DrugType.DrugTypeName)
                .ToDictionary(
                    drugUnit => drugUnit.Key, // Key = the property used in the GroupBy (DrugTypeName)
                    values => values.ToList() // List of DrugTypes associated to the specific DrugUnit
                );

            return drugUnitsDict;
        }
    }
}
