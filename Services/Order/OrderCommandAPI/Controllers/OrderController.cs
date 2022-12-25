using Microsoft.AspNetCore.Mvc;
using OrderCommandAPI.Producers;
using OrderCommandAPI.Schemas;

namespace OrderCommandAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderCreatedProducer _orderCreatedProducer;
    private readonly string _topic;

    public OrderController(IConfiguration configuration)
    {
        this._orderCreatedProducer = new OrderCreatedProducer(configuration);
        this._topic = configuration.GetValue<string>("Kafka:Topic");
    }
    
    [HttpPost]
    public ActionResult<OrderRequest> CreateOrder(OrderRequest request)
    {
        this._orderCreatedProducer.EmitMessage(this._topic, request);
        return request;
    }
}