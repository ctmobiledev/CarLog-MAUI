using CarLog.Models;
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
                        EventId = new Guid("00000001-5abd-465c-ab8c-87d3cca3545c"),
                        EventMileage = 100000,
                        EventName = "Oil Change",
                        EventTimestamp = DateTime.Now
                    },
                    new VehicleEvent
                    {
                        VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                        EventId = new Guid("00000002-5abd-465c-ab8c-87d3cca3545c"),
                        EventMileage = 112000,
                        EventName = "Radiator Flush",
                        EventTimestamp = DateTime.Now
                    },
                    new VehicleEvent
                    {
                        VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                        EventId = new Guid("00000003-5abd-465c-ab8c-87d3cca3545c"),
                        EventMileage = 117000,
                        EventName = "New Tires",
                        EventTimestamp = DateTime.Now
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
                    EventId = new Guid("00000001-5abd-465c-ab8c-87d3cca3545c"),
                    EventMileage = 100000,
                    EventName = "Oil Change",
                    EventTimestamp = DateTime.Now
                });
            });

        }

    }
}
