namespace HandlersApp.Views;

public partial class DrawPage : ContentPage
{
	public DrawPage()
	{
		InitializeComponent();
	}

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        drawView.Clear();
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        drawView.Save();
    }
}