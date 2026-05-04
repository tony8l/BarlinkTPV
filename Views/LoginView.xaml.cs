using BarlinkTPV.Models;
using BarlinkTPV.Navigation;
using BarlinkTPV.Services;
using BarlinkTPV.Singleton;
namespace BarlinkTPV.Views;

public partial class LoginView : ContentPage
{
	public readonly GlobalData globalData;
    private ApiService _apiService;
	public LoginView(GlobalData globalData, ApiService apiService)
	{
		InitializeComponent();
		this.globalData = globalData;
        _apiService = apiService;
    }

    private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
    {
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
            var ultimoFichaje = await _apiService.ObtenerUltimoFichaje(usuario.Dni);

            globalData.IdUsuario = usuario.Id;
            globalData.DniUsuario = usuario.Dni;
            globalData.NombreUsuario = usuario.Nombre;
            globalData.RolUsuario = usuario.Rol;
            globalData.UltimoTipoFichaje = ultimoFichaje?.TipoFichaje;

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