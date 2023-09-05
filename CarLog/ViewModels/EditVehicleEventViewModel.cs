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

        /**
        public Guid EventId { get; set; }
        
        public Guid VID { get; set; }

        public DateTime EventTimestamp { get; set; }

        public Double EventMileage { get; set; }

        public String EventName { get; set; }
        **/

        public static Vehicle _vehicle;


        public String EventTimestampEntry { get; set; }

        public String EventMileageEntry { get; set; }

        public String EventNameEntry { get; set; }


        public ICommand SaveCommand { private set; get; }               // Shared use by EditVehiclePage

        public ICommand CancelCommand { private set; get; }


        public EditVehicleEventViewModel() {

            SaveCommand = new Command(() => {
                Debug.WriteLine(">>> SaveCommand fired");

                Debug.WriteLine(">>> EventTimestampEntry = " + EventTimestampEntry);
                Debug.WriteLine(">>> EventMileageEntry = " + EventMileageEntry);
                Debug.WriteLine(">>> EventNameEntry = " + EventNameEntry);

                _vehicle.VehicleEvents.Add(new VehicleEvent
                {
                    EventId = Guid.NewGuid(),
                    VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                    EventTimestamp = DateTime.Now,
                    EventMileage = int.Parse(EventMileageEntry),
                    EventName = EventNameEntry
                });

                Application.Current.MainPage.Navigation.PopAsync();
            });

            CancelCommand = new Command(() => {
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
