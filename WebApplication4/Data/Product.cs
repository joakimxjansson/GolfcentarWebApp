namespace WebApplication4.Data;

public class Product {
    public int ProductId { get; set; }
    public string ProdName { get; set; }
    public string ProdDescription { get; set; }
    public decimal ProdPrice { get; set; }
    public string ProdImage { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}