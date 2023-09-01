using CarLog.Models;

namespace CarLog;

public partial class EditVehicleEventPage : ContentPage
{

    private Vehicle _vehicle;

	public EditVehicleEventPage(Vehicle vehicle)
	{
		InitializeComponent();
        _vehicle = vehicle;
	}

    private async void btnSave_Clicked(object sender, EventArgs e)
    {

        await Task.Run(() => {

            _vehicle.VehicleEvents.Add(new VehicleEvent
            {
                EventId = Guid.NewGuid(),
                VID = new Guid("e57b36ee-5abd-465c-ab8c-87d3cca3545c"),
                EventTimestamp = DateTime.Now,
                EventMileage = 99000,
                EventName = "Routine Checkup"
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