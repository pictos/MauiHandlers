﻿using Microsoft.Maui.Handlers;
using AProgressBar = Android.Widget.ProgressBar;
using Android.Content.Res;
using Android.Content;
using Android.Graphics.Drawables;
using Microsoft.Maui.Platform;

namespace MauiHandlers.Controls.HorizontalProgressBar;
partial class HorizontalProgressBarHandler : ViewHandler<HorizontalProgressBar, AProgressBar>
{
	const int PROGRESS_MAX_VALUE = 100;

	protected override AProgressBar CreatePlatformView()
	{
		return new AProgressBar(Context, null, Android.Resource.Attribute.ProgressBarStyleHorizontal);
	}

	static void MapTrackColor(HorizontalProgressBarHandler handler, HorizontalProgressBar view)
	{
		handler.PlatformView.UpdateBackground(view.TrackColor);
	}

	static void MapProgress(HorizontalProgressBarHandler handler, HorizontalProgressBar view)
	{
		handler.PlatformView.UpdateProgressValue(view.Progress);
	}
	static void MapProgressColor(HorizontalProgressBarHandler handler, HorizontalProgressBar view)
	{
		handler.PlatformView.UpdateProgressDrawable(view.ProgressColor);
	}
}


static class HorizontalProgressBarExtensions
{
	internal static void UpdateBackground(this AProgressBar platformView, Microsoft.Maui.Graphics.Color color)
	{
		if (color is null || platformView.Context is null)
			return;

		var drawable = GetHorizontalTrack(color.ToPlatform(), platformView.Context);
		platformView.Background = drawable;

		static Drawable? GetHorizontalTrack(Android.Graphics.Color color, Context context)
		{
			var drawable = context.GetDrawable(Resource.Drawable.horizontal_track_bar) as GradientDrawable;
			drawable?.SetColor(ColorStateList.ValueOf(color));
			return drawable;
		}
	}

	public static void UpdateProgressValue(this AProgressBar platformView, double progress)
	{
		platformView.SetProgress(GetProgress(progress), true);

		static int GetProgress(double progress)
		{
			return Convert.ToInt32(
				Math.Floor(progress * 100)
			);
		}
	}

	public static void UpdateProgressDrawable(this AProgressBar platformView, Microsoft.Maui.Graphics.Color color)
	{
		if (color is null || platformView.Context is null)
			return;

		platformView.SetProgressDrawableTiled(GetHorizontalProgress(color.ToPlatform(), platformView.Context));

		static Drawable? GetHorizontalProgress(Android.Graphics.Color color, Context context)
		{
			var scale = context.GetDrawable(Resource.Drawable.horizontal_progress_bar) as ScaleDrawable;

			var drawable = scale!.Drawable as GradientDrawable;
			drawable?.SetColor(ColorStateList.ValueOf(color));

			return scale;
		}
	}
}
