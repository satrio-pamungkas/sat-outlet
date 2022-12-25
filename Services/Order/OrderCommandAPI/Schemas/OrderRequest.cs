namespace OrderCommandAPI.Schemas;

public class OrderRequest
{
    public Guid Id { get; set; }
    public int? Quantity { get; set; }
}