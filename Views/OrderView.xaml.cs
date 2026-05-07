using BarlinkTPV.Models;
using BarlinkTPV.Services;
using System.Collections.ObjectModel;

namespace BarlinkTPV.Views;

public partial class OrderView : ContentPage
{
	private Mesa mesaActual;
	private Ticket ticketActual;
    public ObservableCollection<LineaTicket> Lineas { get; set; } = new();

    private readonly ApiService _apiService;
	private List<Categoria> categorias = new List<Categoria>();
	private List<Producto> productos = new List<Producto>();
    private LineaTicket? lineaSeleccionada = new LineaTicket();

    public OrderView(Mesa mesa, Ticket ticket)
	{
        _apiService = new ApiService();
		InitializeComponent();
		this.mesaActual = mesa;
		this.ticketActual = ticket;
        BindingContext = this;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ObtenerLineasTicket();
        lblNumMesa.Text = mesaActual.CodigoMesa;
        lblEstadoCobro.Text = mesaActual.EstadoMesa.ToString();

        categorias = await _apiService.ObtenerCategoriasVisibles();

        foreach (var categoria in categorias)
        {
            categoria.NombreImagen = categoria.Nombre switch
            {
                "Refrescos" => "refrescos.png",
                "Bocadillos" => "bocadillos.png",
                "Cafés" => "cafes.png",
                "Desayunos" => "desayunos.png",
                "Raciones" => "raciones.png",
                "Licores" => "licores.png",
                "Cubatas" => "cubatas.png",
                "Vinos" => "vinos.png",
                "Carnes" => "carnes.png",
                "Ensaladas" => "ensaladas.png",
                "Cervezas" => "cervezas.png",
                _ => "default.png"
            };
        }

        categoriasCollection.ItemsSource = categorias;
    }
    private async void categoriasCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var categoriaSeleccionada = e.CurrentSelection.FirstOrDefault() as Categoria;
        ((CollectionView)sender).SelectedItem = null;

        if (categoriaSeleccionada == null)
            return;

        productos = await _apiService.ObtenerProductosPorCategoria(categoriaSeleccionada.Id);
        productosCollection.ItemsSource = productos;
    }

    private async void productosCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var productoSeleccionado = e.CurrentSelection.FirstOrDefault() as Producto;
        ((CollectionView)sender).SelectedItem = null;

        if(productoSeleccionado == null)
            return;

        var ticketActualizado = await _apiService.AniadirProductoLineaTicket(ticketActual.Id, productoSeleccionado.Id, 1);

        if (ticketActualizado != null)
        {
            ticketActual = ticketActualizado;
        }

        await ObtenerLineasTicket();

    }

    private async void btnEliminarProducto_Clicked(object sender, EventArgs e)
    {
        if (lineaSeleccionada != null)
        {
            await _apiService.EliminarLineaTicket(ticketActual.Id, lineaSeleccionada.ProductoId);
            await ObtenerLineasTicket();
        }
    }

    private async void btnEliminarTodo_Clicked(object sender, EventArgs e)
    {
        bool confirmacion = await DisplayAlertAsync("Eliminar todo", "¿Estás seguro de que quieres eliminar todas las líneas del ticket?", "Sí", "No");

        if (!confirmacion)
            return;
        else
        { 
            await _apiService.EliminarTodasLasLineasTicket(ticketActual.Id);
            await ObtenerLineasTicket();
        }
    }

    private void btnImprimirCuenta_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnSalir_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void btnCobrar_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnCancelarTicket_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            bool confirmacion = await DisplayAlertAsync("Cancelar Ticket", "¿Estás seguro de que quieres cancelar el ticket actual?", "Sí", "No");

            if (!confirmacion)
                return;

            var ticketEliminado = await _apiService.EliminarTicketCompleto(mesaActual.Id);

            if (ticketEliminado == null)
            {
                await DisplayAlertAsync("Error", "No se ha podido eliminar el ticket", "Aceptar");
                return;
            }
            await _apiService.CambiarEstadoMesa(mesaActual.Id, EstadoMesa.Libre);
            await DisplayAlertAsync("Correcto", "Ticket eliminado correctamente.", "Aceptar");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", ex.Message, "Aceptar");
        }
    }

    private async void btnAbrirCajon_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync("Cajón abierto", "El cajón se ha abierto correctamente", "Aceptar");
    }

    private async Task ObtenerLineasTicket()
    {
        var ticket = await _apiService.ObtenerTicketMesaActual(mesaActual.Id);
        Lineas.Clear();

        if (ticket.Lineas != null)
        {
            foreach (var linea in ticket.Lineas)
            {
                Lineas.Add(new LineaTicket
                {
                    ProductoId = linea.ProductoId,
                    NombreProducto = linea.NombreProducto,
                    PrecioUd = linea.PrecioUd,
                    Cantidad = linea.Cantidad,
                    Iva = linea.Iva
                });
            }
        }
    }

    private async void dataGridLineas_SelectionChanged(object sender, Syncfusion.Maui.DataGrid.DataGridSelectionChangedEventArgs e)
    {
        lineaSeleccionada = e.AddedRows?.FirstOrDefault() as LineaTicket;

        if (lineaSeleccionada == null)
        {
            return;
        }
    }
}