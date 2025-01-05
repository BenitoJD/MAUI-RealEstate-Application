
using RealEstates.Services;

namespace RealEstates.Pages;

public partial class PropertyDetailedPage : ContentPage
{
    private string phoneNumber;

	public PropertyDetailedPage(int propertyId)
	{
		InitializeComponent();
		GetPropertyDetail(propertyId);
    }

    private async void GetPropertyDetail(int propertyId)
    {
        var property = await ApiService.GetPropertyDetail(propertyId);
        if (property != null)
        {
            LblPrice.Text = "$ " + property.Price;
            LblDescription.Text = property.Detail;
            LblAddress.Text = property.Address;
            ImgProperty.Source = property.FullImageUrl;
            phoneNumber =  property.Phone;
        }
    }
    void TapCall_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        PhoneDialer.Open(phoneNumber);
    }

    void TapMessage_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Sms.ComposeAsync(new SmsMessage("Hi! Iam Intrested in your property", phoneNumber));
    }
    void ImgbackBtn_Clicked(System.Object sender, System.EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}