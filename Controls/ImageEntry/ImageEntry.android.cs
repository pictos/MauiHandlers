using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.Content;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace MauiHandlers.Controls.ImageEntry;
partial class ImageEntry
{
	public static partial void RegisterPlatformView()
	{
		EntryHandler.PlatformViewFactory = (handler) =>
		{
			var editText = new AppCompatEditText(handler.Context);
			if (handler.VirtualView is not ImageEntry element)
				return editText;

			AlignImage(handler.Context, editText, element);

			editText.CompoundDrawablePadding = 25;
			editText.Background?.SetColorFilter(Colors.White.ToPlatform(), PorterDuff.Mode.SrcAtop);

			return editText;
		};
	}

	static void AlignImage(Context context, AppCompatEditText editText, ImageEntry element)
	{
		if (!string.IsNullOrEmpty(element.Image))
		{
			switch (element.ImageAlignment)
			{
				case ImageAlignment.Left:
					editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image, context, element), null, null, null);
					break;
				case ImageAlignment.Right:
					editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.Image, context, element), null);
					break;
			}
		}
	}

	public static void MapImage(IEntryHandler handler, IEntry entry)
	{
		if (entry is not ImageEntry imageEntry)
			return;

		AlignImage(handler.MauiContext!.Context!, handler.PlatformView, imageEntry);
	}

	static BitmapDrawable GetDrawable(string imageEntryImage, Context context, ImageEntry element)
	{
		int resID = context.Resources!.GetIdentifier(imageEntryImage, "drawable", context.PackageName);
		var drawable = ContextCompat.GetDrawable(context, resID)!;
		var bitmap = drawableToBitmap(drawable);

		return new BitmapDrawable(Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
	}

	static Bitmap drawableToBitmap(Drawable drawable)
	{
		if (drawable is BitmapDrawable)
		{
			return ((BitmapDrawable)drawable).Bitmap!;
		}

		int width = drawable.IntrinsicWidth;
		width = width > 0 ? width : 1;
		int height = drawable.IntrinsicHeight;
		height = height > 0 ? height : 1;

		Bitmap bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
		Canvas canvas = new Canvas(bitmap);
		drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
		drawable.Draw(canvas);

		return bitmap;
	}
}
