using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLog.Models
{
    public class Vehicle
    {

        public Guid VID { get; set; }

        public Int32 VehicleYear { get; set; }

        public String VehicleMake { get; set; }

        public String VehicleModel { get; set; }

        public String VehicleColor { get; set; }

        public Double VehicleMileage { get; set; }

        public String LicensePlateTag { get; set; }

        public String LicensePlateState { get; set; }

        public String LicensePlateExpiry { get; set; }

        public List<VehicleEvent> VehicleEvents { get; set; } = new List<VehicleEvent>();

    }
}
