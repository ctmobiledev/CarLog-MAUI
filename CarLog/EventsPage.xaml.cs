using CarLog.Models;
using CarLog.Repository;
using CarLog.ViewModels;

namespace CarLog;

public partial class EventsPage : ContentPage
{

    private Vehicle _vehicle;

	public String VehicleName { get; set; }

	public EventsPage(Vehicle vehicle)
	{
		InitializeComponent();
        _vehicle = vehicle;
		
        // This needs to stay here since the viewModel is keyed off the vehicle (can't do that in XAML?)
        BindingContext = new EventsViewModel(_vehicle);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        lblEmptyMsg.IsVisible = (_vehicle.VehicleEvents.Count == 0);

        cvEvents.ItemsSource = null;
        cvEvents.ItemsSource = _vehicle.VehicleEvents;

        cvEvents.SelectedItem = null;
    }


    private async void cvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        VehicleEvent v = (VehicleEvent)e.CurrentSelection.FirstOrDefault();

        if (v != null)
        {
            // MENU OF OPTIONS HERE: VIEW/UPDATE, DELETE ENTRY

            await DisplayAlert("Test", v.EventTimestamp + " " + v.EventMileage + " " +
                v.EventName, "OK");

            // Edit the event here, if desired

            cvEvents.SelectedItem = null;
        }

    }

}