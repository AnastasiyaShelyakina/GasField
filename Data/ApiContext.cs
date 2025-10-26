using GasField.Models;
using Microsoft.EntityFrameworkCore;

namespace GasField.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Well> Wells { get; set; }
        public DbSet<Ukpg> Ukpgs { get; set; }
    }
}
