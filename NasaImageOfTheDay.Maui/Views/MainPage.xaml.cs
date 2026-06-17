using NasaImageOfTheDay.Models;
using NasaImageOfTheDay.ViewModels;

namespace NasaImageOfTheDay.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;

    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_viewModel.Images.Any())
            await _viewModel.LoadImagesAsync();
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not NasaImageItem selected)
            return;

        // Clear selection so item can be tapped again
        ((CollectionView)sender).SelectedItem = null;

        await Shell.Current.GoToAsync(nameof(DetailPage), new Dictionary<string, object>
        {
            { "Item", selected }
        });
    }
}
