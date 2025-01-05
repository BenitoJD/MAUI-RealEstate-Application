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
			await Navigation.PushAsync(new LoginPage());
		}
		else
		{
			await DisplayAlert("Oops", "Something went wrong", "Cancel");
		}
	}
	async void TapLogin_Tapped(System.Object sender, System.EventArgs e)
	{
		await Navigation.PushAsync(new LoginPage());
	}
}