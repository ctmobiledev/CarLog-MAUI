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

        public static void LoadData()
        {

            // to create one
            var testGuid = Guid.NewGuid();
            Debug.WriteLine(">>> testGuid = " + testGuid.ToString());

            CLRepository.Vehicles.Add(new Vehicle
            {
                VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                VehicleYear = 2017,
                VehicleMaker = "Chevrolet",
                VehicleModel = "Spark",
                VehicleColor = "Splash",
                VehicleMileage = 175000,
                LicensePlateTag = "WIN123",
                LicensePlateState = "TX",
                LicensePlateExpiry = "2023-12-31"
            });
            CLRepository.Vehicles.Add(new Vehicle
            {
                VID = new Guid("a47b36ee-5abd-465c-ab8c-87d3cca3545c"),
                VehicleYear = 2024,
                VehicleMaker = "Chevrolet",
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
                VehicleMaker = "Kia",
                VehicleModel = "Forte",
                VehicleColor = "Black",
                VehicleMileage = 18400,
                LicensePlateTag = "YES183",
                LicensePlateState = "TX",
                LicensePlateExpiry = "2023-12-31"
            });
        }
    }
}
