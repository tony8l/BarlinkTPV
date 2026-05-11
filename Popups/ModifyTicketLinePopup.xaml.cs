using BarlinkTPV.Services;
using BarlinkTPV.Models;
using CommunityToolkit.Maui.Views;

namespace BarlinkTPV.Popups;

public partial class ModifyTicketLinePopup : Popup
{
	private Ticket ticketActual;
	private LineaTicket lineaTicket;
	private readonly ApiService _apiService;

	public ModifyTicketLinePopup(Ticket ticket, LineaTicket lineaTicket)
	{
		InitializeComponent();
		this.ticketActual = ticket;
		this.lineaTicket = lineaTicket;
		_apiService = new ApiService();
		entryCantidadProducto.Text = lineaTicket.Cantidad.ToString();
		entryNombreProducto.Text = lineaTicket.NombreProducto;
    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
		await CloseAsync();
    }

    private async void btnConfirmar_Clicked(object sender, EventArgs e)
    {
		if (lineaTicket != null)
		{
            await _apiService.EditarCantidadLineaTicket(ticketActual.Id,lineaTicket.ProductoId, int.Parse(entryCantidadProducto.Text));
            await CloseAsync();
        }
		
    }
}