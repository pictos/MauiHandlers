using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System.Drawing;
using UIKit;

namespace MauiHandlers.Controls.ImageEntry;
partial class ImageEntry
{
	public static partial void RegisterPlatformView()
	{
		EntryHandler.PlatformViewFactory = (handler) =>
		{
			var textField = new MauiTextField();

			if (handler.VirtualView is not ImageEntry element)
				return textField;

			AlignImage(element, textField);
			textField.BorderStyle = UITextBorderStyle.RoundedRect;
			textField.Layer.MasksToBounds = true;

			return textField;
		};
	}

	static void AlignImage(ImageEntry element, MauiTextField textField)
	{
		if (!string.IsNullOrEmpty(element.Image))
		{
			switch (element.ImageAlignment)
			{
				case ImageAlignment.Left:
					textField.LeftViewMode = UITextFieldViewMode.Always;
					textField.RightView = null;
					textField.LeftView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
					break;
				case ImageAlignment.Right:
					textField.RightViewMode = UITextFieldViewMode.Always;
					textField.LeftView = null;
					textField.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
					break;
			}
		}
	}

	public static void MapImage(IEntryHandler handler, IEntry entry)
	{
		if (entry is not ImageEntry imageEntry)
			return;

		AlignImage(imageEntry, handler.PlatformView);
	}

	static UIView GetImageView(string imagePath, int height, int width)
	{
		var uiImageView = new UIImageView(UIImage.FromFile(imagePath))
		{
			Frame = new RectangleF(0, 0, width, height)
		};
		UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width + 10, height));
		objLeftView.AddSubview(uiImageView);

		return objLeftView;
	}
}
