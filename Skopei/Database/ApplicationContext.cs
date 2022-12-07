using Microsoft.EntityFrameworkCore;
using Skopei.Models;

namespace Skopei.Database
{
    /*
     * ApplicationContext class
     * 
     * This class is registered as the DbContext class.
     * Contains the Users and Products DbSet tables which are used in migrations to create the tables.
     */
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}