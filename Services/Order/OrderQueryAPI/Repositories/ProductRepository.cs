using Microsoft.EntityFrameworkCore;
using OrderQueryAPI.Data;
using OrderQueryAPI.Models;

namespace OrderQueryAPI.Repositories;

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

    public Product GetById(Guid id)
    {
        return this._context.Products
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id) ?? throw new InvalidOperationException();
    }

    public IEnumerable<Product> GetAll()
    {
        return this._context.Products.AsNoTracking().ToList();
    }
}