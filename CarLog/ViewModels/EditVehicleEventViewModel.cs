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
    public class EditVehicleEventViewModel : INotifyPropertyChanged
    {

        public static Vehicle _vehicle;

        public static VehicleEvent _CurrentEvent { get; set; }

        public String ActionPrompt { get; set; }

        public String MaintEventTimestampEntry { get; set; }

        public String MaintEventMileageEntry { get; set; }

        public String MaintEventNameEntry { get; set; }


        public ICommand SaveCommand { private set; get; }               // Shared use by EditVehiclePage

        public ICommand CancelCommand { private set; get; }

        public ICommand DeleteCommand { private set; get; }

        public Boolean DeleteButtonVisible { get; set; }


        public EditVehicleEventViewModel(VehicleEvent CurrentEvent) {

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

                DeleteButtonVisible = true;
            }

            SaveCommand = new Command(() => {
                Debug.WriteLine(">>> SaveCommand fired");

                Debug.WriteLine(">>> MaintEventTimestampEntry = " + MaintEventTimestampEntry);
                Debug.WriteLine(">>> MaintEventMileageEntry = " + MaintEventMileageEntry);
                Debug.WriteLine(">>> MaintEventNameEntry = " + MaintEventNameEntry);

                if (_CurrentEvent.MaintEventName == null)
                {
                    _vehicle.VehicleEvents.Add(new VehicleEvent
                    {
                        MaintEventId = Guid.NewGuid(),
                        VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                        MaintEventTimestamp = DateTime.Parse(MaintEventTimestampEntry),
                        MaintEventMileage = int.Parse(MaintEventMileageEntry),
                        MaintEventName = MaintEventNameEntry
                    });
                }
                else
                {
                    var foundEvent = _vehicle.VehicleEvents.Where(x => x.MaintEventId == _CurrentEvent.MaintEventId).FirstOrDefault();

                    foundEvent.MaintEventMileage = int.Parse(MaintEventMileageEntry);
                    foundEvent.MaintEventName = MaintEventNameEntry;
                }

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
