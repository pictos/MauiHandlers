using CoreAnimation;
using CoreGraphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace MauiHandlers.Controls.HorizontalProgressBar;

partial class HorizontalProgressBarHandler : ViewHandler<HorizontalProgressBar, HorizontalProgressBariOS>
{
	protected override HorizontalProgressBariOS CreatePlatformView()
	{
		var virtualView = VirtualView;
		return new HorizontalProgressBariOS(virtualView.Width, virtualView.Height, virtualView.TrackColor.ToCGColor(), virtualView.ProgressColor.ToCGColor(), virtualView.Progress);
	}

	static void MapTrackColor(HorizontalProgressBarHandler handler, HorizontalProgressBar view)
	{
	}

	static void MapProgress(HorizontalProgressBarHandler handler, HorizontalProgressBar view)
	{
	}
	static void MapProgressColor(HorizontalProgressBarHandler handler, HorizontalProgressBar view)
	{
	}
}


sealed class HorizontalProgressBariOS : UIView
{
	private CAShapeLayer _progressLayer = new();
	private CAShapeLayer _trackLayer = new();
	private double _width;
	private double _height;
	private CGColor _trackColor;
	private CGColor _progressColor;
	private double _progress;

	public HorizontalProgressBariOS(
		double width,
		double height,
		CGColor trackColor,
		CGColor progressColor,
		double progress)
	{
		_width = width;
		_height = height;
		_trackColor = trackColor;
		_progressColor = progressColor;
		Progress = progress;
	}

	public CGColor TrackColor { get => _trackColor; set { _trackColor = value; LayoutSubviews(); } }
	public CGColor ProgressColor { get => _progressColor; set { _progressColor = value; LayoutSubviews(); } }
	public double Progress { get => _progress; set { _progress = value; LayoutSubviews(); } }

	public override void LayoutSubviews()
	{
		base.LayoutSubviews();
		MakeHorizontalPath();
	}

	private void MakeHorizontalPath()
	{
		BackgroundColor = UIColor.Clear;
		Layer.CornerRadius = (nfloat)(_height / 2);
		Layer.MasksToBounds = true;

		var path = new UIBezierPath();

		path.MoveTo(new CGPoint(0, _height / 2));
		path.AddLineTo(new CGPoint(Frame.Width, _height / 2));

		_trackLayer.Path = path.CGPath;
		_trackLayer.FillColor = UIColor.Clear.CGColor;
		_trackLayer.StrokeColor = _trackColor;
		_trackLayer.LineWidth = (nfloat)_height;
		_trackLayer.StrokeEnd = (nfloat)1.0;
		Layer.AddSublayer(_trackLayer);

		_progressLayer.Path = path.CGPath;
		_progressLayer.FillColor = UIColor.Clear.CGColor;
		_progressLayer.StrokeColor = _progressColor;
		_progressLayer.LineWidth = (nfloat)_height;
		_progressLayer.StrokeEnd = (nfloat)Progress;
		_progressLayer.LineCap = CAShapeLayer.CapRound;
		Layer.AddSublayer(_progressLayer);

		path.ClosePath();
	}
}