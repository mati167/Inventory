using Inventory.Core.Entities.DTOs.OMDb;
using Inventory.Core.Interfaces.Gateway;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Gateway
{
    public class imdbGateway : IimdbGateway
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<imdbGateway> _logger;

        public imdbGateway(HttpClient httpClient, IConfiguration configuration, ILogger<imdbGateway> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<movieResponse> getMovie(string imdbId)
        {
            try
            {
                _logger.LogTrace($"Obteniendo película con IMDb ID: {imdbId}");

                // Obtener la API key: primero de variable de entorno, luego del appsettings
                var apiKey = Environment.GetEnvironmentVariable("OMDB_API_KEY") 
                    ?? _configuration["ExternalApis:OMDb:ApiKey"];
                var baseUrl = _configuration["ExternalApis:OMDb:BaseUrl"];

                if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(baseUrl))
                {
                    _logger.LogError("API key o BaseUrl de OMDb no configuradas");
                    throw new InvalidOperationException("OMDb API configuration is missing");
                }

                // Construir la URL
                var url = $"{baseUrl}?i={imdbId}&apikey={apiKey}";

                // Hacer el GET request
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Leer el contenido
                var jsonContent = await response.Content.ReadAsStringAsync();

                // Deserializar a movieResponse
                var options = new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true 
                };
                var movie = JsonSerializer.Deserialize<movieResponse>(jsonContent, options);

                if (movie == null || movie.Response == "False")
                {
                    _logger.LogWarning($"Película no encontrada para IMDb ID: {imdbId}");
                    return null;
                }

                _logger.LogTrace($"Película obtenida: {movie.Title}");
                return movie;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Error al hacer request a OMDb API para {imdbId}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error inesperado al obtener película {imdbId}");
                throw;
            }
        }
    }
}
