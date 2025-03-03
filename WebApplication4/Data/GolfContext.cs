using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Data;

public class GolfContext : DbContext {
    public GolfContext(DbContextOptions<GolfContext> options) : base(options) {

    }

     DbSet<Product> Product { get; set; }
     DbSet<Order> Order { get; set; }
     DbSet<Post> Post { get; set; }
     DbSet<CartItems> CartItems { get; set; }
    public DbSet<User> User { get; set; }
    DbSet<Review> Review { get; set; }
     DbSet<SubPost> SubPost { get; set; }
}