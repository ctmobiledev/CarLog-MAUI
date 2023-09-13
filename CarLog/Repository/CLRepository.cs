using CarLog.Models;
using CarLog.ModelsRealm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLog.Repository
{
    public class CLRepository
    {

        public static List<Vehicle> Vehicles = new List<Vehicle>();

        public static void PrintAllVehicles()
        {

            using var realm = RealmService.GetRealm();

            var VehiclesRealm = realm.All<VehicleRealm>();

            Debug.WriteLine(">>> PrintAllVehicles - Count = " + VehiclesRealm.Count());
            Debug.WriteLine(">>> [");
            foreach (var veh in VehiclesRealm)
            {
                Debug.WriteLine(">>>    " + veh.VehicleYear + " " + veh.VehicleMake + " " + veh.VehicleModel);
            }
            Debug.WriteLine(">>> ]");

        }

        public static void PrintAllMaintEvents(VehicleRealm veh)
        {

            using var realm = RealmService.GetRealm();

            var foundVehicleRealm = realm.All<VehicleRealm>().Where(x => x.VID == veh.VID).FirstOrDefault(); 

            if (foundVehicleRealm != null)
            {
                Debug.WriteLine(">>> PrintAllMaintEvents - VehicleRealm = " + veh.VehicleYear + " " + veh.VehicleMake + " " + veh.VehicleModel);
                Debug.WriteLine(">>> PrintAllMaintEvents - Count = " + foundVehicleRealm.VehicleEvents.Count());
                Debug.WriteLine(">>> [");
                foreach (var evt in foundVehicleRealm.VehicleEvents)
                {
                    Debug.WriteLine(">>>    MaintEventId = " + evt.MaintEventId + ", MaintEventName = " + evt.MaintEventName);
                }
                Debug.WriteLine(">>> ]");
            }
            else
            {
                Debug.WriteLine(">>> No RealmObject found for VID = " + veh.VID);
            }

        }


        public static void LoadAllVehicles()
        {

            using var realm = RealmService.GetRealm();

            var VehiclesRealm = realm.All<VehicleRealm>();

            Debug.WriteLine(">>> LoadAllVehicles - Count = " + VehiclesRealm.Count());
            Debug.WriteLine(">>> [");
            foreach (var veh in VehiclesRealm)
            {
                Debug.WriteLine(">>>    " + veh.VehicleYear + " " + veh.VehicleMake + " " + veh.VehicleModel);

                // "Vehicles" repository object is still bound to the XAML UI; would need to change ItemsSource
                // to break the link for good

                var eventsToMove = new List<VehicleEvent>();
                foreach (var evt in veh.VehicleEvents)
                {
                    eventsToMove.Add(new VehicleEvent
                    {
                        MaintEventId = evt.MaintEventId,
                        VID = evt.VID,
                        MaintEventName = evt.MaintEventName,
                        MaintEventMileage = evt.MaintEventMileage,
                        MaintEventTimestamp = DateTime.Parse(evt.MaintEventTimestamp),
                        Cost = evt.Cost,
                        Location = evt.Location,
                        Remarks = evt.Remarks,
                        Servicer = evt.Servicer,
                    });

                    Debug.WriteLine(">>>    -- event: " + evt.MaintEventId + " " + evt.MaintEventName);
                }

                Vehicles.Add(new Vehicle
                {
                    VID = veh.VID,
                    VehicleYear = veh.VehicleYear,
                    VehicleMake = veh.VehicleMake,
                    VehicleModel = veh.VehicleModel,
                    VehicleColor = veh.VehicleColor,
                    VehicleMileage = veh.VehicleMileage,
                    LicensePlateTag = veh.LicensePlateTag,
                    LicensePlateState = veh.LicensePlateState,
                    LicensePlateExpiry = veh.LicensePlateExpiry,
                    VehicleEvents = eventsToMove
                });
            }
            Debug.WriteLine(">>> ]");

        }

        // No need for a separate LoadAllMaintEvents; see the routine above; events are loaded with the veh object.

    }
}
