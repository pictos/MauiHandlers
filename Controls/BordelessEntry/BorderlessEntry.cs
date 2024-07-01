using Microsoft.Maui.Handlers;

namespace MauiHandlers.Controls.BordelessEntry;

static partial class BorderlessEntry
{
	public static BindableProperty HasBorderProperty =
		BindableProperty.Create("HasBorder", typeof(bool), typeof(BorderlessEntry), true);

	public static void SetHasBorder(BindableObject bindable, bool value) =>
		bindable.SetValue(HasBorderProperty, value);

	public static bool GetHasBorder(BindableObject bindable) =>
		(bool)bindable.GetValue(HasBorderProperty);

	public static void MapBorder(IEntryHandler handler, IEntry entry)
	{
		var e = (Entry)entry;
		if (!e.IsSet(HasBorderProperty))
			return;
#if IOS
		handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif ANDROID
		handler.PlatformView.Background = null;
#endif
	}
}
