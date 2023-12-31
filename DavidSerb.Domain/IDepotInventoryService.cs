﻿using DavidSerb.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain
{
    interface IDepotInventoryService
    {
        void AssociateDrugs(List<DrugUnit> drugUnits, string depotId, int startPickNumber, int endPickNumber);

        void DisassociateDrugs(List<DrugUnit> drugUnits, int startPickNumber, int endPickNumber);
    }
}
