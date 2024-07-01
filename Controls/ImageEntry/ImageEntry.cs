﻿namespace MauiHandlers.Controls.ImageEntry;
sealed partial class ImageEntry : Entry
{
	public static readonly BindableProperty ImageProperty =
		BindableProperty.Create(nameof(Image), typeof(string), typeof(ImageEntry), string.Empty);

	public static readonly BindableProperty ImageHeightProperty =
		 BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(ImageEntry), 40);

	public static readonly BindableProperty ImageWidthProperty =
		BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(ImageEntry), 40);

	public static readonly BindableProperty ImageAlignmentProperty =
		BindableProperty.Create(nameof(ImageAlignment), typeof(ImageAlignment), typeof(ImageEntry), ImageAlignment.Left);


	public int ImageWidth
	{
		get { return (int)GetValue(ImageWidthProperty); }
		set { SetValue(ImageWidthProperty, value); }
	}

	public int ImageHeight
	{
		get { return (int)GetValue(ImageHeightProperty); }
		set { SetValue(ImageHeightProperty, value); }
	}

	public string Image
	{
		get { return (string)GetValue(ImageProperty); }
		set { SetValue(ImageProperty, value); }
	}

	public ImageAlignment ImageAlignment
	{
		get { return (ImageAlignment)GetValue(ImageAlignmentProperty); }
		set { SetValue(ImageAlignmentProperty, value); }
	}

	public static partial void RegisterPlatformView();
}

public enum ImageAlignment
{
	Left,
	Right
}