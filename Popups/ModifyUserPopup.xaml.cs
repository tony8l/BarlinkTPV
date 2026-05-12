using BarlinkTPV.Models;
using BarlinkTPV.Services;
using BarlinkTPV.Models.DTOs;
using CommunityToolkit.Maui.Views;

namespace BarlinkTPV.Popups;

public partial class ModifyUserPopup : Popup
{
	private Usuario usuarioSeleccionado;
	private ApiService _apiService;
    public ModifyUserPopup(Usuario usuario)
	{
		InitializeComponent();
		usuarioSeleccionado = usuario;
		_apiService = new ApiService();
		entryDni.Text = usuarioSeleccionado.Dni;
		entryNombre.Text = usuarioSeleccionado.Nombre;
		entryApellidos.Text = usuarioSeleccionado.Apellidos;
		pickerRol.ItemsSource = Enum.GetValues(typeof(RolUsuario)).Cast<RolUsuario>().ToList();
		pickerRol.SelectedItem = usuarioSeleccionado.Rol;
		checkActivado.IsChecked = usuarioSeleccionado.Activado;
    }

    private async void btnConfirmar_Clicked(object sender, EventArgs e)
    {
		if (usuarioSeleccionado != null)
		{
            await _apiService.EditarUsuario(usuarioSeleccionado.Id, entryDni.Text, entryNombre.Text, entryApellidos.Text, (RolUsuario)pickerRol.SelectedItem, checkActivado.IsChecked);
			await CloseAsync();
        }
    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }
}