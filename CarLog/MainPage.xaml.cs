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

            if (CLRepository.Vehicles.Count > 0)
            {
                lblEmptyMsg.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            RefreshData();
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

            await Navigation.PushAsync(new EditVehiclePage());

            // A call to an add form would precede this, of course.
            /////[TEMP] await CLRepository.AddVehicleAsync();

            lblEmptyMsg.IsVisible = false;

            RefreshData();

        }

        private void btnHelp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }

        private void btnAbout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AboutPage());
        }

    }

}