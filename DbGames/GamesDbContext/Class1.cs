using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GamesDbContext
{
    public class GameDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Studio> Studios {  get; set; }

        static DbContextOptions<GameDbContext> _options;

        static GameDbContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<GameDbContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }

        public GameDbContext()
           : base(_options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Studio)         
                .WithMany()                   
                .HasForeignKey(g => g.StudioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
