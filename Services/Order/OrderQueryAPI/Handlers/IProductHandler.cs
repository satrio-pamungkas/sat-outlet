using OrderQueryAPI.Models;

namespace OrderQueryAPI.Handlers;

public interface IProductHandler
{
    void CreateProduct(Product data);
}