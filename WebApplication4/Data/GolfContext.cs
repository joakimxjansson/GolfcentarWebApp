using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WebApplication4.Data;

public class GolfContext : DbContext {
    private readonly PasswordHasher _passwordHasher = new();
    public GolfContext(DbContextOptions<GolfContext> options) : base(options) {

    }

    public DbSet<Product> Product { get; set; }
     public DbSet<Order> Order { get; set; }
     public DbSet<Post> Post { get; set; }
    public DbSet<CartItems> CartItems { get; set; }
    public DbSet<User> Users { get; set; }
   public DbSet<Review> Review { get; set; }
    public DbSet<SubPost> SubPost { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<Comment> Comments { get; set; } 

    //Produkter - Anton
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Product>().HasData(
            new Product {
                ProductId = 1, ProdName = "Golfklubba Driver", ProdDescription = "Fin driver av högsta kvalite",
                ProdPrice = 2599, ProdImage = "driver.jpg"
            },
            new Product {
                ProductId = 2, ProdName = "Golfklubba J�rn", ProdDescription = "Järnklubba i världsklass",
                ProdPrice = 1999, ProdImage = "jarnklubba.jpg"
            },
            new Product {
                ProductId = 3, ProdName = "Golfklubba Putter", ProdDescription = "Perfekt balans", ProdPrice = 1699,
                ProdImage = "putter.jpg"
            },
            new Product {
                ProductId = 4, ProdName = "Golfbag", ProdDescription = "Vattentålig", ProdPrice = 1999,
                ProdImage = "golfbag.jpg"
            },
            new Product {
                ProductId = 5, ProdName = "Golfhandske", ProdDescription = "Bra grepp", ProdPrice = 299,
                ProdImage = "handske.jpg"
            },
            new Product {
                ProductId = 6, ProdName = "Golfbollar (12-pack)", ProdDescription = "Högkvalitativa bollar.",
                ProdPrice = 349, ProdImage = "golfballs.jpg"
            },
            new Product {
                ProductId = 7, ProdName = "Golfkeps", ProdDescription = "Perfekt när solen tittar fram",
                ProdPrice = 249, ProdImage = "centarkeps.jpg"
            },
            new Product {
                ProductId = 8, ProdName = "peg (10-pack)", ProdDescription = "pegs i trä", ProdPrice = 39,
                ProdImage = "peg.jpg"
            }

        );
        modelBuilder.Entity<User>().HasData (
            new User
            {
                UserId = 1, Username ="Admin", FirstName ="Nevena",
                LastName = "Kicanovic",
                Password =_passwordHasher.Hash("Admin123"),
                Admin = 1, Email = "admin@test.com",
                Saldo = 10000,
                UserImage = "/images/DefaultImage.png"
            },
            new User
            {
                UserId = 2,
                Username = "TigerWoods",
                FirstName = "Tiger", LastName = "Woods",
                Password = _passwordHasher.Hash("Woods123"), 
                Admin = 0, 
                Email = "woods@test.com", 
                UserImage = "/images/DefaultImage.png",
                Saldo = 10000
            }
        );

        modelBuilder.Entity<Post>().HasData(

            new Post
            {
                PostId = 1,
                Title = "Golf är kul",
                Content = "Första gången jag och familjen testar Golfcentars banor. Mycket trevlig upplevelse," +
                "hela familjen hade roligt. Även Olle som inte är direkt intresserad av golf. I och för sig ville han bara" +
                " gräva med sin spade i bunkern.. Aja godkänd sand från hans håll.",
                UserId = 2,
                User = null,
                PublishDate = new DateTime(2025, 01, 25)
            },
            new Post {
                PostId = 2, Title = "Måsar överallt",
                Content = "Måsar är inte min grej. De är överallt och skriker. Jag vill bara spela golf i lugn och ro utan att" +
                " de vanaliserar och snor mina bollar. Jag har till och med fått en mås i ansiktet. Det var inte kul. " +
                " kanske går att träna på att slå dem med klubban? Jag ska prova nästa gång.",
                UserId = 1,
                User = null,
                PublishDate = new DateTime(2025, 01, 25)
            }
        );

        modelBuilder.Entity<Comment>().HasData(
            new Comment
            {
                CommentID = 1,
                Content = "Håller med, golf är kul!",
                PostId = 1,
                UserId = 1,
                CreatedAt = new DateTime(2025, 01, 25),
                ReviewId = null,
                User = null,
            },

            new Comment
            {
                CommentID = 2,
                Content = "Tråkigt att höra om måsarna! Men tror inte du ska öva med dem sen..",
                PostId = 2,
                UserId = 2,
                CreatedAt = new DateTime(2025, 01, 25),
                ReviewId = null,
                User = null,
            },
            new Comment
            {
                CommentID = 3,
                Content = "Tråkigt att höra om klubban,kom förbi shoppen så testar vi ut en ny åt dig!  ",
                PostId = null,
                UserId = 1,
                CreatedAt = new DateTime(2025, 01, 25),
                ReviewId = 2,
                User = null,
            },

            new Comment
            {
                CommentID = 4,
                Content = "Härligt!! Såg när du stod och körde med den här om dagen!",
                PostId = null,
                UserId = 2,
                CreatedAt = new DateTime(2025, 01, 28),
                ReviewId = 1,
                User = null,
            }

        );

        modelBuilder.Entity<Review>().HasData(
            new Review
            {
                ReviewId = 1,
                Content = "Bra golfklubba, rekommenderar den starkt!",
                Product = null,
                ProductId = 1,
                UserId = 1,
                Date = new DateTime(2025, 01, 25),
                User = null
            },
            new Review
            {
                ReviewId = 2,
                Content = "Inte så bra som jag trott. Känns inte så bra i handen.",
                Product = null,
                ProductId = 2,
                UserId = 2,
                Date = new DateTime(2025, 01, 25),
                User = null
            }

        );

        modelBuilder.Entity<Follow>()                                           
            .HasKey(k => new { k.FollowerId, k.FolloweeId });

        modelBuilder.Entity<Follow>()                                          
            .HasOne(u => u.Followee)
            .WithMany( u => u.Follower)
            .HasForeignKey(u => u.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Follow>()                                     
            .HasOne(u => u.Follower)
            .WithMany( u => u.Followee)
            .HasForeignKey(u => u.FolloweeId)
            .OnDelete(DeleteBehavior.Restrict);


    }
    
}
