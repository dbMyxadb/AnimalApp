using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Xml.Linq;
using finallexamp.Api.Interfaces;
using finallexamp.Api.Models;
using finallexamp.Core.Interfaces;

namespace finallexamp.Api.Services
{
    public class AnimalServiceApi : IAnimalServiceApi
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService _loggerService;

        public AnimalServiceApi(HttpClient httpClient, ILoggerService loggerService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://aes.shenlu.me/");
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }


        public async Task<List<AnimalApi>> GetAllAnimalsByScientificNameAsync()
        {
            var response = _httpClient.GetAsync($"api/v1/species");
            if (!response.Result.IsSuccessStatusCode)
            {
                var error = new HttpRequestException($"Error fetching data: {response.Result.ReasonPhrase}");
                _loggerService.LogError("Failed to fetch animals by name from API.", error);
                throw error;
            }

            try
            {
                var content = await response.Result.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(content))
                {
                    _loggerService.LogWarning("Received empty response from API.");
                    return new List<AnimalApi>();
                }

                var animalsByName = JsonSerializer.Deserialize<List<AnimalApi>>(content);
                if (animalsByName == null || !animalsByName.Any())
                {
                    _loggerService.LogWarning("No animals found in the response.");
                }

                _loggerService.LogInformation($"Found {animalsByName.Count} animals in the response.");
                return animalsByName ?? new List<AnimalApi>();
            }
            catch (JsonException ex)
            {
                _loggerService.LogError($"Error deserializing response:", ex);
                return new List<AnimalApi>();
            }
        }


        public async Task<WrapperAnimal> GetAllAnimalsByNameAsync(string name)
        {
            var response = await _httpClient.GetAsync($"api/search?q={name}");

            if (!response.IsSuccessStatusCode)
            {
                var error = new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                _loggerService.LogError("Failed to fetch animals by name from API.", error);
                throw error;
            } 

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(content))
                {
                    _loggerService.LogWarning("Received empty response from API.");
                    return new WrapperAnimal();
                }

                var animalsByName = JsonSerializer.Deserialize<WrapperAnimal>(content);

                if (animalsByName?.Animals == null)
                {
                    _loggerService.LogWarning("No animals found in the response.");
                }

                _loggerService.LogInformation($"Found {animalsByName.Count} animals in the response.");
                return animalsByName;
            }
            catch (JsonException ex)
            {
                _loggerService.LogError($"Error deserializing response:", ex);
                return new WrapperAnimal();
            }
        }


        public async Task<WrapperAnimal> GetAllAnimalsByNameSortedAsync()
        {
            var response = await _httpClient.GetAsync($"api/search?sortType=common_name");

            if (!response.IsSuccessStatusCode)
            {
                var error = new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                _loggerService.LogError("Failed to fetch sorted animals by name from API.", error);
                throw error;
            }

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(content))
                {
                    _loggerService.LogWarning("Received empty response from API.");
                    return new WrapperAnimal();
                }

                var sortedAnimals = JsonSerializer.Deserialize<WrapperAnimal>(content);

                if (sortedAnimals?.Animals == null)
                {
                    _loggerService.LogWarning("No sorted animals found in the response.");
                }

                _loggerService.LogInformation($"Found {sortedAnimals.Count} sorted animals in the response.");
                return sortedAnimals;
            }
            catch (JsonException ex)
            {
                _loggerService.LogError($"Error deserializing response:", ex);
                return new WrapperAnimal();
            }
        }


        public async Task<WrapperAnimal> GetAllAnimalsByCountryCodeAsync(string isoCode)
        {
            var response = await _httpClient.GetAsync($"api/search?q={isoCode}");
            if (!response.IsSuccessStatusCode)
            {
                var error = new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                _loggerService.LogError("Failed to fetch animals by country code from API.", error);
                throw error;
            }

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(content))
                {
                    _loggerService.LogWarning("Received empty response from API.");
                    return new WrapperAnimal { Animals = new List<AnimalApi>() };
                }

                var animalsByCountry = JsonSerializer.Deserialize<WrapperAnimal>(content);
                if (animalsByCountry?.Animals == null)
                {
                    _loggerService.LogWarning("No animals found for the specified country code.");
                    return new WrapperAnimal { Animals = new List<AnimalApi>() };
                }

                var filteredAnimals = animalsByCountry.Animals
                    .Where(a => a.CountryCode?.Equals(isoCode, StringComparison.OrdinalIgnoreCase) ?? false)
                    .ToList();

                _loggerService.LogInformation($"Found {filteredAnimals.Count} animals for country code {isoCode}.");

                return new WrapperAnimal { Animals = filteredAnimals };
            }
            catch (JsonException jsonEx)
            {
                _loggerService.LogError("Error deserializing animal data.", jsonEx);
                throw;
            }
        }

    }
}
