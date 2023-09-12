using CarLog.Models;
using CarLog.ModelsRealm;
using CarLog.Repository;
using Realms;
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

        private readonly Realm realm;

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

            realm = RealmService.GetRealm();

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

                if (_CurrentVehicle.VehicleMileage != 0)
                {
                    VehicleMileageEntry = _CurrentVehicle.VehicleMileage.ToString();
                }
                else
                {
                    VehicleMileageEntry = String.Empty;
                }

                LicensePlateTagEntry = _CurrentVehicle.LicensePlateTag;
                LicensePlateStateEntry = _CurrentVehicle.LicensePlateState;
                LicensePlateExpiryEntry = _CurrentVehicle.LicensePlateExpiry;

                DeleteButtonVisible = true;
            }


            SaveCommand = new Command(() =>
            {
                int intVehicleYear = 0;
                double dblVehicleMileage = 0.0;

                Debug.WriteLine(">>> SaveCommand fired");

                Debug.WriteLine(">>> VehicleYearEntry = " + VehicleYearEntry);
                Debug.WriteLine(">>> VehicleMakeEntry = " + VehicleMakeEntry);
                Debug.WriteLine(">>> VehicleModelEntry = " + VehicleModelEntry);

                if (AllInputsValid() == false)
                {
                    return;
                }

                var yearOk = int.TryParse(VehicleYearEntry, out intVehicleYear);
                if (!yearOk) 
                {
                    VehicleYearEntry = "0";
                    intVehicleYear = 0; 
                }

                var mileageOk = double.TryParse(VehicleMileageEntry, out dblVehicleMileage);
                if (!mileageOk) 
                {
                    VehicleMileageEntry = String.Empty;
                    dblVehicleMileage = 0;
                }

                if (_CurrentVehicle.VehicleMake == null)
                {
                    var addedVehicleGuid = Guid.NewGuid();

                    CLRepository.Vehicles.Add(new Vehicle
                    {
                        VID = addedVehicleGuid,
                        VehicleYear = intVehicleYear,                  // will need to validate
                        VehicleMake = VehicleMakeEntry.ToString().Trim(),
                        VehicleModel = VehicleModelEntry.ToString().Trim(),
                        VehicleColor = VehicleColorEntry.ToString().Trim(),
                        VehicleMileage = dblVehicleMileage,
                        LicensePlateTag = LicensePlateTagEntry.ToString().Trim(),
                        LicensePlateState = LicensePlateStateEntry.ToString().Trim(),
                        LicensePlateExpiry = LicensePlateExpiryEntry.ToString().Trim()
                    });

                    realm.Write(() =>
                    {
                        realm.Add<VehicleRealm>(new VehicleRealm
                        {
                            VID = addedVehicleGuid,
                            VehicleYear = intVehicleYear,
                            VehicleMake = VehicleMakeEntry.ToString().Trim(),
                            VehicleModel = VehicleModelEntry.ToString().Trim(),
                            VehicleColor = VehicleColorEntry.ToString().Trim(),
                            VehicleMileage = dblVehicleMileage,
                            LicensePlateTag = LicensePlateTagEntry.ToString().Trim(),
                            LicensePlateState = LicensePlateStateEntry.ToString().Trim(),
                            LicensePlateExpiry = LicensePlateExpiryEntry.ToString().Trim()
                        });

                        Debug.WriteLine(">>> Realm new vehicle added: " + VehicleMakeEntry);
                    });

                }
                else
                {
                    var foundVehicle = CLRepository.Vehicles.Where(x => x.VID == _CurrentVehicle.VID).FirstOrDefault();

                    foundVehicle.VID = _CurrentVehicle.VID;
                    foundVehicle.VehicleYear = intVehicleYear;
                    foundVehicle.VehicleMake = VehicleMakeEntry.Trim();
                    foundVehicle.VehicleModel = VehicleModelEntry.Trim();
                    foundVehicle.VehicleColor = VehicleColorEntry.Trim();
                    foundVehicle.VehicleMileage = dblVehicleMileage;
                    foundVehicle.LicensePlateTag = LicensePlateTagEntry.Trim();
                    foundVehicle.LicensePlateState = LicensePlateStateEntry.Trim();
                    foundVehicle.LicensePlateExpiry = LicensePlateExpiryEntry.Trim();


                    realm.Write(() =>
                    {
                        var foundVehicleRealm = realm.All<VehicleRealm>().Where(x => x.VID == _CurrentVehicle.VID).FirstOrDefault(); ;

                        foundVehicleRealm.VID = _CurrentVehicle.VID;
                        foundVehicleRealm.VehicleYear = intVehicleYear;
                        foundVehicleRealm.VehicleMake = VehicleMakeEntry.Trim();
                        foundVehicleRealm.VehicleModel = VehicleModelEntry.Trim();
                        foundVehicleRealm.VehicleColor = VehicleColorEntry.Trim();
                        foundVehicleRealm.VehicleMileage = dblVehicleMileage;
                        foundVehicleRealm.LicensePlateTag = LicensePlateTagEntry.Trim();
                        foundVehicleRealm.LicensePlateState = LicensePlateStateEntry.Trim();
                        foundVehicleRealm.LicensePlateExpiry = LicensePlateExpiryEntry.Trim();

                        Debug.WriteLine(">>> Realm existing vehicle updated: " + VehicleMakeEntry);
                    });

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

        private Boolean AllInputsValid()
        {

            if (VehicleYearEntry == String.Empty)
            {
                Application.Current.MainPage.DisplayAlert("Missing Input",
                                "Please enter a year for the vehicle.", "OK");
                return false;
            }

            if (VehicleMakeEntry == String.Empty)
            {
                Application.Current.MainPage.DisplayAlert("Missing Input",
                                "Please enter a make for the vehicle.", "OK");
                return false;
            }

            if (VehicleModelEntry == String.Empty)
            {
                Application.Current.MainPage.DisplayAlert("Missing Input",
                                "Please enter a model for the vehicle.", "OK");
                return false;
            }

            return true;

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
                        Debug.WriteLine(">>> ConfirmDelete - Delete Vehicle: Vehicle did not delete successfully.");
                    }
                }


                var foundVehicleRealm = realm.All<VehicleRealm>().Where(x => x.VID == _CurrentVehicle.VID).FirstOrDefault(); ;

                if (foundVehicleRealm != null)
                {
                    try
                    {
                        realm.Write(() =>
                        {
                            realm.Remove(foundVehicleRealm);            // returns a void, so we have to check this way
                        });
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Delete Vehicle",
                            "Vehicle did not delete successfully.", "OK");
                    }

                    Debug.WriteLine(">>> ConfirmDelete - Vehicle deleted successfully: " + _CurrentVehicle.VehicleYear + " " +
                        _CurrentVehicle.VehicleMake + " " + _CurrentVehicle.VehicleModel);
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
