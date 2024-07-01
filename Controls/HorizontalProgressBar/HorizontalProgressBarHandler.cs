using Microsoft.Maui.Handlers;

namespace MauiHandlers.Controls.HorizontalProgressBar;

sealed partial class HorizontalProgressBarHandler
{
	public static PropertyMapper<HorizontalProgressBar, HorizontalProgressBarHandler> HorizontalProgressBarMapper =
			new(ViewMapper)
			{
				[nameof(HorizontalProgressBar.TrackColor)] = MapTrackColor,
				[nameof(HorizontalProgressBar.Progress)] = MapProgress,
				[nameof(HorizontalProgressBar.ProgressColor)] = MapProgressColor
			};

	public HorizontalProgressBarHandler() :
		base(HorizontalProgressBarMapper)
	{

	}

	public HorizontalProgressBarHandler(PropertyMapper mapper)
		: base(mapper)
	{

	}
}
