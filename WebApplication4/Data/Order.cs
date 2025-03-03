namespace WebApplication4.Data;

public class Order {
    public int OrderId { get; set; }

    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public User UserId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public Product ProductId { get; set; }
}