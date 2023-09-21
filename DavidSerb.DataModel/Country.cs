﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        [ForeignKey("Depot")]
        public int DepotId { get; set; }
        // navigation property
        public Depot Depot { get; set; }

        public Country(int countryId, string countryName)
        {
            CountryId = countryId;
            CountryName = countryName;
            // ??? poate trb. sa adaug si DepotId pt. ca atunci cand cream o Country ar trb. sa cunoastem si de la ce Depot se aprovizioneaza?
        }
    }
}