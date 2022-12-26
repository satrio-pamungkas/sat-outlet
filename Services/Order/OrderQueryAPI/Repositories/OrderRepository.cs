using Microsoft.EntityFrameworkCore;
using OrderQueryAPI.Data;
using OrderQueryAPI.Models;

namespace OrderQueryAPI.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;

    OrderRepository(DataContext context)
    {
        this._context = context;
    }

    public void Create(Order data)
    {
        this._context.Orders.Add(data);
        this._context.SaveChanges();
    }

    public Order GetById(Guid id)
    {
        return this._context.Orders
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id) ?? throw new InvalidOperationException();
    }

    public IEnumerable<Order> GetAll()
    {
        return this._context.Orders.AsNoTracking().ToList();
    }
}