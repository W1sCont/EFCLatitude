using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace WarehouseDbContext
{
    public class ClassDbContext : DbContext
    {
        private static DbContextOptions<ClassDbContext> _options;
        static ClassDbContext()
        {
            string basePath = AppContext.BaseDirectory;
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(basePath);
            builder.AddJsonFile("appsettings.json", optional: true);

            var config = builder.Build();
            string? connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ClassDbContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }
        public ClassDbContext() : this(_options) { }
        public ClassDbContext(DbContextOptions<ClassDbContext> _options)
            : base(_options) { }
        public virtual DbSet<ClassGoods> Goods { get; set; }
        public virtual DbSet<ClassSupplier> Suppliers { get; set; }
        public virtual DbSet<ClassTypeOfGood> Types { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClassGoods>()
                .HasOne(g => g.TypeOfGood)
                .WithMany(t => t.Goods)
                .HasForeignKey(g => g.TypeOfGoodId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClassGoods>()
                .HasOne(g => g.Supplier)
                .WithMany(s => s.Goods)
                .HasForeignKey(g => g.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
