using System.ComponentModel;
using System.Runtime.CompilerServices;
using NasaImageOfTheDay.Models;

namespace NasaImageOfTheDay.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public class DetailViewModel : INotifyPropertyChanged
{
    private NasaImageItem? _item;

    public NasaImageItem? Item
    {
        get => _item;
        set { _item = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
