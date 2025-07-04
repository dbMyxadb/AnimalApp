using finallexamp.Core.Interfaces;
using finallexamp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace finallexamp.DAL.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalDbContext _context;

        public AnimalRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async Task<List<Animal>> GetAllAsync()
        {
            return await _context.Animals.ToListAsync();
        }


        public async Task AddAsync(Animal animal)
        {
            _context.Add(animal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync( Animal updatedAnimal,int id)
        {
            var existingAnimal = await _context.Animals.FindAsync(id);
            if (existingAnimal == null)
            {
                throw new Exception($"Animal with ID {id} not found.");
            }

            existingAnimal.Name = updatedAnimal.Name;
            existingAnimal.ScientificName = updatedAnimal.ScientificName;
            existingAnimal.CountryCode = updatedAnimal.CountryCode;
            existingAnimal.ConservationStatus = updatedAnimal.ConservationStatus;
            existingAnimal.GroupName = updatedAnimal.GroupName;

            await _context.SaveChangesAsync();
        }



        public async Task DeleteAsync(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Remove(animal);
                await _context.SaveChangesAsync();
            }
        }
    }
}