using CarLog.Models;
using CarLog.ViewModels;

namespace CarLog;

public partial class EventsPage : ContentPage
{

	public String VehicleName { get; set; }

	public EventsPage(Vehicle vehicle)
	{
		InitializeComponent();
		BindingContext = new EventsViewModel(vehicle);
    }

    private void btnNew_Clicked(object sender, EventArgs e)
    {

        DisplayAlert("Test", "New", "OK");

    }

    private async void cvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        VehicleEvent v = (VehicleEvent)e.CurrentSelection.FirstOrDefault();

        if (v != null)
        {
            await DisplayAlert("Test", v.EventTimestamp + " " + v.EventMileage + " " +
                v.EventName, "OK");

            // Edit the event here, if desired

            cvEvents.SelectedItem = null;
        }

    }

}