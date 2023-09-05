using CarLog.Models;
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


        // **** This didn't behave with INotifyPropertyChanged, so CLRepository.Vehicles is used instead **** 
        //public ObservableCollection<Vehicle> Vehicles { get; set; }         


        public MainViewModel() {

            AddCommand = new Command(() => {
                Debug.WriteLine(">>> AddCommand fired");
                Application.Current.MainPage.Navigation.PushAsync(new EditVehiclePage());
            });

            SelectCommand = new Command<Object>((Object e) => {
                if (SelectedVehicle != null)
                {
                    Debug.WriteLine(">>> SelectCommand fired, item is: " + SelectedVehicle.VehicleMake + " " + SelectedVehicle.VehicleModel);
                    Application.Current.MainPage.Navigation.PushAsync(new EventsPage(SelectedVehicle));
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

        private async void LoadDataFromRepository()
        {

            /////[TEMP] await CLRepository.LoadDataAsync();

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
