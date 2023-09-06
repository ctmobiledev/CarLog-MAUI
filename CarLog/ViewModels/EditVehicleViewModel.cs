using CarLog.Models;
using CarLog.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarLog.ViewModels
{
    public class EditVehicleViewModel : INotifyPropertyChanged
    {

        /**
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
        **/

        private Vehicle _CurrentVehicle { get; set; }

        public String ActionPrompt { get; set; }

        public String VehicleYearEntry { get; set; }

        public String VehicleMakeEntry { get; set; }

        public String VehicleModelEntry { get; set; }


        public ICommand SaveCommand { private set; get; }               // Shared use by EditVehiclePage

        public ICommand CancelCommand { private set; get; }


        public EditVehicleViewModel(Vehicle CurrentVehicle)
        {

            _CurrentVehicle = CurrentVehicle;
            Debug.WriteLine(">>> EditVehicleViewModel fired, _CurrentVehicle = " +
                _CurrentVehicle.VehicleMake + " " + _CurrentVehicle.VehicleModel);

            if (_CurrentVehicle.VehicleMake == null)
            {
                ActionPrompt = "Add a vehicle below.";
            }
            else
            {
                ActionPrompt = "Edit the vehicle below.";

                VehicleYearEntry = _CurrentVehicle.VehicleYear.ToString();
                VehicleMakeEntry = _CurrentVehicle.VehicleMake;
                VehicleModelEntry = _CurrentVehicle.VehicleModel;
            }


            SaveCommand = new Command(() =>
            {
                Debug.WriteLine(">>> SaveCommand fired");

                Debug.WriteLine(">>> VehicleYearEntry = " + VehicleYearEntry);
                Debug.WriteLine(">>> VehicleMakeEntry = " + VehicleMakeEntry);
                Debug.WriteLine(">>> VehicleModelEntry = " + VehicleModelEntry);

                if (_CurrentVehicle.VehicleMake == null)
                {
                    CLRepository.Vehicles.Add(new Vehicle
                    {
                        VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                        VehicleYear = int.Parse(VehicleYearEntry),                  // will need to validate
                        VehicleMake = VehicleMakeEntry,
                        VehicleModel = VehicleModelEntry,
                        VehicleColor = "Splash",
                        VehicleMileage = 175000,                                    // change to string for entry
                        LicensePlateTag = "WIN123",
                        LicensePlateState = "TX",
                        LicensePlateExpiry = "2023-12-31"
                    });
                }
                else
                {
                    var foundVehicle = CLRepository.Vehicles.Where(x => x.VID == _CurrentVehicle.VID).FirstOrDefault();

                    foundVehicle.VehicleYear = int.Parse(VehicleYearEntry);
                    foundVehicle.VehicleMake = VehicleMakeEntry;
                    foundVehicle.VehicleModel = VehicleModelEntry;
                    foundVehicle.VehicleColor = "Splash";
                    foundVehicle.VehicleMileage = 175000;
                    foundVehicle.LicensePlateTag = "WIN123";
                    foundVehicle.LicensePlateState = "TX";
                    foundVehicle.LicensePlateExpiry = "2023-12-31";
                }

                Application.Current.MainPage.Navigation.PopAsync();
            });

            CancelCommand = new Command(() =>
            {
                Debug.WriteLine(">>> CancelCommand fired");
                Application.Current.MainPage.Navigation.PopAsync();
            });
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            if (PropertyChanged != null)
            {
                Debug.WriteLine(">>> propertyname = " + propertyname);
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

    }
}
