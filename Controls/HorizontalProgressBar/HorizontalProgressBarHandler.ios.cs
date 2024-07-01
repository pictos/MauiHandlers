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
		return new HorizontalProgressBariOS(virtualView.Height, virtualView.Width, virtualView.TrackColor.ToCGColor(), virtualView.ProgressColor.ToCGColor(), virtualView.Progress);
	}

	protected override void ConnectHandler(HorizontalProgressBariOS platformView)
	{
		//VirtualView.SizeChanged += VirtualView_SizeChanged;
	}

	protected override void DisconnectHandler(HorizontalProgressBariOS platformView)
	{
		VirtualView.SizeChanged -= VirtualView_SizeChanged;
	}

	void VirtualView_SizeChanged(object? sender, EventArgs e)
	{
		if (VirtualView.Height != this.PlatformView.Height)
			PlatformView.Height = VirtualView.Height;

		if (VirtualView.Width != PlatformView.Width)
			PlatformView.Width = PlatformView.Width;
	}

	static void MapTrackColor(HorizontalProgressBarHandler handler, HorizontalProgressBar view)
	{
		view.TrackColor ??= Colors.Transparent;
		handler.PlatformView.TrackColor = view.TrackColor.ToCGColor();
	}

	static void MapProgress(HorizontalProgressBarHandler handler, HorizontalProgressBar view)
	{
		handler.PlatformView.Progress = view.Progress;
	}
	static void MapProgressColor(HorizontalProgressBarHandler handler, HorizontalProgressBar view)
	{
		view.ProgressColor ??= Colors.Transparent;
		handler.PlatformView.ProgressColor = view.ProgressColor.ToCGColor();
	}
}


sealed class HorizontalProgressBariOS : MauiView
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
		Width = width;
		Height = height <= 0 ? 30 : height;
		_trackColor = trackColor;
		_progressColor = progressColor;
		Progress = progress;
	}

	public CGColor TrackColor { get => _trackColor; set { _trackColor = value; LayoutSubviews(); } }
	public CGColor ProgressColor { get => _progressColor; set { _progressColor = value; LayoutSubviews(); } }
	public double Progress { get => _progress; set { _progress = value; LayoutSubviews(); } }

	public double Width
	{
		get => _width;
		set
		{
			if (_width == value)
				return;

			_width = value;
			LayoutSubviews();
		}
	}
	public double Height
	{
		get => _height;
		set
		{
			if (_height == value)
				return;
			_height = value;
			LayoutSubviews();
		}
	}

	public override void LayoutSubviews()
	{
		base.LayoutSubviews();
		MakeHorizontalPath();
	}

	private void MakeHorizontalPath()
	{
		BackgroundColor = UIColor.Clear;
		var height = Frame.Height;
		var width = Frame.Width;
		Layer.CornerRadius = (nfloat)(height / 2);
		Layer.MasksToBounds = true;

		var path = new UIBezierPath();

		path.MoveTo(new CGPoint(0, height / 2));
		path.AddLineTo(new CGPoint(Frame.Width, height / 2));

		_trackLayer.Path = path.CGPath;
		_trackLayer.FillColor = UIColor.Clear.CGColor;
		_trackLayer.StrokeColor = _trackColor;
		_trackLayer.LineWidth = (nfloat)height;
		_trackLayer.StrokeEnd = (nfloat)1.0;
		Layer.AddSublayer(_trackLayer);

		_progressLayer.Path = path.CGPath;
		_progressLayer.FillColor = UIColor.Clear.CGColor;
		_progressLayer.StrokeColor = _progressColor;
		_progressLayer.LineWidth = (nfloat)height;
		_progressLayer.StrokeEnd = (nfloat)Progress;
		_progressLayer.LineCap = CAShapeLayer.CapRound;
		Layer.AddSublayer(_progressLayer);

		path.ClosePath();
	}
}