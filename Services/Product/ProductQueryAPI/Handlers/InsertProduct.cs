using ProductQueryAPI.Repositories;
using ProductQueryAPI.Models;

namespace ProductQueryAPI.Handlers;

public class InsertProduct : IInsertProduct
{
    private readonly IProductRepository _productRepository;

    public InsertProduct(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }
    public void CreateProduct(Product data)
    {
        this._productRepository.Create(data);
    }
}