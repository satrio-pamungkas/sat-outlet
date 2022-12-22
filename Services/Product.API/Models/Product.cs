namespace Product.API.Models;

public class Product
{
    public Guid Id { get; set; }
    public String? Name { get; set; }
    public int? Price { get; set; }
    public int? Quantity { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}