using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLog.Models
{
    public class VehicleEvent
    {

        public Guid EventId { get; set; }
        
        public Guid VID { get; set; }

        public DateTime EventTimestamp { get; set; }

        public Double EventMileage { get; set; }

        public String EventName { get; set; }

    }
}
