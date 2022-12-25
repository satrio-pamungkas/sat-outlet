using ProductQueryAPI.Data;
using ProductQueryAPI.Models;

namespace ProductQueryAPI.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        this._context = context;
    }

    public void Create(Product data)
    {
        this._context.Products.Add(data);
        this._context.SaveChanges();
    }
}