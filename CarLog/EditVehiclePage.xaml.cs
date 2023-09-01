using CarLog.Models;
using CarLog.Repository;

namespace CarLog;

public partial class EditVehiclePage : ContentPage
{
	public EditVehiclePage()
	{
		InitializeComponent();
	}

    private async void btnSave_Clicked(object sender, EventArgs e)
    {

        // test only
        await Task.Run(() => {

            CLRepository.Vehicles.Add(new Vehicle
            {
                VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                VehicleYear = 2017,
                VehicleMaker = "Chevrolet",
                VehicleModel = "Spark",
                VehicleColor = "Splash",
                VehicleMileage = 175000,
                LicensePlateTag = "WIN123",
                LicensePlateState = "TX",
                LicensePlateExpiry = "2023-12-31"
            });

        });

        await Navigation.PopAsync();

    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {

        // If data changed, confirm exit

        Navigation.PopAsync();

    }

}