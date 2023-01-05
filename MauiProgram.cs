using Microsoft.AspNetCore.Components.WebView.Maui;
using CourseWork.Data;
using MudBlazor.Services;


namespace CourseWork;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		
		
		builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<InventoryService>();
        builder.Services.AddMudServices();

        return builder.Build();
	}
}
