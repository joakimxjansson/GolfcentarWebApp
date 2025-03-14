using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Data;

public class GolfContext : DbContext {
    public GolfContext(DbContextOptions<GolfContext> options) : base(options) {

    }

    public DbSet<Product> Product { get; set; }
     public DbSet<Order> Order { get; set; }
     public DbSet<Post> Post { get; set; }
    public DbSet<CartItems> CartItems { get; set; }
    public DbSet<User> Users { get; set; }
   public DbSet<Review> Review { get; set; }
    public DbSet<SubPost> SubPost { get; set; }

    //Produkter - Anton
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, ProdName = "Golfklubba Driver", ProdDescription = "Fin driver av högsta kvalite", ProdPrice = 2599, ProdImage = "driver.jpg" },
            new Product { ProductId = 2, ProdName = "Golfklubba J�rn", ProdDescription = "Järnklubba i världsklass", ProdPrice = 1999, ProdImage = "jarnklubba.jpg" },
            new Product { ProductId = 3, ProdName = "Golfklubba Putter", ProdDescription = "Perfekt balans", ProdPrice = 1699, ProdImage = "putter.jpg" },
            new Product { ProductId = 4, ProdName = "Golfbag", ProdDescription = "Vattentålig", ProdPrice = 1999, ProdImage = "golfbag.jpg" },
            new Product { ProductId = 5, ProdName = "Golfhandske", ProdDescription = "Bra grepp", ProdPrice = 299, ProdImage = "handske.jpg" },
            new Product { ProductId = 6, ProdName = "Golfbollar (12-pack)", ProdDescription = "Högkvalitativa bollar.", ProdPrice = 349, ProdImage = "golfballs.jpg" },
            new Product { ProductId = 7, ProdName = "Golfkeps", ProdDescription = "Perfekt när solen tittar fram", ProdPrice = 249, ProdImage = "keps.jpg" },
            new Product { ProductId = 8, ProdName = "peg (10-pack)", ProdDescription = "pegs i trä", ProdPrice = 39, ProdImage = "peg.jpg" }

        );

       

    }
    
        
    

}
