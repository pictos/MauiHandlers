using MauiHandlers.Controls.BordelessEntry;
using MauiHandlers.Controls.HorizontalProgressBar;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace MauiHandlers;
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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.ConfigureMauiHandlers(h =>
			{
				EntryHandler.Mapper.Add("HasBorder", BorderlessEntry.MapBorder);
				h.AddHandler(typeof(HorizontalProgressBar), typeof(HorizontalProgressBarHandler));
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
