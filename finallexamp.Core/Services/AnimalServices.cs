

using finallexamp.Core.Interfaces;
using finallexamp.Core.Models;

namespace finallexamp.Core.Services
{
    public class AnimalServices : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ILoggerService _loggerService;

        public AnimalServices(IAnimalRepository animalRepository, ILoggerService loggerService)
        {
            _animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
            _loggerService = loggerService ?? throw new ArgumentNullException(nameof(loggerService));
        }

        public async Task<List<Animal>> GetAllAnimalsByScientificNameAsync()
        {
            try
            {
                var animals = await _animalRepository.GetAllAsync();
                if (animals == null || !animals.Any())
                {
                    _loggerService.LogWarning("No animals found in the database.");
                    return new List<Animal>();
                }
                
                _loggerService.LogInformation($"Found {animals.Count} animals in the database.");
                return animals;
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error fetching animals: {ex.Message}", ex);
                throw;
            }
        }
        public async Task<List<Animal>> GetAllAnimalsByNameAsync(string animalName)
        {
            try
            {
                var animals = await _animalRepository.GetAllAsync();
                if (animals == null || !animals.Any())
                {
                    _loggerService.LogWarning("No animals found in the database.");
                    return new List<Animal>();
                }
                var filteredAnimals = animals.Where(a => a.Name.Contains(animalName, StringComparison.OrdinalIgnoreCase)).ToList();
                if (!filteredAnimals.Any())
                {
                    _loggerService.LogWarning($"No animals found with name containing '{animalName}'.");
                    return new List<Animal>();
                }
                _loggerService.LogInformation($"Found {filteredAnimals.Count} animals with name containing '{animalName}'.");
                return filteredAnimals;
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error fetching animals by name: {ex.Message}", ex);
                throw new ArgumentNullException("Animal cannot be null.");
            }
        }
        public async Task AddAnimalAsync(Animal animal)
        {
            if (animal == null)
            {
                
                _loggerService.LogWarning("Attempted to add a null animal.");
                throw new ArgumentNullException(nameof(animal), "Animal cannot be null.");
            }
            
            try
            {
                await _animalRepository.AddAsync(animal);
                _loggerService.LogInformation($"Animal {animal.Name} added successfully.");
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error logging animal addition: {ex.Message}", ex);
            }
        }


        public async Task UpdateAnimalAsync(Animal animal, int id)
        {
            if (animal == null)
            {
                _loggerService.LogWarning("Attempted to update a null animal.");
                throw new ArgumentNullException(nameof(animal));
            }

            try
            {
                await _animalRepository.UpdateAsync(animal, id);
                _loggerService.LogInformation($"Animal '{animal.Name}' updated successfully.");
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error updating animal with ID {id}: {ex.Message}", ex);
                throw;
            }
        }



        public async Task DeleteAnimalAsync(int id)
        {
            try { 
                var animal = await _animalRepository.GetAllAsync();
                if (animal == null || !animal.Any(a => a.Id == id))
                {
                    _loggerService.LogWarning($"Animal with ID {id} not found for deletion.");
                    throw new KeyNotFoundException($"Animal with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error checking animal existence: {ex.Message}", ex);
                throw;
            }
            await _animalRepository.DeleteAsync(id);
        }

        public Task<List<Animal>> GetAllAnimalsByCountryCodeAsync(string code)
        {
            try
            {
                var animals = _animalRepository.GetAllAsync().Result;
                if (animals == null || !animals.Any())
                {
                    _loggerService.LogWarning("No animals found in the database.");
                    return Task.FromResult(new List<Animal>());
                }
                
                var filteredAnimals = animals.Where(a => a.CountryCode.Equals(code, StringComparison.OrdinalIgnoreCase)).ToList();
                if (!filteredAnimals.Any())
                {
                    _loggerService.LogWarning($"No animals found with country code '{code}'.");
                    return Task.FromResult(new List<Animal>());
                }
                
                _loggerService.LogInformation($"Found {filteredAnimals.Count} animals with country code '{code}'.");
                return Task.FromResult(filteredAnimals);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error fetching animals by country code: {ex.Message}", ex);
                throw;
            }
        }

        public Task<List<Animal>> GetAllAnimalsByNameSortedAsync()
        {
            try
            {
                var animals = _animalRepository.GetAllAsync().Result;
                if (animals == null || !animals.Any())
                {
                    _loggerService.LogWarning("No animals found in the database.");
                    return Task.FromResult(new List<Animal>());
                }

                var filteredAnimals = animals.OrderBy(a => a.Name).ToList();

                if (!filteredAnimals.Any())
                {
                    _loggerService.LogWarning("No animals found after sorting by name.");
                    return Task.FromResult(new List<Animal>());
                }
                
                _loggerService.LogInformation($"Found {filteredAnimals.Count} animals sorted by name.");
                return Task.FromResult(filteredAnimals);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error fetching and sorting animals by name: {ex.Message}", ex);
                throw new ArgumentNullException("Animal cannot be null.");
            }
        }
    }
}
