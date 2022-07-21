using LoginService.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginService.Entity
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext() 
        { 

        }
        
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AppDb");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
