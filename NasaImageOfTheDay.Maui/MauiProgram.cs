using Microsoft.Extensions.Logging;
using NasaImageOfTheDay.Services;
using NasaImageOfTheDay.ViewModels;
using NasaImageOfTheDay.Views;

namespace NasaImageOfTheDay;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMaui();

        // Services
        builder.Services.AddSingleton<NasaImageService>();

        // ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<DetailViewModel>();

        // Views
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<DetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
