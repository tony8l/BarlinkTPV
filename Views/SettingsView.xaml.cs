using BarlinkTPV.Resources.Styles;
using BarlinkTPV.Resources.Languages;
using BarlinkTPV.Services;
using BarlinkTPV.Singleton;
using BarlinkTPV.Models.DTOs;
using BarlinkTPV.Models;

namespace BarlinkTPV.Views;

public partial class SettingsView : ContentPage
{
    private GlobalData globalData;
    private SettingsService ajustesService;
    private Ajustes ajustes = new Ajustes();
    public SettingsView(GlobalData globalData)
	{
		InitializeComponent();
        this.globalData = globalData;
        ajustesService = new SettingsService();
        ajustes.Idioma = globalData.IdiomaActual;
        ajustes.Tema = globalData.TemaActual;
    }

    private void btnTemaPredeterminado_Clicked(object sender, EventArgs e)
    {
        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries == null)
        {
            return;
        }

        ajustes.Tema = Tema.Predeterminado;
        mergedDictionaries.Clear();
        ajustesService.AplicarAjustes(ajustes);
        
    }

    private void btnTemaClaro_Clicked(object sender, EventArgs e)
    {
        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries == null)
        {
            return;
        }

        ajustes.Tema = Tema.Claro;
        mergedDictionaries.Clear();
        ajustesService.AplicarAjustes(ajustes);
    }

    private void btnTemaOscuro_Clicked(object sender, EventArgs e)
    {
        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries == null)
        {
            return;
        }

        ajustes.Tema = Tema.Oscuro;
        mergedDictionaries.Clear();
        ajustesService.AplicarAjustes(ajustes);
    }
}