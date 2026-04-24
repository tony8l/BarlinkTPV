using BarlinkTPV.Navigation;
namespace BarlinkTPV.Views;

public partial class HomeView : ContentPage
{
	public HomeView()
	{
		InitializeComponent();
	}

    private void btnCerrarSesion_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new InitialNavigation();
    }

    private async void btnCaja_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PosView");
    }
}