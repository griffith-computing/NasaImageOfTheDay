using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using NasaImageOfTheDay.Models;
using NasaImageOfTheDay.Services;

namespace NasaImageOfTheDay.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly NasaImageService _service;
    private bool _isLoading;
    private string _errorMessage = string.Empty;

    public ObservableCollection<NasaImageItem> Images { get; } = new();

    public bool IsLoading
    {
        get => _isLoading;
        set { _isLoading = value; OnPropertyChanged(); }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasError)); }
    }

    public bool HasError => !string.IsNullOrEmpty(_errorMessage);

    public ICommand LoadCommand { get; }

    public MainViewModel(NasaImageService service)
    {
        _service = service;
        LoadCommand = new Command(async () => await LoadImagesAsync());
    }

    public async Task LoadImagesAsync()
    {
        if (IsLoading)
            return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var items = await _service.GetImagesAsync();
            Images.Clear();
            foreach (var item in items)
                Images.Add(item);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load images: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
