using OrderQueryAPI.Models;

namespace OrderQueryAPI.Repositories;

public interface IOrderRepository
{
    void Create(Order data);
    Order GetById(Guid id);
    IEnumerable<Order> GetAll();
}