using BarlinkTPV.Models;
using BarlinkTPV.Models.DTOs;
using BarlinkTPV.Popups;
using BarlinkTPV.Services;
using BarlinkTPV.Singleton;
using CommunityToolkit.Maui.Extensions;
using System.Collections.ObjectModel;
using System.Globalization;

namespace BarlinkTPV.Views;

public partial class OrderView : ContentPage
{
    private GlobalData globalData;

    // Variables para guardar el estado actual recibido por el constructor
	private Mesa mesaActual;
	private Ticket ticketActual;

    // Colección observable para poder actualizar el DataGrid
    public ObservableCollection<LineaTicket> Lineas { get; set; } = new();

    // Servicio para realizar las llamadas a la API
    private readonly ApiService _apiService;

    // Listas para guardar los datos recibidos desde la API
	private List<Categoria> categorias = new List<Categoria>();
	private List<Producto> productos = new List<Producto>();

    // Variable para guardar la línea seleccionada en el DataGrid con el evento SelectionChanged
    private LineaTicket? lineaSeleccionada = new LineaTicket();

    private string cantidadNumpad = "";
    public OrderView(Mesa mesa, Ticket ticket, GlobalData globalData)
	{
        _apiService = new ApiService();
		InitializeComponent();
		this.mesaActual = mesa;
		this.ticketActual = ticket;
        this.globalData = globalData;
        lineaSeleccionada = null;
        BindingContext = this;
	}

    // Sobreescribimos elmétodo OnAppearing para cargar todos los datos necesarios
    // Se carga una información de la mesa, tanto el código de la mesa como el estado de ella
    // Se cargan las categorías disponibles y se asigna una imagen a cada categoría dependiendo de su nombre
    // Se asignan los datos a la CollectioNView de las categorías para poder mostrarlas
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ObtenerLineasTicket();
        lblNumMesa.Text = mesaActual.CodigoMesa;
        if (globalData.IdiomaActual == Idioma.ENG)
        {
            lblEstadoCobro.Text = mesaActual.EstadoMesa switch
            {
                EstadoMesa.Libre => "Free",
                EstadoMesa.Ocupada => "Occupied",
                _ => mesaActual.EstadoMesa.ToString()
            };
        }
        else if (globalData.IdiomaActual == Idioma.ES)
        {
            lblEstadoCobro.Text = mesaActual.EstadoMesa.ToString();
        }
        categorias = await _apiService.ObtenerCategoriasVisibles();

        // Asignamos una imagen a cada categoría
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

