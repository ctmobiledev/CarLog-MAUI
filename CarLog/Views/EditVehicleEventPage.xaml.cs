using CarLog.Models;
using CarLog.ViewModels;

namespace CarLog;

public partial class EditVehicleEventPage : ContentPage
{

    public static Vehicle _vehicle;

	public EditVehicleEventPage(Vehicle vehicle, VehicleEvent vehicleEvent)
	{
		InitializeComponent();

        BindingContext = new EditVehicleEventViewModel(vehicleEvent);

        EditVehicleEventViewModel._vehicle = vehicle;
	}

}