using RealEstates.Services;

namespace RealEstates.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

	async void BtnRegister_Clicked(System.Object sender, System.EventArgs e)
	{
		var response = await ApiService.RegisterUser(EntFullName.Text, EntEmail.Text, EntPassword.Text, EntPhone.Text);
		if (response)
		{
			await DisplayAlert("Success", "You have successfully registered", "OK");
		}
		else
		{
			await DisplayAlert("Oops", "Something went wrong", "Cancel");
		}
	}
}