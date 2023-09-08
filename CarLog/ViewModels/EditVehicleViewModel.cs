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

        public String VehicleYearEntry { get; set; } = String.Empty;

        public String VehicleMakeEntry { get; set; } = String.Empty;

        public String VehicleModelEntry { get; set; } = String.Empty;

        public String VehicleColorEntry { get; set; } = String.Empty;

        public String VehicleMileageEntry { get; set; } = String.Empty;

        public String LicensePlateTagEntry { get; set; } = String.Empty;

        public String LicensePlateStateEntry { get; set; } = String.Empty;

        public String LicensePlateExpiryEntry { get; set; } = String.Empty;


        public ICommand SaveCommand { private set; get; }               // Shared use by EditVehiclePage

        public ICommand CancelCommand { private set; get; }

        public ICommand DeleteCommand { private set; get; }

        public Boolean DeleteButtonVisible { get; set; }


        public EditVehicleViewModel(Vehicle CurrentVehicle)
        {

            _CurrentVehicle = CurrentVehicle;
            Debug.WriteLine(">>> EditVehicleViewModel fired, _CurrentVehicle = " +
                _CurrentVehicle.VehicleMake + " " + _CurrentVehicle.VehicleModel);

            if (_CurrentVehicle.VehicleMake == null)
            {
                ActionPrompt = "Add a vehicle below.";

                DeleteButtonVisible = false;
            }
            else
            {
                ActionPrompt = "Edit the vehicle below.";

                VehicleYearEntry = _CurrentVehicle.VehicleYear.ToString();
                VehicleMakeEntry = _CurrentVehicle.VehicleMake;
                VehicleModelEntry = _CurrentVehicle.VehicleModel;

                VehicleColorEntry = _CurrentVehicle.VehicleColor;
                VehicleMileageEntry = _CurrentVehicle.VehicleMileage.ToString();
                LicensePlateTagEntry = _CurrentVehicle.LicensePlateTag;
                LicensePlateStateEntry = _CurrentVehicle.LicensePlateState;
                LicensePlateExpiryEntry = _CurrentVehicle.LicensePlateExpiry;

                DeleteButtonVisible = true;
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
                        VID = Guid.NewGuid(),
                        VehicleYear = int.Parse(VehicleYearEntry.ToString()),                  // will need to validate
                        VehicleMake = VehicleMakeEntry.ToString(),
                        VehicleModel = VehicleModelEntry.ToString(),
                        VehicleColor = VehicleColorEntry.ToString(),
                        VehicleMileage = double.Parse(VehicleMileageEntry.ToString()),
                        LicensePlateTag = LicensePlateTagEntry.ToString(),
                        LicensePlateState = LicensePlateStateEntry.ToString(),
                        LicensePlateExpiry = LicensePlateExpiryEntry.ToString()
                    });
                }
                else
                {
                    var foundVehicle = CLRepository.Vehicles.Where(x => x.VID == _CurrentVehicle.VID).FirstOrDefault();

                    foundVehicle.VID = _CurrentVehicle.VID;
                    foundVehicle.VehicleYear = int.Parse(VehicleYearEntry);
                    foundVehicle.VehicleMake = VehicleMakeEntry;
                    foundVehicle.VehicleModel = VehicleModelEntry;
                    foundVehicle.VehicleColor = VehicleColorEntry;
                    foundVehicle.VehicleMileage = double.Parse(VehicleMileageEntry);
                    foundVehicle.LicensePlateTag = LicensePlateTagEntry;
                    foundVehicle.LicensePlateState = LicensePlateStateEntry;
                    foundVehicle.LicensePlateExpiry = LicensePlateExpiryEntry;
                }

                Application.Current.MainPage.Navigation.PopAsync();
            });

            CancelCommand = new Command(() =>
            {
                Debug.WriteLine(">>> CancelCommand fired");
                Application.Current.MainPage.Navigation.PopAsync();
            });

            DeleteCommand = new Command(() =>
            {
                Debug.WriteLine(">>> DeleteCommand fired");
                ConfirmDelete();
            });


        }

        private async void ConfirmDelete()
        {

            bool TapConfirm = await Application.Current.MainPage.DisplayAlert("Delete Vehicle",
                "Are you sure you want to delete this vehicle?", "Yes, Delete", "No, Cancel");

            if (TapConfirm)
            {
                var foundVehicle = CLRepository.Vehicles.Where(x => x.VID == _CurrentVehicle.VID).FirstOrDefault();

                if (foundVehicle != null)
                {
                    var deleteOk = CLRepository.Vehicles.Remove(foundVehicle);

                    if (!deleteOk)
                    {
                        // This should never happen 

                        await Application.Current.MainPage.DisplayAlert("Delete Vehicle",
                            "Vehicle did not delete successfully.", "OK");
                    }
                }

                await Application.Current.MainPage.Navigation.PopAsync();
            }

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
