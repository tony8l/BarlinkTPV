using BarlinkTPV.Resources.Styles;
using BarlinkTPV.Resources.Languages;
using BarlinkTPV.Services;
using BarlinkTPV.Singleton;
using BarlinkTPV.Models.DTOs;
using BarlinkTPV.Models;

namespace BarlinkTPV.Views;

public partial class SettingsView : ContentPage
{
    private ApiService _apiService;
    private GlobalData globalData;
    private SettingsService ajustesService;
    private Ajustes ajustes = new Ajustes();
    public SettingsView(GlobalData globalData)
	{
		InitializeComponent();
        _apiService = new ApiService();
        this.globalData = globalData;
        ajustesService = new SettingsService();
        ajustes.Idioma = globalData.IdiomaActual;
        ajustes.Tema = globalData.TemaActual;
    }

    private async void btnTemaPredeterminado_Clicked(object sender, EventArgs e)
    {
        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries == null)
        {
            return;
        }

        ajustes.Tema = Tema.Predeterminado;
        mergedDictionaries.Clear();
        ajustesService.AplicarAjustes(ajustes);
        globalData.TemaActual = Tema.Predeterminado;
        await _apiService.EditarAjustes(globalData.AjustesActualesId, globalData.TemaActual, globalData.IdiomaActual);
    }

    private async void btnTemaClaro_Clicked(object sender, EventArgs e)
    {
        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries == null)
        {
            return;
        }

        ajustes.Tema = Tema.Claro;
        mergedDictionaries.Clear();
        ajustesService.AplicarAjustes(ajustes);
        globalData.TemaActual = Tema.Claro;
        await _apiService.EditarAjustes(globalData.AjustesActualesId, globalData.TemaActual, globalData.IdiomaActual);
    }

    private async void btnTemaOscuro_Clicked(object sender, EventArgs e)
    {
        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries == null)
        {
            return;
        }

        ajustes.Tema = Tema.Oscuro;
        mergedDictionaries.Clear();
        ajustesService.AplicarAjustes(ajustes);
        globalData.TemaActual = Tema.Oscuro;
        await _apiService.EditarAjustes(globalData.AjustesActualesId, globalData.TemaActual, globalData.IdiomaActual);
    }

    private async void btnIdiomaEspanol_Clicked(object sender, EventArgs e)
    {
        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries == null)
        {
            return;
        }

        ajustes.Idioma = Idioma.ES;
        mergedDictionaries.Clear();
        ajustesService.AplicarAjustes(ajustes);
        globalData.IdiomaActual = Idioma.ES;
        await _apiService.EditarAjustes(globalData.AjustesActualesId, globalData.TemaActual, globalData.IdiomaActual);
    }

    private async void btnIdiomaIngles_Clicked(object sender, EventArgs e)
    {
        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if(mergedDictionaries == null)
        {
            return;
        }

        ajustes.Idioma = Idioma.ENG;
        mergedDictionaries.Clear();
        ajustesService.AplicarAjustes(ajustes);
        globalData.IdiomaActual = Idioma.ENG;
        await _apiService.EditarAjustes(globalData.AjustesActualesId, globalData.TemaActual, globalData.IdiomaActual);

    }
}