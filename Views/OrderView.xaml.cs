using BarlinkTPV.Models;
using BarlinkTPV.Services;

namespace BarlinkTPV.Views;

public partial class OrderView : ContentPage
{
	private Mesa mesa;
	private Ticket ticket;

    private readonly ApiService _apiService;
	private List<Categoria> categorias = new List<Categoria>();
	private List<Producto> productos = new List<Producto>();

    public OrderView(Mesa mesa, Ticket ticket)
	{
        _apiService = new ApiService();
		InitializeComponent();
		this.mesa = mesa;
		this.ticket = ticket;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        categorias = await _apiService.ObtenerCategorias();
        productos = await _apiService.ObtenerProductos();

        categoriasCollection.ItemsSource = categorias;
        productosCollection.ItemsSource = productos;
    }
    private void categoriasCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void productosCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}