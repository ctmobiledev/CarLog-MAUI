using CarLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLog.ViewModels
{
    public class EventsViewModel
    {

        public Vehicle _vehicle {  get; set; }

        public String VehicleName { get; set; } 

        public EventsViewModel() { }

        public EventsViewModel(Vehicle vehicle) {
            _vehicle = vehicle;

            VehicleName = _vehicle.VehicleYear.ToString() + " " + _vehicle.VehicleModel;
        }

    }
}
