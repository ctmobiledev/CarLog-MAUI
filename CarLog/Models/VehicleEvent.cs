﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLog.Models
{
    public class VehicleEvent
    {

        public Guid MaintEventId { get; set; }
        
        public Guid VID { get; set; }

        public DateTime MaintEventTimestamp { get; set; }

        public Double MaintEventMileage { get; set; }

        public String MaintEventName { get; set; }

    }
}
