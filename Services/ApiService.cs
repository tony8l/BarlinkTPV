using BarlinkTPV.Models;
using BarlinkTPV.Models.DTOs;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BarlinkTPV.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7002/api/")
            };

            // Configuras las opciones una sola vez en el constructor
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _jsonOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public async Task<Usuario?> IniciarSesion(string dni)
        {
            var response = await _httpClient.GetAsync($"usuarios/dni/{dni}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Usuario>(json, _jsonOptions);
        }

        public async Task<Fichaje?> ObtenerUltimoFichaje(string dni)
        {
            var response = await _httpClient.GetAsync($"fichajes/dni/{dni}/ultimoFichaje");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Fichaje>(json, _jsonOptions);
        }

        public async Task<Fichaje?> FicharEntrada(string empleadoId)
        {
            var request = new CrearFichajeDto
            {
                EmpleadoId = empleadoId
            };

            var response = await _httpClient.PostAsJsonAsync($"fichajes/crear/entrada", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Fichaje>(_jsonOptions);
        }

        public async Task<Fichaje?> FicharSalida(string empleadoId)
        {
            var request = new CrearFichajeDto
            {
                EmpleadoId = empleadoId
            };

            var response = await _httpClient.PostAsJsonAsync($"fichajes/crear/salida", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Fichaje>(_jsonOptions);
        }

        public async Task<List<Mesa>> ObtenerMesas()
        { 
            var response = await _httpClient.GetAsync($"mesas");

            if (!response.IsSuccessStatusCode)
                return new List<Mesa>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Mesa>>(json, _jsonOptions) ?? new List<Mesa>();
        }

        public async Task<bool> CambiarEstadoMesa(string mesaId, EstadoMesa nuevoEstado)
        {
            var request = new ActualizarMesaDto
            {
                EstadoMesa = nuevoEstado
            };

            var response = await _httpClient.PatchAsJsonAsync($"mesas/{mesaId}", request);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Ticket>> ObtenerTickets()
        {
            var response = await _httpClient.GetAsync($"tickets");

            if (!response.IsSuccessStatusCode)
                return new List<Ticket>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Ticket>>(json, _jsonOptions) ?? new List<Ticket>();
        }

        public async Task<Ticket?> ObtenerTicketMesaActual(string mesaId)
        {
            var response = await _httpClient.GetAsync($"mesas/{mesaId}/ticketActivo");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Ticket>(json, _jsonOptions);
        }

        public async Task<Ticket?> AbrirTicket(string mesaId)
        {
            var request = new CrearTicketDto
            {
                MesaId = mesaId
            };

            var response = await _httpClient.PostAsJsonAsync("tickets/crear", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Ticket>(_jsonOptions);
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            var response = await _httpClient.GetAsync($"productos/visibles");

            if (!response.IsSuccessStatusCode)
                return new List<Producto>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Producto>>(json, _jsonOptions) ?? new List<Producto>();
        }

        public async Task<List<Categoria>> ObtenerCategorias()
        {
            var response = await _httpClient.GetAsync($"categorias/visibles");

            if (!response.IsSuccessStatusCode)
                return new List<Categoria>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Categoria>>(json, _jsonOptions) ?? new List<Categoria>();
        }
    }
}
