using BarlinkTPV.Models;
using BarlinkTPV.Services;
using CommunityToolkit.Maui.Views;
using BarlinkTPV.Singleton;

namespace BarlinkTPV.Popups;

public partial class WorkerRegPopup : Popup
{
    private ApiService _apiService;
    private GlobalData globalData;
	public WorkerRegPopup(GlobalData globalData)
	{
		InitializeComponent();
        _apiService = new ApiService();
        this.globalData = globalData;

        // Si el usuario ya ha fichado una "Entrada", se desactiva el Botón Entrada y se activa el Botón Salida
        if (globalData.UltimoTipoFichaje == TipoFichaje.Entrada)
        {
            btnEntrada.IsEnabled = false;
            btnSalida.IsEnabled = true;;
        }

        // Si el usuario ya ha fichado una "Salida", se desactiva el Botón Salida y se activa el Botón Entrada
        else if (globalData.UltimoTipoFichaje == TipoFichaje.Salida || globalData.UltimoTipoFichaje == null)
        {
            btnEntrada.IsEnabled = true;
            btnSalida.IsEnabled = false;;
        }
    }

    // Se inserta el fichaje de entrada y volver atrás
    private async void btnEntrada_Clicked(object sender, EventArgs e)
    {
        var fichaje = await _apiService.FicharEntrada(globalData.IdUsuario);
        globalData.UltimoTipoFichaje = TipoFichaje.Entrada;
        await CloseAsync();
    }

    // Se inserta el fichaje de salida y volver atrás
    private async void btnSalida_Clicked(object sender, EventArgs e)
    {
        var fichaje = await _apiService.FicharSalida(globalData.IdUsuario);
        globalData.UltimoTipoFichaje = TipoFichaje.Salida;
        await CloseAsync();
    }

    // Volver atrás
    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }
}