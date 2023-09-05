using CarLog.Models;
using CarLog.ViewModels;

namespace CarLog;

public partial class EditVehicleEventPage : ContentPage
{

    public static Vehicle _vehicle;

	public EditVehicleEventPage(Vehicle vehicle)
	{
		InitializeComponent();
		EditVehicleEventViewModel._vehicle = vehicle;
	}

}