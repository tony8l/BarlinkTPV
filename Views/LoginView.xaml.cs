using BarlinkTPV.Navigation;
using BarlinkTPV.Services;
using BarlinkTPV.Singleton;
using BarlinkTPV.Models;
namespace BarlinkTPV.Views;

public partial class LoginView : ContentPage
{
	public readonly GlobalData globalData;
    private ApiService _apiService;
	public LoginView(GlobalData globalData)
	{
		InitializeComponent();
		this.globalData = globalData;
	}

    private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
    {
        /*bool loginCorrecto = true;
		string nombreUsuario = "Antony";

		if (loginCorrecto)
		{
			globalData.NombreUsuario = nombreUsuario;
            Application.Current.MainPage = new UserNavigation(globalData);
        }*/

        string dniIntroducido;
        if(entryDni.Text != null)
        {
            dniIntroducido = entryDni.Text.Trim().ToUpper();
        }
        else
        {
            dniIntroducido = null;
        }

		 if (string.IsNullOrEmpty(dniIntroducido))
        {
            await DisplayAlertAsync("Aviso", "Introduce un DNI", "Aceptar");
            return;
        }

        var usuario = await _apiService.IniciarSesion(dniIntroducido);

        if (usuario != null)
        {
            globalData.NombreUsuario = usuario.Nombre;

            if (usuario.Rol == RolUsuario.Camarero)
            {
                Application.Current.MainPage = new UserNavigation(globalData);
            }
            else if (usuario.Rol == RolUsuario.Encargado)
            { 
                
            }
        }
        else
        {
            await DisplayAlertAsync("Error", "Usuario no encontrado", "Aceptar");
        }
    }

    private void btnApagar_Clicked(object sender, EventArgs e)
    {
		Application.Current.Quit();
    }
}