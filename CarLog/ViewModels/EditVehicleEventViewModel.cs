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
    public class EditVehicleEventViewModel : INotifyPropertyChanged
    {

        private readonly Realm realm;

        public static Vehicle _vehicle;     // set from outside, in MainViewModel, SelectCommand

        public static VehicleEvent _CurrentEvent { get; set; }

        public String ActionPrompt { get; set; }

        public String MaintEventTimestampEntry { get; set; } = String.Empty;

        public String MaintEventMileageEntry { get; set; } = String.Empty;

        public String MaintEventNameEntry { get; set; } = String.Empty;

        public String CostEntry { get; set; } = String.Empty;

        public String ServicerEntry { get; set; } = String.Empty;

        public String LocationEntry { get; set; } = String.Empty;

        public String RemarksEntry { get; set; } = String.Empty;


        public ICommand SaveCommand { private set; get; }               // Shared use by EditVehiclePage

        public ICommand CancelCommand { private set; get; }

        public ICommand DeleteCommand { private set; get; }

        public Boolean DeleteButtonVisible { get; set; }


        public EditVehicleEventViewModel(VehicleEvent CurrentEvent) {

            realm = RealmService.GetRealm();

            _CurrentEvent = CurrentEvent;

            Debug.WriteLine(">>> EditVehicleEventViewModel fired, _CurrentEvent = " +
                _CurrentEvent.MaintEventMileage.ToString() + " " + _CurrentEvent.MaintEventName);

            if (_CurrentEvent.MaintEventName == null)
            {
                ActionPrompt = "Add an event below.";

                MaintEventTimestampEntry = DateTime.Now.ToString();

                DeleteButtonVisible = false;
            }
            else
            {
                ActionPrompt = "Edit the event below.";

                MaintEventTimestampEntry = _CurrentEvent.MaintEventTimestamp.ToString();
                MaintEventNameEntry = _CurrentEvent.MaintEventName;
                MaintEventMileageEntry = _CurrentEvent.MaintEventMileage.ToString();
                CostEntry = _CurrentEvent.Cost.ToString("########0.00");
                ServicerEntry = _CurrentEvent.Servicer;
                LocationEntry = _CurrentEvent.Location;
                RemarksEntry = _CurrentEvent.Remarks;

                DeleteButtonVisible = true;
            }

            SaveCommand = new Command(() => {
                Debug.WriteLine(">>> SaveCommand fired");

                DateTime dateTimestamp;
                int intMileage;
                double dblCost;

                Debug.WriteLine(">>> MaintEventTimestampEntry = " + MaintEventTimestampEntry);
                Debug.WriteLine(">>> MaintEventMileageEntry = " + MaintEventMileageEntry);
                Debug.WriteLine(">>> MaintEventNameEntry = " + MaintEventNameEntry);

                if (AllInputsValid() == false)
                {
                    return;
                }

                var timestampOk = DateTime.TryParse(MaintEventTimestampEntry, out dateTimestamp);
                if (!timestampOk)
                {
                    MaintEventTimestampEntry = DateTime.Now.ToString();
                    dateTimestamp = DateTime.Now;
                }

                var mileageOk = int.TryParse(MaintEventMileageEntry, out intMileage);
                if (!mileageOk)
                {
                    MaintEventMileageEntry = "0";
                    intMileage = 0;
                }

                var costOk = double.TryParse(CostEntry, out dblCost);
                if (!costOk)
                {
                    CostEntry = String.Empty;
                    dblCost = 0;
                }


                if (_CurrentEvent.MaintEventName == null)
                {
                    _vehicle.VehicleEvents.Add(new VehicleEvent
                    {
                        MaintEventId = Guid.NewGuid(),
                        VID = _vehicle.VID,      
                        MaintEventTimestamp = dateTimestamp,
                        MaintEventMileage = intMileage,
                        MaintEventName = MaintEventNameEntry.ToString().Trim(),
                        Cost = dblCost,
                        Servicer = ServicerEntry.ToString().Trim(),
                        Location = LocationEntry.ToString().Trim(),
                        Remarks = RemarksEntry.ToString().Trim()
                    });

                    realm.Write(() =>
                    {
                        // Just update/overwrite the vehicle since we already have it
                        var foundVehicleRealm = realm.All<VehicleRealm>().Where(x => x.VID == _vehicle.VID).FirstOrDefault(); ;

                        foundVehicleRealm.VehicleEvents.Add(new VehicleEventRealm
                        {
                            MaintEventId = Guid.NewGuid(),
                            VID = _vehicle.VID,      
                            MaintEventTimestamp = dateTimestamp.ToString(),             // Realm does not support DateTime yet, so we'll use a String for now
                            MaintEventMileage = intMileage,
                            MaintEventName = MaintEventNameEntry.ToString().Trim(),
                            Cost = dblCost,
                            Servicer = ServicerEntry.ToString().Trim(),
                            Location = LocationEntry.ToString().Trim(),
                            Remarks = RemarksEntry.ToString().Trim()
                        });

                        CLRepository.PrintAllMaintEvents(foundVehicleRealm);

                        Debug.WriteLine(">>> Realm new vehicle event added: " + MaintEventNameEntry);
                    });
                }
                else
                {

                    // _vehicle is CLRepository's object which the UI is bound to (for now)
                    var foundEvent = _vehicle.VehicleEvents.Where(x => x.MaintEventId == _CurrentEvent.MaintEventId).FirstOrDefault();      

                    foundEvent.MaintEventId = _CurrentEvent.MaintEventId;
                    foundEvent.VID = _CurrentEvent.VID;
                    foundEvent.MaintEventTimestamp = dateTimestamp;           
                    foundEvent.MaintEventMileage = intMileage;
                    foundEvent.MaintEventName = MaintEventNameEntry.Trim();
                    foundEvent.Cost = dblCost;
                    foundEvent.Servicer = ServicerEntry.Trim();
                    foundEvent.Location = LocationEntry.Trim();
                    foundEvent.Remarks = RemarksEntry.Trim();


                    // Just update/overwrite the vehicle since we already have it
                    var foundVehicleRealm = realm.All<VehicleRealm>().Where(x => x.VID == _vehicle.VID).FirstOrDefault();

                    var foundEventRealm = foundVehicleRealm.VehicleEvents.Where(x => x.MaintEventId == _CurrentEvent.MaintEventId).FirstOrDefault();

                    realm.Write(() =>
                    {
                        foundEventRealm.MaintEventId = _CurrentEvent.MaintEventId;
                        foundEventRealm.VID = _CurrentEvent.VID;
                        foundEventRealm.MaintEventTimestamp = dateTimestamp.ToString();          // Realm doesn't support DateTime yet, so using a String
                        foundEventRealm.MaintEventMileage = intMileage;
                        foundEventRealm.MaintEventName = MaintEventNameEntry.Trim();
                        foundEventRealm.Cost = dblCost;
                        foundEventRealm.Servicer = ServicerEntry.Trim();
                        foundEventRealm.Location = LocationEntry.Trim();
                        foundEventRealm.Remarks = RemarksEntry.Trim();

                        Debug.WriteLine(">>> Realm existing vehicle event updated: " + MaintEventNameEntry);
                    });

                    CLRepository.PrintAllMaintEvents(foundVehicleRealm);

                }

                // Update Realm here; we have the _vehicle which was passed in

                Application.Current.MainPage.Navigation.PopAsync();
            });

            CancelCommand = new Command(() => {
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

            if (MaintEventMileageEntry == String.Empty)
            {
                Application.Current.MainPage.DisplayAlert("Missing Input",
                                "Please enter the mileage for the event.", "OK");
                return false;
            }

            if (MaintEventNameEntry == String.Empty)
            {
                Application.Current.MainPage.DisplayAlert("Missing Input",
                                "Please enter a name for the event.", "OK");
                return false;
            }

            return true;

        }

        private async void ConfirmDelete()
        {

            bool TapConfirm = await Application.Current.MainPage.DisplayAlert("Delete Event",
                "Are you sure you want to delete this event?", "Yes, Delete", "No, Cancel");

            if (TapConfirm)
            {

                var foundEvent = _vehicle.VehicleEvents.Where(x => x.MaintEventId == _CurrentEvent.MaintEventId).FirstOrDefault();

                if (foundEvent != null)
                {
                    var deleteOk = _vehicle.VehicleEvents.Remove(foundEvent);

                    if (!deleteOk)
                    {
                        // This should never happen 

                        await Application.Current.MainPage.DisplayAlert("Delete Event",
                            "Event did not delete successfully.", "OK");
                    }
                }


                // Update/overwrite the events list

                var foundVehicleRealm = realm.All<VehicleRealm>().Where(x => x.VID == _vehicle.VID).FirstOrDefault(); ;
                var foundEventRealm = foundVehicleRealm.VehicleEvents.Where(x => x.MaintEventId == _CurrentEvent.MaintEventId).FirstOrDefault();

                if (foundVehicleRealm != null)
                {
                    try
                    {
                        realm.Write(() =>
                        {
                            foundVehicleRealm.VehicleEvents.Remove(foundEventRealm);
                        });
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Delete Event",
                            "Event did not delete successfully.", "OK");
                    }

                    Debug.WriteLine(">>> ConfirmDelete - Event deleted successfully: " + _CurrentEvent.MaintEventId + " " +
                        _CurrentEvent.MaintEventName);
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
