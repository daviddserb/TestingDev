using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.DataModel
{
    public class CorrelateData
    {
        // ??? Am nevoie sa pun navigational properties? (eu cred ca nu pt. ca vreau doar sa salvez informatii pt. a le printa, nu am nevoie de clasa asta pt. a face legaturi)

        //Depot Name, Country Name, Drug Type Name, Drug Unit Id and Pick Number.
        public string DepotName { get; set; }

        public string CountryName { get; set; } // ??? Daca un Depot poate sa aiba mai multe Country => aici nu ar trebuie Lists? (eu cred ca nu pt. ca ...)

        public string DrugTypeName { get; set; }

        public string DrugUnitId { get; set; }
        public int PickNumber { get; set; }
    }
}
