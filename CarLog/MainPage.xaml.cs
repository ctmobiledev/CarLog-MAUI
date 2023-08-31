using CarLog.Models;
using CarLog.Repository;
using System.Diagnostics;


namespace CarLog
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            CLRepository.LoadData();
            RefreshData();

            BindingContext = CLRepository.Vehicles;
        }

        private void RefreshData()
        {
            cvVehicles.ItemsSource = null;          
            cvVehicles.ItemsSource = CLRepository.Vehicles;
        }

        private void cvVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Vehicle v = (Vehicle)e.CurrentSelection.FirstOrDefault();

            DisplayAlert("Test", v.VehicleYear + " " + v.VehicleMaker + " " +
                v.VehicleModel, "OK");

        }

    }

}