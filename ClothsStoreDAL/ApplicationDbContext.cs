using Microsoft.EntityFrameworkCore;
using ClothsStore.BL.Models;

namespace ClothsStore.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<CartItem> Users { get; set; }
        public virtual DbSet<Product> Recipes { get; set; }
    }
}
