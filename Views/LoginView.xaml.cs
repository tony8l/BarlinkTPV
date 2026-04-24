using BarlinkTPV.Navigation;
namespace BarlinkTPV.Views;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
	}

    private void btnIniciarSesion_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new UserNavigation();
    }
}