using BarlinkTPV.Models;
using BarlinkTPV.Models.DTOs;
using BarlinkTPV.Navigation;
using BarlinkTPV.Services;
using BarlinkTPV.Singleton;
using BarlinkTPV.Resources.Languages;
using BarlinkTPV.Resources.Styles;
namespace BarlinkTPV.Views;

public partial class LoginView : ContentPage
{
	public readonly GlobalData globalData;
    private ApiService _apiService;
    private SettingsService ajustes;
	public LoginView(GlobalData globalData, ApiService apiService)
	{
		InitializeComponent();
		this.globalData = globalData;
        _apiService = apiService;
        ajustes = new SettingsService();
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

            var ajustesUsuario = await _apiService.ObtenerAjustesUsuario(usuario.Id);

            if (ajustesUsuario == null)
            {
                ajustesUsuario = await _apiService.CrearAjustes(usuario.Id);
            }

            if (ajustesUsuario != null)
            {
                globalData.AjustesActualesId = ajustesUsuario.Id;
                globalData.TemaActual = ajustesUsuario.Tema;
                globalData.IdiomaActual = ajustesUsuario.Idioma;
                ajustes.AplicarAjustes(ajustesUsuario);
            }

            if (usuario.Rol == RolUsuario.Camarero)
            {
                Application.Current.MainPage = new UserNavigation(globalData);
            }
            else if (usuario.Rol == RolUsuario.Encargado)
            {
                Application.Current.MainPage = new AdminNavigation(globalData);
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