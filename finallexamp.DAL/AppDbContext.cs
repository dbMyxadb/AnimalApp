using finallexamp.Core.Models;
using finallexamp.DAL.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace finallexamp.DAL
{
    public class AnimalDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(config);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new AnimalConfiguration());
        }
    }
}
