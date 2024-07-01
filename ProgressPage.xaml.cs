namespace MauiHandlers;

public partial class ProgressPage
{
	public ProgressPage()
	{
		InitializeComponent();
	}

	void Button_Clicked(object sender, EventArgs e)
	{
		var value = Random.Shared.NextDouble();
		horizontalProgress.Progress = value;
		lbl.Text = value.ToString();
	}
}