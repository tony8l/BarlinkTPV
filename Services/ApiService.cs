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

        // CADENA DE CONEXIÓN INICIAL
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

        #region Métodos de autenticación y fichajes de usuario
        // Método para iniciar sesión utilizando el DNI del usuario
        public async Task<Usuario?> IniciarSesion(string dni)
        {
            var response = await _httpClient.GetAsync($"usuarios/dni/{dni}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Usuario>(json, _jsonOptions);
        }

        // Método para cargar el último tipo de fichaje de un empleado utilizando su DNI
        public async Task<Fichaje?> ObtenerUltimoFichaje(string dni)
        {
            var response = await _httpClient.GetAsync($"fichajes/dni/{dni}/ultimoFichaje");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Fichaje>(json, _jsonOptions);
        }

        // Método para fichar la entrada de un empleado utilizando su ID
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

        // Método para fichar la salida de un empleado utilizando su ID
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

        // Método para obtener los usuarios
        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            var response = await _httpClient.GetAsync($"usuarios");
            if (!response.IsSuccessStatusCode)
                return new List<Usuario>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Usuario>>(json, _jsonOptions) ?? new List<Usuario>();
        }

        // Método para crear un usuario
        public async Task<Usuario?> CrearUsuario(string dni, string nombre, string apellidos, RolUsuario rolUsuario, bool activado)
        {
            var request = new CrearUsuarioDto
            {
                Dni = dni,
                Nombre = nombre,
                Apellidos = apellidos,
                Rol = rolUsuario,
                Activado = activado,
            };

            var response = await _httpClient.PostAsJsonAsync("usuarios/crear", request);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Usuario>(_jsonOptions);
        }

        // Método para editar un usuario
        public async Task<bool> EditarUsuario(string usuarioId, string dni, string nombre, string apellidos, RolUsuario rolUsuario, bool activo)
        {
            var request = new ActualizarUsuarioDto
            {
                Dni = dni,
                Nombre = nombre,
                Apellidos = apellidos,
                Rol = rolUsuario,
                Activado = activo
            };
            var response = await _httpClient.PatchAsJsonAsync($"usuarios/{usuarioId}", request);

            return response.IsSuccessStatusCode;
        }
        #endregion
        #region Métodos de gestión de mesas
        // Método para obtener la lista de mesas disponibles
        public async Task<List<Mesa>> ObtenerMesas()
        {
            var response = await _httpClient.GetAsync($"mesas");

            if (!response.IsSuccessStatusCode)
                return new List<Mesa>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Mesa>>(json, _jsonOptions) ?? new List<Mesa>();
        }

        // Método para cambiar el estado de una mesa
        public async Task<bool> CambiarEstadoMesa(string mesaId, EstadoMesa nuevoEstado)
        {
            var request = new ActualizarMesaDto
            {
                EstadoMesa = nuevoEstado
            };

            var response = await _httpClient.PatchAsJsonAsync($"mesas/{mesaId}", request);

            return response.IsSuccessStatusCode;
        }

        // Método para obtener el ticket activo de una mesa
        public async Task<Ticket?> ObtenerTicketMesaActual(string mesaId)
        {
            var response = await _httpClient.GetAsync($"mesas/{mesaId}/ticketActivo");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Ticket>(json, _jsonOptions);
        }
        #endregion
        #region Métodos categorías/productos VER+TICAR
        // Método para obtener todas las categorías visibles
        public async Task<List<Categoria>> ObtenerCategoriasVisibles()
        {
            var response = await _httpClient.GetAsync($"categorias/visibles");

            if (!response.IsSuccessStatusCode)
                return new List<Categoria>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Categoria>>(json, _jsonOptions) ?? new List<Categoria>();
        }

        // Método para obtener todos los productos según su categoría visibles
        public async Task<List<Producto>> ObtenerProductosPorCategoria(string categoriaId)
        {
            var response = await _httpClient.GetAsync($"productos/categoria/{categoriaId}");
            if (!response.IsSuccessStatusCode)
                return new List<Producto>();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Producto>>(json, _jsonOptions) ?? new List<Producto>();
        }
        #endregion
        #region Métodos de gestión de tickets
        // Método para abrir un nuevo ticket en una mesa
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

        // Método para obtener todos los tickets
        public async Task<List<Ticket>> ObtenerTickets()
        {
            var response = await _httpClient.GetAsync($"tickets");

            if (!response.IsSuccessStatusCode)
                return new List<Ticket>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Ticket>>(json, _jsonOptions) ?? new List<Ticket>();
        }

        // Método para ticar un producto en un ticket
        public async Task<Ticket?> AniadirProductoLineaTicket(string ticketId, string productoId, int cantidad)
        {
            var request = new CrearLineaTicketDto
            {
                ProductoId = productoId,
                Cantidad = cantidad
            };

            var response = await _httpClient.PostAsJsonAsync($"tickets/id/{ticketId}/lineas", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Ticket>(_jsonOptions);
        }

        // Método para eliminar un ticket de una mesa
        public async Task<Ticket?> EliminarTicketCompleto(string mesaId)
        {
            var response = await _httpClient.DeleteAsync($"tickets/eliminar/mesaId/{mesaId}");
            if (!response.IsSuccessStatusCode)
                return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Ticket>(json, _jsonOptions);
        }

        // Método para eliminar un producto del ticket
        public async Task<Ticket?> EliminarLineaTicket(string ticketId, string productoId)
        {
            var response = await _httpClient.DeleteAsync($"tickets/id/{ticketId}/lineas/{productoId}/eliminar");
            if (!response.IsSuccessStatusCode)
                return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Ticket>(json, _jsonOptions);
        }

        // Método para eliminar todas las lineas de un ticket (reiniciar ticket)
        public async Task<bool> EliminarTodasLasLineasTicket(string ticketId)
        {
            var response = await _httpClient.DeleteAsync($"tickets/id/{ticketId}/lineas/eliminarLineas");
            if (!response.IsSuccessStatusCode)
                return false;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(json, _jsonOptions);
        }

        // Método para editar la cantidad de un producto en una línea del ticket
        public async Task<Ticket?> EditarCantidadLineaTicket(string ticketId, string productoId, int nuevaCantidad)
        {
            var request = new ActualizarLineaTicketDto
            {
                Cantidad = nuevaCantidad
            };
            var response = await _httpClient.PatchAsJsonAsync($"tickets/id/{ticketId}/lineas/{productoId}", request);
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<Ticket>(_jsonOptions);
        }

        // Método para cobrar un ticket
        public async Task<Cobro> CobrarTicket(string ticketId, string empleadoId, MetodoPago metodoPago, decimal importeEntregado)
        {
            var request = new CrearCobroDto
            {
                TicketId = ticketId,
                EmpleadoId = empleadoId,
                MetodoPago = metodoPago,
                ImporteEntregado = importeEntregado
            };

            var response = await _httpClient.PostAsJsonAsync("cobros/crear", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Cobro>(_jsonOptions);
        }
        #endregion
        #region Métodos de ajustes
        public async Task<Ajustes?> CrearAjustes(string usuarioId)
        {
            var request = new CrearAjusteDto
            {
                UsuarioId = usuarioId,
                Tema = Tema.Predeterminado,
                Idioma = Idioma.ES
            };

            var response = await _httpClient.PostAsJsonAsync("ajustes/crear", request);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Ajustes>(_jsonOptions);
        }

        public async Task<Ajustes?> ObtenerAjustesUsuario(string usuarioId)
        {
            var response = await _httpClient.GetAsync($"ajustes/{usuarioId}");
            if (!response.IsSuccessStatusCode)
                return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Ajustes>(json, _jsonOptions);
        }

        public async Task<bool> EditarAjustes(string ajustesId, Tema tema, Idioma idioma)
        {
            var request = new ActualizarAjustesDto
            {
                Tema = tema,
                Idioma = idioma
            };
            var response = await _httpClient.PatchAsJsonAsync($"ajustes/{ajustesId}", request);

            return response.IsSuccessStatusCode;
        }
        #endregion
    }
}
