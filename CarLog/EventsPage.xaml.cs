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
		BindingContext = new EventsViewModel(_vehicle);
    }

    private void RefreshData()
    {
        cvEvents.ItemsSource = null;
        cvEvents.ItemsSource = _vehicle.VehicleEvents;
    }

    private async void btnNew_Clicked(object sender, EventArgs e)
    {

        //await DisplayAlert("Test", "New", "OK");

        // A call to an add form would precede this, of course.
        await CLRepository.AddVehicleEventAsync(_vehicle);

        RefreshData();

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