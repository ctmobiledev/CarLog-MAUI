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

}