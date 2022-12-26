using Microsoft.AspNetCore.Mvc;
using OrderQueryAPI.Repositories;
using OrderQueryAPI.Models;

namespace OrderQueryAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        this._orderRepository = orderRepository;
    }

    [HttpGet]
    public ActionResult GetAllData()
    {
        var data = this._orderRepository.GetAll();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetSpecific(string id)
    {
        var uuid = new Guid(id);
        return this._orderRepository.GetById(uuid);
    }
}