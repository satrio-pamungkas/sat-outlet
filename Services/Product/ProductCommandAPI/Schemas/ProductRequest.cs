namespace ProductCommandAPI.Schemas;

public class ProductRequest
{
    public String? Name { get; set; }
    public int? Price { get; set; }
    public int? Quantity { get; set; }
}