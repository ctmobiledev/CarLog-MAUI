using CarLog.Models;
using CarLog.Repository;
using CarLog.ViewModels;
using System.Diagnostics;


namespace CarLog
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }


        // A bug with the ObservableCollection not updating from NotifyPropertyChanged led to this workaround

        protected override void OnAppearing()
        {
            base.OnAppearing();

            lblEmptyMsg.IsVisible = (CLRepository.Vehicles.Count == 0);

            cvVehicles.ItemsSource = null;
            cvVehicles.ItemsSource = CLRepository.Vehicles;

            cvVehicles.SelectedItem = null;
        }

    }

}