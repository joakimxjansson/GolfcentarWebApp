namespace WebApplication4.Data;

public class CartItems {
    public int CartItemsId { get; set; }
    public Product Prod { get; set; }
    public int Quantity { get; set; }
    public int TotalPrice { get; set; }
}