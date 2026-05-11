using BarlinkTPV.Services;
using BarlinkTPV.Models;
using BarlinkTPV.Singleton;

namespace BarlinkTPV.Views;

public partial class PosView : ContentPage
{
    private readonly ApiService _apiService;
    private List<Mesa> mesas = new List<Mesa>();
    private GlobalData globalData;

    public PosView(GlobalData globalData)
    {
        InitializeComponent();
        _apiService = new ApiService();
        this.globalData = globalData;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        mesas = await _apiService.ObtenerMesas();
        List<Mesa> mesasSala = mesas
            .Where(m => m.CategoriaMesa == "Sala")
            .Where(m => m.Bloqueada == false)
            .ToList();

        collectionMesasSala.ItemsSource = mesasSala;

        List<Mesa> mesasTerraza = mesas
            .Where(m => m.CategoriaMesa == "Terraza")
            .Where(m => m.Bloqueada == false)
            .ToList();

        collectionMesasTerraza.ItemsSource = mesasTerraza;
    }

    private async void collectionMesasSala_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Mesa? mesaSeleccionada = e.CurrentSelection.FirstOrDefault() as Mesa;
        ((CollectionView)sender).SelectedItem = null;

        if (mesaSeleccionada == null)
        {
            return;
        }
        await MesaSeleccionadaEvents(mesaSeleccionada);
    }

    private async void collectionMesasTerraza_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Mesa? mesaSeleccionada = e.CurrentSelection.FirstOrDefault() as Mesa;
        ((CollectionView)sender).SelectedItem = null;

        if (mesaSeleccionada == null)
        {
            return;
        }
        await MesaSeleccionadaEvents(mesaSeleccionada);
    }

    public async Task MesaSeleccionadaEvents(Mesa mesaSeleccionada)
    {
        if (mesaSeleccionada.EstadoMesa == EstadoMesa.Libre)
        {
            Ticket? ticket = await _apiService.AbrirTicket(mesaSeleccionada.Id);
            if (ticket == null)
            {
                await DisplayAlertAsync("Error", "No se ha encontrado un ticket disponible en esta mesa", "Aceptar");
                return;
            }
            bool estadoMesa = await _apiService.CambiarEstadoMesa(mesaSeleccionada.Id, EstadoMesa.Ocupada);

            if (!estadoMesa)
            {
                await DisplayAlertAsync("Error", "No se ha podido cambiar el estado de la mesa", "Aceptar");
                return;
            }

            mesaSeleccionada.EstadoMesa = EstadoMesa.Ocupada;
            await Navigation.PushAsync(new OrderView(mesaSeleccionada, ticket, globalData));
        }

        else
        {
            Ticket ticket = await _apiService.ObtenerTicketMesaActual(mesaSeleccionada.Id);

            if (ticket == null)
            {
                await DisplayAlertAsync("Error", "No se ha encontrado un ticket disponible en esta mesa", "Aceptar");
                return;
            }

            await Navigation.PushAsync(new OrderView(mesaSeleccionada, ticket, globalData));
        }
    }
}