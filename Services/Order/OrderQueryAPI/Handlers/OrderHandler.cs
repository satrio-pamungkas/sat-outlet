using OrderQueryAPI.Repositories;
using OrderQueryAPI.Models;

namespace OrderQueryAPI.Handlers;

public class OrderHandler : IOrderHandler
{
    private readonly IOrderRepository _orderRepository;

    public OrderHandler(IOrderRepository orderRepository)
    {
        this._orderRepository = orderRepository;
    }
    public void CreateProduct(Order data)
    {
        this._orderRepository.Create(data);
    }
}