using ProductQueryAPI.Models;

namespace ProductQueryAPI.Handlers;

public interface IInsertProduct
{
    void CreateProduct(Product data);
}