using RealEstates.Services;

namespace RealEstates.Pages;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	    LblUserName.Text = "Hi " + Preferences.Get("userName", string.Empty);
		GetCategories();
		GetTrendingProperties();
	}

    private async void GetTrendingProperties()
    {
		var properties = await ApiService.GetTrendingProperties();
        CvTopPicks.ItemsSource = properties;
    }

    private async void GetCategories()
	{
		var categories = await ApiService.GetCategories();
		CvCategories.ItemsSource = categories;
    }
}