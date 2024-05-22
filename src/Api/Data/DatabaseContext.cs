using Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
    }
}