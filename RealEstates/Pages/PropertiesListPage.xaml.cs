
using RealEstates.Models;
using RealEstates.Services;

namespace RealEstates.Pages;

public partial class PropertiesListPage : ContentPage
{
	public PropertiesListPage(int categoryId, string categoryName)
	{
		InitializeComponent();
		Title = categoryName;
		GetPropertyList(categoryId);

    }

    private void CvProperties_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as PropertyByCategory;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PropertyDetailedPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }

    private async void GetPropertyList(int categoryId)
    {
	  var properties = await ApiService.GetPropertyByCategory(categoryId);
		CvProperties.ItemsSource = properties;

    }
}