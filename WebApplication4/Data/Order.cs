namespace WebApplication4.Data;

public class Order {
    public int OrderId { get; set; }

    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public User User { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public Product Product { get; set; }
}