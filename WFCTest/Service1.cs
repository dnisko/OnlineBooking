using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WFCTest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class Service1 : IOBService
    {
        //public AddressFilterMode AddressFilterMode { get; set; }

        private onlinebookingDBContext db = new onlinebookingDBContext();

        public IQueryable<nastan> GetNastans()
        {
            return db.nastans;
        }

        public nastan GetNastan(string Id)
        {
            int NastanId = Convert.ToInt32(Id);
            nastan c = db.nastans.Find(NastanId);
            return c;
        }
    }
}
