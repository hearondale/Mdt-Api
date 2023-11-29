using MdtApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MdtApi.Controllers.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Impound> Impounds => Set<Impound>();
        public DbSet<Citizen> Citizen => Set<Citizen>();
        public DbSet<Officer> Officer => Set<Officer>();
    }
}
