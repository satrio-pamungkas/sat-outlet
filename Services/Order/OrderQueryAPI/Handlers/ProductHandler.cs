using OrderQueryAPI.Repositories;
using OrderQueryAPI.Models;

namespace OrderQueryAPI.Handlers;

public class ProductHandler : IProductHandler
{
    private readonly IProductRepository _productRepository;

    public ProductHandler(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }
    public void CreateProduct(Product data)
    {
        this._productRepository.Create(data);
    }
}