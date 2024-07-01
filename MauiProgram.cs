using MauiHandlers.Controls;
using MauiHandlers.Controls.BordelessEntry;
using MauiHandlers.Controls.HorizontalProgressBar;
using MauiHandlers.Controls.ImageEntry;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using IImage = Microsoft.Maui.IImage;

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
				EntryHandler.Mapper.Add(nameof(ImageEntry.Image), ImageEntry.MapImage);
				EntryHandler.Mapper.Add(nameof(ImageEntry.ImageAlignment), ImageEntry.MapImage);
				EntryHandler.Mapper.Add(nameof(ImageEntry.ImageWidth), ImageEntry.MapImage);
				EntryHandler.Mapper.Add(nameof(ImageEntry.ImageHeight), ImageEntry.MapImage);
				h.AddHandler(typeof(HorizontalProgressBar), typeof(HorizontalProgressBarHandler));

#if IOS
				ButtonHandler.Mapper.AppendToMapping(nameof(IButton.Height), NewMap);
				ButtonHandler.Mapper.AppendToMapping(nameof(IButton.Width), NewMap);
				ButtonHandler.Mapper.ReplaceMapping<IButton, IButtonHandler>(nameof(IButton.CornerRadius), NewMap);


				static void NewMap(IButtonHandler handler, Microsoft.Maui.IButton button)
				{
					ControlExtensions.CreateCircle(handler.PlatformView, (Button)button);
				}
#endif
			});

		ImageEntry.RegisterPlatformView();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
