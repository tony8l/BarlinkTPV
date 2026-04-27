using CommunityToolkit.Maui.Views;

namespace BarlinkTPV.Popups;

public partial class WorkerRegPopup : Popup
{
    public string Resultado { get; set; } = "";
	public WorkerRegPopup(string ultimoFichaje)
	{
		InitializeComponent();

		if (string.IsNullOrEmpty(ultimoFichaje) || ultimoFichaje == "Salida")
        {
            btnEntrada.IsEnabled = true;
            btnSalida.IsEnabled = false;
            lblEstado.Text = "Puedes fichar ENTRADA";
        }
        else if (ultimoFichaje == "Entrada")
        {
            btnEntrada.IsEnabled = false;
            btnSalida.IsEnabled = true;
            lblEstado.Text = "Puedes fichar SALIDA";
        }
    }

    private async void btnEntrada_Clicked(object sender, EventArgs e)
    {
        Resultado = "Entrada";
        await CloseAsync();
    }

    private async void btnSalida_Clicked(object sender, EventArgs e)
    {
        Resultado = "Salida";
        await CloseAsync();
    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        Resultado = null;
        await CloseAsync();
    }
}