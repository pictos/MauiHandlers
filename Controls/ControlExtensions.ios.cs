using CoreAnimation;
using UIKit;

namespace MauiHandlers.Controls;

static class ControlExtensions
{
	const string borderName = "borderLayerName";

	public static void CreateCircle(UIView control, View virtualView)
	{
		var min = Math.Max(virtualView.Width, virtualView.Height);

		if (min <= 0)
			return;

		if (virtualView.Width != virtualView.Height)
		{
			virtualView.WidthRequest = virtualView.HeightRequest = min;
			return;
		}


		control.Layer.CornerRadius = (nfloat)(min / 2.0);

		control.Layer.MasksToBounds = false;
		control.ClipsToBounds = true;

		var tempLayer = control.Layer.Sublayers?.FirstOrDefault(x => x.Name == borderName);
		tempLayer?.RemoveFromSuperLayer();

		var externalBorder = new CALayer
		{
			Name = borderName,
			CornerRadius = control.Layer.CornerRadius,
			Frame = new CoreGraphics.CGRect(-.5, -.5, min + 1, min + 1),
			MasksToBounds = true,
		};

		control.Layer.AddSublayer(externalBorder);
	}
}
