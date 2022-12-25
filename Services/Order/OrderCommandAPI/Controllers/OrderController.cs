using Microsoft.AspNetCore.Mvc;
using OrderCommandAPI.Producers;
using OrderCommandAPI.Schemas;

namespace OrderCommandAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderTopicProducer _orderTopicProducer;
    private readonly string _topic;

    public OrderController(IConfiguration configuration)
    {
        this._orderTopicProducer = new OrderTopicProducer(configuration);
        this._topic = configuration.GetValue<string>("Kafka:Topic:Order");
    }
    
    [HttpPost]
    public ActionResult<OrderRequest> CreateOrder(OrderRequest request)
    {
        this._orderTopicProducer.EmitMessage(this._topic, request);
        return request;
    }
}