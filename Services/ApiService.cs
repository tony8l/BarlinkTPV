using BarlinkTPV.Models;
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
                BaseAddress = new Uri("https://tu-api.com/api/")
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
    }
}
