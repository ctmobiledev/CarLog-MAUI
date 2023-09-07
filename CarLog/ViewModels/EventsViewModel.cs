using CarLog.Models;
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
    public class EventsViewModel : INotifyPropertyChanged
    {

        public ICommand AddCommand { private set; get; }

        public ICommand SelectCommand { private set; get; }


        public Vehicle _vehicle {  get; set; }

        public String VehicleName { get; set; }


        public VehicleEvent SelectedEvent { get; set; }          // selection from list "lands" here; can be processed as needed


        public EventsViewModel() { 
        
        }

        public EventsViewModel(Vehicle vehicle, CollectionView cview) {
            _vehicle = vehicle;

            VehicleName = _vehicle.VehicleYear.ToString() 
                + " " + _vehicle.VehicleMake
                + " " + _vehicle.VehicleModel;

            AddCommand = new Command(() => {
                Debug.WriteLine(">>> AddCommand fired");
                VehicleEvent newEvent = new VehicleEvent();
                Application.Current.MainPage.Navigation.PushAsync(new EditVehicleEventPage(_vehicle, newEvent));
            });

            SelectCommand = new Command<Object>(async (Object e) => {
                if (SelectedEvent != null)
                {
                    Debug.WriteLine(">>> SelectCommand fired, item is: " + SelectedEvent.MaintEventName);

                    // No menu needed; just one action, editing
                    await Application.Current.MainPage.Navigation.PushAsync(new EditVehicleEventPage(_vehicle, SelectedEvent));

                    /***
                    // Menu to appear here: Edit/View Children/Delete
                    var TapAction = await Application.Current.MainPage.DisplayActionSheet("Pick An Action",
                        "Cancel", null, "Edit Event");                          // no child data below this
                    Debug.WriteLine(">>> TapAction: " + TapAction);

                    switch (TapAction.ToString())
                    {
                        case "Edit Event":
                            await Application.Current.MainPage.Navigation.PushAsync(new EditVehicleEventPage(_vehicle, SelectedEvent));
                            break;
                        default:
                            break;
                    }
                                         //
                    // Turn off the selection (for Android, mainly) - but after the selection has been processed!
                    //
                    IDispatcherTimer DeselectionTimer;
                    DeselectionTimer = Application.Current.Dispatcher.CreateTimer();
                    DeselectionTimer.Interval = TimeSpan.FromMilliseconds(250);
                    DeselectionTimer.Tick += (sender, e) => {
                        cview.SelectedItem = null;
                        DeselectionTimer.Stop();
                    };
                    DeselectionTimer.Start();
                    ***/

                }
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
