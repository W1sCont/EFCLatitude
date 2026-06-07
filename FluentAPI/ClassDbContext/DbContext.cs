using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClassDbContext
{
    public class HrmDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobTitle> Role { get; set; }

        static DbContextOptions<HrmDbContext> _options;
        static HrmDbContext()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<HrmDbContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }
        public HrmDbContext()
            : base(_options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("HumanResourceManagement");
            modelBuilder.Entity<Employee>().HasOne(i => i.JobTitle_Id).WithMany(i => i.Employees).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Employee>().Property(i => i.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(i => i.Surname).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().ToTable(t => t.HasCheckConstraint("CK_Employee_Age", "[Age] >= 18 AND [Age] <= 65"));

            modelBuilder.Entity<JobTitle>().ToTable("Role");
            modelBuilder.Entity<JobTitle>().Property(i => i.Title).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<JobTitle>().HasIndex(i => i.Title).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
