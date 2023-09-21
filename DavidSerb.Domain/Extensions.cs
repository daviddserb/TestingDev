using DavidSerb.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain
{
    public static class Extensions
    {
        public static SystemDataSet systemDataSet = new SystemDataSet();

        public static Dictionary<string, List<DrugUnit>> ToGroupedDrugUnits(this IList<DrugUnit> drugUnits)
        {
            Dictionary<string, List<DrugUnit>> drugUnitsDict = new Dictionary<string, List<DrugUnit>>();

            foreach (DrugUnit drugUnit in systemDataSet.DrugUnits)
            {
                string drugTypeName = drugUnit.DrugType?.DrugTypeName;

                if (drugTypeName != null)
                {
                    if (!drugUnitsDict.ContainsKey(drugTypeName)) drugUnitsDict[drugTypeName] = new List<DrugUnit>();

                    drugUnitsDict[drugTypeName].Add(drugUnit);
                }
            }

            return drugUnitsDict;
        }
    }
}
