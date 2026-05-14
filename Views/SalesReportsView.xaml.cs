using BarlinkTPV.Models;
using BarlinkTPV.Services;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;

namespace BarlinkTPV.Views;

public partial class SalesReportsView : ContentPage
{
    // Colección para cargar los datos en el Grid
    public ObservableCollection<Cobro> Cobros { get; set; } = new();
    // Colección para cargar los datos ne el gráfico
    public ObservableCollection<InformeVentasChart> DatosGrafico { get; set; } = new();
    // Colección para cargar los colores en el gráfico
    public ObservableCollection<Brush> CustomBrushes { get; set; } = new();
    private ApiService _apiService;
    private DateTime fechaSeleccionada;
	public SalesReportsView()
	{
		InitializeComponent();
        _apiService = new ApiService();
        // Añadimos los colores para cambiarlos en las 2 barras del gráfico
        CustomBrushes.Add(new SolidColorBrush(Color.FromArgb("#1f57ff")));
        CustomBrushes.Add(new SolidColorBrush(Color.FromArgb("#ff0000")));
        BindingContext = this;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Al cargar la pantalla, cargamos los datos de ventas del periodo del día en específico, desde las 00:00 hasta las 23:59
        await ObtenerCobros(DateTime.Now.Date);
        var ventas = await _apiService.ObtenerInformeVentas(DateTime.Now);
        await CargarInforme(DateTime.Now.Date);

        // Mostramos en la barra inferior el informe de ventas general
        lblImporteTotal.Text = string.Format(Traducir("SalesReportsView_TotalVentas"), ventas.TotalCaja);
        lblImporteTarjeta.Text = string.Format(Traducir("SalesReportsView_TotalTarjeta"), ventas.TotalTarjeta);
        lblImporteEfectivo.Text = string.Format(Traducir("SalesReportsView_TotalEfectivo"), ventas.TotalEfectivo);
    }

    // Método que carga los datos en el gráfico
    private async Task CargarInforme(DateTime diaSeleccionado)
    {
        var informe = await _apiService.ObtenerInformeVentas(diaSeleccionado);

        DatosGrafico.Clear();

        DatosGrafico.Add(new InformeVentasChart
        {
            Categoria = "Efectivo",
            Importe = informe.TotalEfectivo
        });

        DatosGrafico.Add(new InformeVentasChart
        {
            Categoria = "Tarjeta",
            Importe = informe.TotalTarjeta
        });
    }

    // Método que carga los datos en el Grid
    private async Task ObtenerCobros(DateTime diaSeleccionado)
    {
        var cobros = await _apiService.ObtenerCobrosDia(diaSeleccionado);
        
        Cobros.Clear();

        if (cobros != null)
        {
            foreach (var linea in cobros)
            {
                Cobros.Add(new Cobro
                {
                    Id =linea.Id,
                    TicketId = linea.TicketId,
                    CodigoTicket = linea.CodigoTicket,
                    EmpleadoId = linea.EmpleadoId,
                    MetodoPago = linea.MetodoPago,
                    ImporteTotal = linea.ImporteTotal,
                    ImporteEntregado = linea.ImporteEntregado,
                    Devolucion = linea.Devolucion,
                    FechaCobro = linea.FechaCobro
                });
            }
        }
    }

    private async void datePickerInforme_DateSelected(object sender, DateChangedEventArgs e)
    {
        fechaSeleccionada = (DateTime) ((DatePicker)sender).Date;
        await ObtenerCobros(fechaSeleccionada);
        var ventas = await _apiService.ObtenerInformeVentas(fechaSeleccionada);
        await CargarInforme(fechaSeleccionada);

        // Mostramos en la barra inferior el informe de ventas general
        lblImporteTotal.Text = string.Format(Traducir("SalesReportsView_TotalVentas"), ventas.TotalCaja);
        lblImporteTarjeta.Text = string.Format(Traducir("SalesReportsView_TotalTarjeta"), ventas.TotalTarjeta);
        lblImporteEfectivo.Text = string.Format(Traducir("SalesReportsView_TotalEfectivo"), ventas.TotalEfectivo);
    }

    private string Traducir(string clave)
    {
        if (Application.Current?.Resources.TryGetValue(clave, out var valor) == true)
            return valor?.ToString() ?? string.Empty;

        return clave;
    }
}