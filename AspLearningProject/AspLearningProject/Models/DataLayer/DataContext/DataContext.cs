using Microsoft.EntityFrameworkCore;

namespace AspLearningProject.Models.DataLayer.DataContext
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
       
    }
}
