using BarlinkTPV.Singleton;
using BarlinkTPV.Views;

namespace BarlinkTPV.Navigation;

public partial class AdminNavigation : Shell
{
	private readonly GlobalData globalData;
	public AdminNavigation(GlobalData globalData)
	{
		InitializeComponent();
		this.globalData = globalData;
	}

    private void btnSalir_Clicked(object sender, EventArgs e)
    {
		globalData.CerrarSesion();
		Application.Current.MainPage = new InitialNavigation();
    }
}