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

        if (globalData.UltimoTipoFichaje == TipoFichaje.Entrada)
        {
            btnEntrada.IsEnabled = false;
            btnSalida.IsEnabled = true;;
        }

        else if (globalData.UltimoTipoFichaje == TipoFichaje.Salida || globalData.UltimoTipoFichaje == null)
        {
            btnEntrada.IsEnabled = true;
            btnSalida.IsEnabled = false;;
        }
    }

    private async void btnEntrada_Clicked(object sender, EventArgs e)
    {
        var fichaje = await _apiService.FicharEntrada(globalData.IdUsuario);
        globalData.UltimoTipoFichaje = TipoFichaje.Entrada;
        await CloseAsync();
    }

    private async void btnSalida_Clicked(object sender, EventArgs e)
    {
        var fichaje = await _apiService.FicharSalida(globalData.IdUsuario);
        globalData.UltimoTipoFichaje = TipoFichaje.Salida;
        await CloseAsync();
    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }
}