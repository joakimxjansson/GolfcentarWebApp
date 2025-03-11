using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Data;

public class GolfContext : DbContext {
    public GolfContext(DbContextOptions<GolfContext> options) : base(options) {

    }

    public DbSet<Product> Product { get; set; }
     DbSet<Order> Order { get; set; }
     DbSet<Post> Post { get; set; }
     DbSet<CartItems> CartItems { get; set; }
    public DbSet<User> Users { get; set; }
    DbSet<Review> Review { get; set; }
     DbSet<SubPost> SubPost { get; set; }

    //Produkter - Anton
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, ProdName = "Golfklubba Driver", ProdDescription = "Fin driver av h�gsta kvalit�", ProdPrice = 2599, ProdImage = "driver.jpg" },
            new Product { ProductId = 2, ProdName = "Golfklubba J�rn", ProdDescription = "J�rnklubba i v�rldsklass", ProdPrice = 1999, ProdImage = "jarnklubba.jpg" },
            new Product { ProductId = 3, ProdName = "Golfklubba Putter", ProdDescription = "Perfekt balans", ProdPrice = 1699, ProdImage = "putter.jpg" },
            new Product { ProductId = 4, ProdName = "Golfbag", ProdDescription = "Vattent�lig", ProdPrice = 1999, ProdImage = "golfbag.jpg" },
            new Product { ProductId = 5, ProdName = "Golfhandske", ProdDescription = "Bra grepp", ProdPrice = 299, ProdImage = "handske.jpg" },
            new Product { ProductId = 6, ProdName = "Golfbollar (12-pack)", ProdDescription = "H�gkvalitativa bollar.", ProdPrice = 349, ProdImage = "golfballs.jpg" },
            new Product { ProductId = 7, ProdName = "Golfkeps", ProdDescription = "Perfekt n�r solen tittar fram", ProdPrice = 249, ProdImage = "keps.jpg" },
            new Product { ProductId = 8, ProdName = "peg (10-pack)", ProdDescription = "pegs i tr�", ProdPrice = 39, ProdImage = "peg.jpg" }

        );
        
    }
}