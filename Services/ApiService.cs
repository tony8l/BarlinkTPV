using BarlinkTPV.Models;
using BarlinkTPV.Models.DTOs;
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
    }
}
