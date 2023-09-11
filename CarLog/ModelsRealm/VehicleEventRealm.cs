using Realms;
using Realms.Schema;
using Realms.Weaving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLog.ModelsRealm
{
    public partial class VehicleEventRealm : IRealmObject
    {

        [PrimaryKey]
        public Guid MaintEventId { get; set; }
        
        public Guid VID { get; set; }

        public String MaintEventTimestamp { get; set; }           // Originally DateTime; Realm doesn't support

        public Double MaintEventMileage { get; set; }

        public String MaintEventName { get; set; }

        public Double Cost { get; set; }

        public String Servicer { get; set; }

        public String Location { get; set; }

        public String Remarks { get; set; }

    }
}
