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
    public partial class VehicleRealm : IRealmObject
    {

        [PrimaryKey]
        public Guid VID { get; set; }

        public Int32 VehicleYear { get; set; }

        public String VehicleMake { get; set; }

        public String VehicleModel { get; set; }

        public String VehicleColor { get; set; }

        public Double VehicleMileage { get; set; }

        public String LicensePlateTag { get; set; }

        public String LicensePlateState { get; set; }

        public String LicensePlateExpiry { get; set; }

        public IList<VehicleEventRealm> VehicleEvents { get; }              // Not sure how a 'set' works yet

    }
}
