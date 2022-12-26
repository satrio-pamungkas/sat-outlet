namespace OrderCommandAPI.Schemas;

public class ProductQuantityUpdateRequest
{
    public Guid ProductId { get; set; }
    public int? Quantity { get; set; }
}