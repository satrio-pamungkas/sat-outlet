using OrderQueryAPI.Models;

namespace OrderQueryAPI.Handlers;

public interface IOrderHandler
{
    void CreateProduct(Order data);
}