            if (globalData.IdiomaActual == Idioma.ENG)
            {
                categoria.Nombre = categoria.Nombre switch
                {
                    "Refrescos" => "Soft Drinks",
                    "Bocadillos" => "Sandwiches",
                    "Cafés" => "Coffee",
                    "Desayunos" => "Breakfast",
                    "Raciones" => "Sharing Plates",
                    "Licores" => "Liqueurs",
                    "Cubatas" => "Mixed Drinks",
                    "Vinos" => "Wines",
                    "Carnes" => "Meat Dishes",
                    "Ensaladas" => "Salads",
                    "Cervezas" => "Beers",
                    _ => categoria.Nombre
                };
            }
        }
        categoriasCollection.ItemsSource = categorias;
    }

    // Método que muestra las categorías disponibles (esVisible = true)
    // Después se muestran los productos de esa categoría disponibles (esVisible = true)
    private async void categoriasCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var categoriaSeleccionada = e.CurrentSelection.FirstOrDefault() as Categoria;
        ((CollectionView)sender).SelectedItem = null;

        if (categoriaSeleccionada == null)
            return;

        productos = await _apiService.ObtenerProductosPorCategoria(categoriaSeleccionada.Id);

        // Asignamos una imagen a cada producto
        foreach (var producto in productos)
        {
            producto.NombreImagen = producto.Nombre switch
            {
                "Bocadillo Chivito" => "chivito.webp",
                "Bocadillo Lomo-Queso" => "lomoqueso.png",
                "Bocadillo Serranito" => "serranito.png",
                "Montado Lomo-Queso" => "lomoqueso.png",
                "Montado Serranito" => "serranito.png",
                "Café Expresso" => "expresso.png",
                "Café Cortado" => "cafeleche.webp",
                "Café Americano" => "americano.png",
                "Café Bombon" => "bombon.png",
                "Chuletón Vaca" => "chuleton.png",
                "Entrecot" => "entrecot.webp",
                "Chuletón Wagyu" => "wagyu.webp",
                "Caña" => "cania.png",
                "Jarra" => "jarralitro.png",
                "Tercio" => "tercio.png",
                "Copa Barceló" => "barcelo.jpg",
                "Copa JB" => "jb.png",
                "Copa Larios" => "larios.webp",
                "Copa Beefeater" => "beefeater.webp",
                "Croissant" => "croissant.png",
                "Napolitana" => "napolitana.png",
                "Zumo Naranja" => "zumonaranja.png",
                "Zumo Melocotón" => "zumomelocoton.png",
                "Chupito Hierbas" => "licorhierbas.png",
                "Chupito Orujo" => "orujo.png",
                "Patatas Bravas" => "patatasbravas.png",
                "Rabo Frito" => "rabofrito.png",
                "Calamares" => "rcalamares.bmp",
                "Surtido Ibérico" => "surtidoibericos.bmp",
                "Agua" => "agua.png",
                "Coca Cola" => "cocacola.png",
                "Fanta Naranja" => "fantanaranja.png",
                "Fanta Limón" => "fantalimon.png",
                "Copa Vino Blanco" => "copablanco.png",
                "Copa Vino Tinto" => "copatinto.webp",
                "Botella Vino Blanco" => "botvinoblanco.png",
                "Botella Vino Tinto" => "botvinotinto.png",
                _ => "default.png"
            };

            if (globalData.IdiomaActual == Idioma.ENG)
            {
                producto.Nombre = producto.Nombre switch
                {
                    "Bocadillo Chivito" => "Chivito Large Sandwich",
                    "Bocadillo Lomo-Queso" => "Pork Loin & Cheese Large Sandwich",
                    "Bocadillo Serranito" => "Serranito Large Sandwich",
                    "Montado Lomo-Queso" => "Pork Loin & Cheese Small Sandwich",
                    "Montado Serranito" => "Serranito Small Sandwich",
                    "Café Expresso" => "Espresso Coffee",
                    "Café Cortado" => "Cortado Coffee",
                    "Café Americano" => "Americano Coffee",
                    "Café Bombon" => "Bombon Coffee",
                    "Chuletón Vaca" => "Beef Rib Steak (Bone-in)",
                    "Entrecot" => "Ribeye Steak (Boneless)",
                    "Chuletón Wagyu" => "Wagyu Rib Steak (Bone-in)",
                    "Caña" => "Small Beer Draft",
                    "Jarra" => "Beer Pitcher",
                    "Tercio" => "Tercio Beer (1/3 Liter)",
                    "Copa Barceló" => "Barceló Rum Glass",
                    "Copa JB" => "JB Whiskey Glass",
                    "Copa Larios" => "Larios Gin Glass",
                    "Copa Beefeater" => "Beefeater Gin Glass",
                    "Croissant" => "Croissant",
                    "Napolitana" => "Napolitana Pastry",
                    "Zumo Naranja" => "Fresh Orange Juice",
                    "Zumo Melocotón" => "Fresh Peach Juice",
                    "Chupito Hierbas" => "Herbal Liqueur Shot",
                    "Chupito Orujo" => "Orujo Liqueur Shot",
                    "Patatas Bravas" => "Patatas Bravas",
                    "Rabo Frito" => "Fried Oxtail",
                    "Calamares" => "Fried Calamari",
                    "Surtido Ibérico" => "Iberian Charcuterie Assortment",
                    "Agua" => "Water",
                    "Coca Cola" => "Coca Cola",
                    "Fanta Naranja" => "Fanta Orange",
                    "Fanta Limón" => "Fanta Lemon",
                    "Copa Vino Blanco" => "White Wine Glass",
                    "Copa Vino Tinto" => "Red Wine Glass",
                    "Botella Vino Blanco" => "White Wine Bottle",
                    "Botella Vino Tinto" => "Red Wine Bottle",
                    _ => producto.Nombre
                };
            }
        }
        productosCollection.ItemsSource = productos;
    }

    // Método que añade una unidad del producto que se ha seleccionado
    // Se añade a la colección de "lineaTicket" y se muestra en el DatGrid
    private async void productosCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var productoSeleccionado = e.CurrentSelection.FirstOrDefault() as Producto;
        ((CollectionView)sender).SelectedItem = null;

        if(productoSeleccionado == null)
            return;

        int cantidad;
        if (string.IsNullOrEmpty(cantidadNumpad))
        {
            cantidad = 1;
        }
        else
        {
            cantidad = int.Parse(cantidadNumpad);
        }

        var ticketActualizado = await _apiService.AniadirProductoLineaTicket(ticketActual.Id, productoSeleccionado.Id, cantidad);

        if (ticketActualizado != null)
        {
            ticketActual = ticketActualizado;
        }

        cantidadNumpad = "";
        lblCantidadValue.Text = "1";
        await ObtenerLineasTicket();

    }

    // Método que elimina la línea completa del producto seleccionado
    private async void btnEliminarProducto_Clicked(object sender, EventArgs e)
    {
        if (lineaSeleccionada != null)
        {
            await _apiService.EliminarLineaTicket(ticketActual.Id, lineaSeleccionada.ProductoId);
            await ObtenerLineasTicket();
            lineaSeleccionada = null;
        }
    }

    // Método que elimina todas las líneas del ticket actual
    // IMPORTANTE: Las líneas se eliminan pero el ticket sigue existiendo
    private async void btnEliminarTodo_Clicked(object sender, EventArgs e)
    {
        bool confirmacion = await DisplayAlertAsync("Eliminar todo", "¿Estás seguro de que quieres eliminar todas las líneas del ticket?", "Sí", "No");

        if (!confirmacion)
            return;
        else
        { 
            await _apiService.EliminarTodasLasLineasTicket(ticketActual.Id);
            await ObtenerLineasTicket();
            lineaSeleccionada = null;
        }
    }

    // Método que vuelve para el menú de las mesas
    private async void btnSalir_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void btnCobrar_Clicked(object sender, EventArgs e)
    {
        try
        {
            decimal importeTotal = ticketActual.Total;

            string metodoSeleccionado = await DisplayActionSheetAsync(
                "Selecciona el método de pago",
                "Cancelar",
                null,
                "Efectivo",
                "Tarjeta"
            );

            if (string.IsNullOrWhiteSpace(metodoSeleccionado) || metodoSeleccionado == "Cancelar")
                return;

            MetodoPago metodoPago = metodoSeleccionado switch
            {
                "Efectivo" => MetodoPago.Efectivo,
                "Tarjeta" => MetodoPago.Tarjeta,
                _ => throw new Exception("Método de pago no válido")
            };

            decimal entregado = importeTotal;

            if (metodoPago == MetodoPago.Efectivo)
            {
                string resul = await DisplayPromptAsync(
                    "Cobrar Ticket",
                    "Introduce el importe entregado por el cliente:",
                    "Aceptar",
                    "Cancelar",
                    "Ej: 10,75",
                    keyboard: Keyboard.Numeric
                );

                if (string.IsNullOrWhiteSpace(resul))
                    return;

                string valorNormalizado = resul.Replace('.', ',');

                if (!decimal.TryParse(valorNormalizado, NumberStyles.Any, CultureInfo.CurrentCulture, out entregado))
                {
                    await DisplayAlertAsync("Error", "Introduce un importe válido", "Aceptar");
                    return;
                }

                if (entregado < importeTotal)
                {
                    await DisplayAlertAsync("Error", "El importe entregado no puede ser menor que el total", "Aceptar");
                    return;
                }
            }

            var dto = new CrearCobroDto
            {
                TicketId = ticketActual.Id,
                EmpleadoId = globalData.IdUsuario,
                MetodoPago = metodoPago,
                ImporteEntregado = entregado
            };

            var cobroCreado = await _apiService.CobrarTicket(ticketActual.Id, globalData.IdUsuario, metodoPago, entregado);

            if (cobroCreado == null)
            {
                await DisplayAlertAsync("Error", "No se ha podido registrar el cobro", "Aceptar");
                return;
            }

            string resumen =
                $"Método de pago: {cobroCreado.MetodoPago}\n" +
                $"Importe total: {cobroCreado.ImporteTotal:N2} €\n" +
                $"Importe entregado: {cobroCreado.ImporteEntregado:N2} €\n" +
                $"Devolución: {cobroCreado.Devolucion:N2} €\n" +
                $"Fecha: {cobroCreado.FechaCobro:dd/MM/yyyy HH:mm}";

            await DisplayAlertAsync("Cobro realizado", resumen, "Aceptar");

            await _apiService.CambiarEstadoMesa(mesaActual.Id, EstadoMesa.Libre);
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", ex.Message, "Aceptar");
        }
    }

    // Método que elimina el ticket de la mesa actual y deja la mesa en un estado "LIBRE"
    // IMPORTANTE: Se elimina el ticket totalmente y se pierde todo el historial 
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

    // Método que simula la apertura del cajón del dinero
    private async void btnAbrirCajon_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync("Cajón abierto", "El cajón se ha abierto correctamente", "Aceptar");
    }

    // Evento que gestiona el cambio de linea en el DataGrid
    // Se guarda la ínea seleccionada de la tabla
    private async void dataGridLineas_SelectionChanged(object sender, Syncfusion.Maui.DataGrid.DataGridSelectionChangedEventArgs e)
    {
        lineaSeleccionada = e.AddedRows?.FirstOrDefault() as LineaTicket;

        if (lineaSeleccionada == null)
        {
            return;
        }
    }

    private async void btnEditarLinea_Clicked(object sender, EventArgs e)
    {
        if (lineaSeleccionada == null)
        {
            await DisplayAlertAsync("Error", "No se ha seleccionado ninguna línea", "Aceptar");
            return;
        }
        else
        {
            var popup = new ModifyTicketLinePopup(ticketActual, lineaSeleccionada);
            await this.ShowPopupAsync(popup);
            lineaSeleccionada = null;
        }
        
    }

    private async void NumpadButton_Clicked(object sender, EventArgs e)
    {
        if (sender is not Button boton)
            return;

        string valorNumpad = boton.Text;
        string auxValorNumpad;

        if (string.IsNullOrEmpty(cantidadNumpad))
        {
            auxValorNumpad = valorNumpad;
        }
        else
        {
            auxValorNumpad = cantidadNumpad + valorNumpad;
        }

        if (!int.TryParse(auxValorNumpad, out int nuevoValor))
        {
            return;
        }

        if (nuevoValor < 1 || nuevoValor > 20)
        {
            await DisplayAlertAsync("Error", "La cantidad debe estar entre 1 y 20", "Aceptar");

            cantidadNumpad = "";
            lblCantidadValue.Text = "1";
            return;
        }

        cantidadNumpad = auxValorNumpad;
        lblCantidadValue.Text = cantidadNumpad;
    }

    private void numPadBorrar_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cantidadNumpad))
        {
            cantidadNumpad = "";
            lblCantidadValue.Text = "1";
            return;
        }

        cantidadNumpad = cantidadNumpad.Substring(0, cantidadNumpad.Length - 1);

        if (string.IsNullOrEmpty(cantidadNumpad))
        {
            cantidadNumpad = "";
            lblCantidadValue.Text = "1";
        }
        else
        {
            lblCantidadValue.Text = cantidadNumpad;
        }
    }

    // Método que actualiza las líneas de la tabla para que se muestren actualizadas al añadir o eliminar una línea
    private async Task ObtenerLineasTicket()
    {
        var ticket = await _apiService.ObtenerTicketMesaActual(mesaActual.Id);
        ticketActual = ticket;
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

        ActualizarInfoTicket();
    }

    private void ActualizarInfoTicket()
    {
        lblTotalIvaValue.Text = $"{ticketActual.TotalIva:N2} €";
        lblImporteTotalValue.Text = $"{ticketActual.Total:N2} €";
    }
}