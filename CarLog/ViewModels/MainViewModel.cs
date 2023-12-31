﻿using CarLog.Models;
using CarLog.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarLog.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public ICommand AddCommand { private set; get; }

        public ICommand SelectCommand { private set; get; }

        public ICommand HelpCommand { private set; get; }

        public ICommand AboutCommand { private set; get; }


        public Vehicle SelectedVehicle { get; set; }                    // selection from list "lands" here; can be processed as needed
                                                                               // note: binding mechanism can't see static vars

        // **** This didn't behave with INotifyPropertyChanged, so CLRepository.Vehicles is used instead **** 
        //public ObservableCollection<Vehicle> Vehicles { get; set; }         


        public MainViewModel(CollectionView cview) {

            CLRepository.LoadAllVehicles();

            AddCommand = new Command(() => {
                SelectedVehicle = new Vehicle();
                Debug.WriteLine(">>> AddCommand fired");
                Application.Current.MainPage.Navigation.PushAsync(new EditVehiclePage(SelectedVehicle));
            });

            SelectCommand = new Command<Object>(async (Object e) => {
                if (SelectedVehicle != null)
                {
                    Debug.WriteLine(">>> SelectCommand fired, item is: " + SelectedVehicle.VehicleMake + " " + SelectedVehicle.VehicleModel);

                    // Menu to appear here: Edit/View Children/Delete
                    var TapAction = await Application.Current.MainPage.DisplayActionSheet("Vehicle Menu", 
                        "Cancel", null, "View Events", "Edit Vehicle");
                    Debug.WriteLine(">>> TapAction: " + TapAction);

                    switch(TapAction.ToString())
                    {
                        case "View Events":
                            await Application.Current.MainPage.Navigation.PushAsync(new EventsPage(SelectedVehicle));
                            break;
                        case "Edit Vehicle":
                            await Application.Current.MainPage.Navigation.PushAsync(new EditVehiclePage(SelectedVehicle));
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

                }
            });

            HelpCommand = new Command(() => {
                Debug.WriteLine(">>> HelpCommand fired");
                Application.Current.MainPage.Navigation.PushAsync(new HelpPage());
            });

            AboutCommand = new Command(() => {
                Debug.WriteLine(">>> AboutCommand fired");
                Application.Current.MainPage.Navigation.PushAsync(new AboutPage());
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
