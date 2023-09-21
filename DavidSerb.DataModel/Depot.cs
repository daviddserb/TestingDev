﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel
{
    public class Depot
    {
        public string DepotId { get; set; }
        public string DepotName { get; set; }

        // navigation property (reverse)
        public Country Country { get; set; }
        public ICollection<Country> Countries { get; set; }

        public DrugUnit DrugUnit { get; set; }

        public Depot(string depotId, string depotName)
        {
            DepotId = depotId;
            DepotName = depotName;
            // ??? nu stiu daca trb. initializat si Countries, pt. ca, cand se construieste un Depot stim si la ce Countries va livra?
            Countries = new List<Country>();
        }
    }
}