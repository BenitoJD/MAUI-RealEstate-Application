using RealEstates.Models;
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
	void CvCategories_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
	{
		var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
		if (currentSelection == null) return;
		Navigation.PushAsync(new PropertiesListPage(currentSelection.Id, currentSelection.Name));
		((CollectionView)sender).SelectedItem = null;
	}
	void CvTopPicks_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as TrendingProperty;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PropertyDetailedPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }

    void TapSearch_Tapped(System.Object Sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Navigation.PushModalAsync(new SearchPage());
    }
}