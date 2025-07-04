using finallexamp.Api.Models;

namespace finallexamp.Api.Interfaces
{
    public interface IAnimalServiceApi
    {
        Task<List<AnimalApi>> GetAllAnimalsByScientificNameAsync();
        Task<WrapperAnimal> GetAllAnimalsByNameAsync(string name);
        Task<WrapperAnimal> GetAllAnimalsByNameSortedAsync();
        Task<WrapperAnimal> GetAllAnimalsByCountryCodeAsync(string code);
    }
}
