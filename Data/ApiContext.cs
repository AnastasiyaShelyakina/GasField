using GasField.Models;
using Microsoft.EntityFrameworkCore;

namespace GasField.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        //public DbSet<User> Users { get; set; }
        public DbSet<Well> Wells { get; set; }
        public DbSet<Ukpg> Ukpgs { get; set; }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Один УКПГ — много скважин
            modelBuilder.Entity<Well>()
                .HasOne(w => w.Ukpg)
                .WithMany(u => u.Wells)
                .HasForeignKey(w => w.UkpgId)
                .OnDelete(DeleteBehavior.Cascade);

            // Уникальный логин пользователя
            modelBuilder.Entity<Person>()
                .HasIndex(u => u.Login)
                .IsUnique();

            // Добавляем тестовых пользователей
            SeedUsers(modelBuilder);
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    Id = 1,
                    Login = "Operator",
                    PasswordHash = "$2a$11$3ufDM9/ALD4/aoVANHk7FuFfFlJPcw8LfzQRlX7e8KQVIR3XfWoYi",
                    Role = "Admin",
                    FullName = "Администратор системы",
                    CreatedAt = new DateTime(2025, 10, 1, 0, 0, 0, DateTimeKind.Utc) 
                },
                new Person
                {
                    Id = 2,
                    Login = "Engineer",
                    PasswordHash = "$2a$11$3ufDM9/ALD4/aoVANHk7FuFfFlJPcw8LfzQRlX7e8KQVIR3XfWoYi",
                    Role = "Engineer",
                    FullName = "Инженер по добыче",
                    CreatedAt = new DateTime(2025, 10, 1, 0, 0, 0, DateTimeKind.Utc) 
                }
            );
        }

    }
}

