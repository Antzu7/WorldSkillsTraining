using System.Data.Entity;
using WorldSkills.Models;

namespace WorldSkills.Data
{
    internal class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() : base("DBConnection") {}

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
