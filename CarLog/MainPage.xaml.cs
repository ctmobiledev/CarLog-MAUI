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
            BindingContext = new MainViewModel();           // loads data, waits til ready
            RefreshData();
        }

        private void RefreshData()                          // ideally needs to be bound, to use INotifyPropertyChanged
        {
            cvVehicles.ItemsSource = null;          
            cvVehicles.ItemsSource = CLRepository.Vehicles;
        }

        private async void cvVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // MENU OF OPTIONS HERE: VIEW/UPDATE, DELETE ENTRY

            Vehicle v = (Vehicle)e.CurrentSelection.FirstOrDefault();

            if (v != null)
            {
                //await DisplayAlert("Test", v.VehicleYear + " " + v.VehicleMaker + " " +
                //    v.VehicleModel, "OK");

                await Navigation.PushAsync(new EventsPage(v));

                cvVehicles.SelectedItem = null;
            }

        }

        private async void btnNew_Clicked(object sender, EventArgs e)
        {

            //await DisplayAlert("Test", "New", "OK");

            // A call to an add form would precede this, of course.
            await CLRepository.AddVehicleAsync();

            RefreshData();

        }

    }

}