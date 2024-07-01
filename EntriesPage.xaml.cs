namespace MauiHandlers;

public partial class EntriesPage : ContentPage
{
	public EntriesPage()
	{
		InitializeComponent();
	}

	void Button_Clicked(object sender, EventArgs e)
	{
		imgEntry.ImageAlignment = imgEntry.ImageAlignment == Controls.ImageEntry.ImageAlignment.Left
			? Controls.ImageEntry.ImageAlignment.Right
			: Controls.ImageEntry.ImageAlignment.Left;
	}
}