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

        // Test Only

        public async static Task LoadDataAsync()
        {

            await Task.Run(() => {

                // to create one
                var testGuid = Guid.NewGuid();
                Debug.WriteLine(">>> testGuid = " + testGuid.ToString());

                CLRepository.Vehicles.Add(new Vehicle
                {
                    VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                    VehicleYear = 2017,
                    VehicleMake = "Chevrolet",
                    VehicleModel = "Spark",
                    VehicleColor = "Splash",
                    VehicleMileage = 175000,
                    LicensePlateTag = "WIN123",
                    LicensePlateState = "TX",
                    LicensePlateExpiry = "2023-12-31",
                    VehicleEvents = new List<VehicleEvent>
                {
                    new VehicleEvent
                    {
                        VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                        MaintEventId = new Guid("00000001-5abd-465c-ab8c-87d3cca3545c"),
                        MaintEventMileage = 100000,
                        MaintEventName = "Oil Change",
                        MaintEventTimestamp = DateTime.Now
                    },
                    new VehicleEvent
                    {
                        VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                        MaintEventId = new Guid("00000002-5abd-465c-ab8c-87d3cca3545c"),
                        MaintEventMileage = 112000,
                        MaintEventName = "Radiator Flush",
                        MaintEventTimestamp = DateTime.Now
                    },
                    new VehicleEvent
                    {
                        VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                        MaintEventId = new Guid("00000003-5abd-465c-ab8c-87d3cca3545c"),
                        MaintEventMileage = 117000,
                        MaintEventName = "New Tires",
                        MaintEventTimestamp = DateTime.Now
                    },
                }
                });

                CLRepository.Vehicles.Add(new Vehicle
                {
                    VID = new Guid("a47b36ee-5abd-465c-ab8c-87d3cca3545c"),
                    VehicleYear = 2024,
                    VehicleMake = "Chevrolet",
                    VehicleModel = "Malibu",
                    VehicleColor = "Gold",
                    VehicleMileage = 28000,
                    LicensePlateTag = "WIN123",
                    LicensePlateState = "TX",
                    LicensePlateExpiry = "2023-12-31"
                });

                CLRepository.Vehicles.Add(new Vehicle
                {
                    VID = new Guid("881b36ee-5abd-465c-ab8c-87d3cca3545c"),
                    VehicleYear = 2023,
                    VehicleMake = "Kia",
                    VehicleModel = "Forte",
                    VehicleColor = "Black",
                    VehicleMileage = 18400,
                    LicensePlateTag = "YES183",
                    LicensePlateState = "TX",
                    LicensePlateExpiry = "2023-12-31"
                });

            });

        }

        public async static Task AddVehicleAsync()
        {

            Debug.WriteLine(">>> AddVehicleAsync fired");

            await Task.Run(() =>
            {
                CLRepository.Vehicles.Add(new Vehicle
                {
                    VID = Guid.NewGuid(),
                    VehicleYear = 2023,
                    VehicleMake = "Ford",
                    VehicleModel = "Fiesta",
                    VehicleColor = "Green",
                    VehicleMileage = 25000,
                    LicensePlateTag = "FRD192",
                    LicensePlateState = "TX",
                    LicensePlateExpiry = "2023-11-30"
                });
            });

        }

        public async static Task AddVehicleEventAsync(Vehicle veh)
        {

            Debug.WriteLine(">>> AddVehicleEventAsync fired");

            await Task.Run(() =>
            {
                veh.VehicleEvents.Add(new VehicleEvent
                {
                    VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                    MaintEventId = new Guid("00000001-5abd-465c-ab8c-87d3cca3545c"),
                    MaintEventMileage = 100000,
                    MaintEventName = "Oil Change",
                    MaintEventTimestamp = DateTime.Now
                });
            });

        }

    }
}
