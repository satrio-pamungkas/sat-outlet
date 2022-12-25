using ProductQueryAPI.Models;

namespace ProductQueryAPI.Repositories;

public interface IProductRepository
{
    void Create(Product data);
    Product GetById(Guid id);
    IEnumerable<Product> GetAll();
}