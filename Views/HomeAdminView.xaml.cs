using BarlinkTPV.Navigation;
using BarlinkTPV.Popups;
using BarlinkTPV.Singleton;
using CommunityToolkit.Maui.Extensions;
namespace BarlinkTPV.Views;

// Vista de menú para un usuario "Encargado" (Admin)
public partial class HomeAdminView : ContentPage
{
    private readonly GlobalData globalData;
	public HomeAdminView(GlobalData globalData)
	{
		InitializeComponent();
        this.globalData = globalData;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        lblNombreUsuario.Text = globalData.NombreUsuario;
    }
    
    // Botones del menú
    // Con cada uno se accede a una vista diferente
    // Parecido a un Flyout, pero en forma de menú
    private void btnCerrarSesion_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new InitialNavigation();
    }

    private async void btnCaja_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///PosView");
    }

    private async void btnFichar_Clicked(object sender, EventArgs e)
    {
        var popup = new WorkerRegPopup(globalData);
        await this.ShowPopupAsync(popup);
    }

    private void btnApagar_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    private async void btnConfiguracion_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///SettingsView");
    }

    private async void btnUsuarios_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///UsersManagementView");
    }

    private async void btnInformeVentas_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///SalesReportsView");
    }
}