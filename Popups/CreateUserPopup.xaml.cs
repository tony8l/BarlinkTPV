using BarlinkTPV.Models;
using BarlinkTPV.Services;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;

namespace BarlinkTPV.Popups;

public partial class CreateUserPopup : Popup
{
	private ApiService _apiService;
	public CreateUserPopup()
	{
		InitializeComponent();
		_apiService = new ApiService();
        pickerRol.ItemsSource = Enum.GetValues(typeof(RolUsuario)).Cast<RolUsuario>().ToList();
        pickerRol.SelectedItem = RolUsuario.Camarero;
        checkActivado.IsChecked = true;
    }

    // Insertamos un nuevo usuario con los valores pasados por parámetro
    private async void btnConfirmar_Clicked(object sender, EventArgs e)
    {
		var usuario = await _apiService.CrearUsuario(entryDni.Text, entryNombre.Text, entryApellidos.Text, (RolUsuario)pickerRol.SelectedItem, checkActivado.IsChecked);
        await _apiService.CrearAjustes(usuario.Id);
        await CloseAsync();
    }

    // Volver atrás
    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }
}