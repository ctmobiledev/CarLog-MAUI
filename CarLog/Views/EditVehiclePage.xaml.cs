using CarLog.Models;
using CarLog.Repository;
using CarLog.ViewModels;

namespace CarLog;

public partial class EditVehiclePage : ContentPage
{
	public EditVehiclePage(Vehicle vehicle)
	{
		InitializeComponent();

        // This needs to stay here since the viewModel is keyed off the vehicle (can't do that in XAML?)
        BindingContext = new EditVehicleViewModel(vehicle);
    }

}