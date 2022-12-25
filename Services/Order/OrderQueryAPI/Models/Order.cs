namespace OrderQueryAPI.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public String? Name { get; set; }
    public String? Status { get; set; }
    public int? Price { get; set; }
    public int? Quantity { get; set; }
    public int? TotalPrice { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}