using System.Collections.ObjectModel;
using BarlinkTPV.Models;
using BarlinkTPV.Popups;
using BarlinkTPV.Services;
using CommunityToolkit.Maui.Extensions;

namespace BarlinkTPV.Views;

public partial class UsersManagementView : ContentPage
{
	ApiService _apiService;
	public ObservableCollection<Usuario> Usuarios { get; set; } = new();
	public Usuario? usuarioSeleccionado = new Usuario();

	public UsersManagementView()
	{
		InitializeComponent();
		_apiService = new ApiService();
		usuarioSeleccionado = null;
		BindingContext = this;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await ObtenerUsuarios();
	}

    private void dataGridUsuarios_SelectionChanged(object sender, Syncfusion.Maui.DataGrid.DataGridSelectionChangedEventArgs e)
    {
		usuarioSeleccionado = e.AddedRows?.FirstOrDefault() as Usuario;

		if (usuarioSeleccionado == null)
		{
			return;
		}
    }

	public async Task ObtenerUsuarios()
	{
		var usuarios = await _apiService.ObtenerUsuarios();
		Usuarios.Clear();
		foreach (var usuario in usuarios)
		{
			Usuarios.Add(usuario);
		}
	}

    private async void btnNuevoUsuario_Clicked(object sender, EventArgs e)
    {
		var popup = new CreateUserPopup();
		await this.ShowPopupAsync(popup);
		usuarioSeleccionado = null;
    }

    private async void btnEditarUsuario_Clicked(object sender, EventArgs e)
    {
		if (usuarioSeleccionado == null)
		{
			await DisplayAlertAsync("Error", "No se ha seleccionado ningun usuario", "Aceptar");
			return;
		}
		else
		{
            var popup = new ModifyUserPopup(usuarioSeleccionado);
			await this.ShowPopupAsync(popup);
			usuarioSeleccionado = null;
        }
    }

    private async void btnActualizarTabla_Clicked(object sender, EventArgs e)
    {
		await ObtenerUsuarios();
    }
}