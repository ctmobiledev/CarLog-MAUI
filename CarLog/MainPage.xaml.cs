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

        private async void cvVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Vehicle v = (Vehicle)e.CurrentSelection.FirstOrDefault();

            if (v != null)
            {
                await DisplayAlert("Test", v.VehicleYear + " " + v.VehicleMaker + " " +
                    v.VehicleModel, "OK");

                await Navigation.PushAsync(new EventsPage(v));

                cvVehicles.SelectedItem = null;
            }

        }

        private void btnNew_Clicked(object sender, EventArgs e)
        {

            DisplayAlert("Test", "New", "OK");

        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {



        }

    }

}