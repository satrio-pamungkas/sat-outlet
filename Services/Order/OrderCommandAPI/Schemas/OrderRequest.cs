namespace OrderCommandAPI.Schemas;

public class OrderRequest
{
    public Guid ProductId { get; set; }
    public int? Quantity { get; set; }
}