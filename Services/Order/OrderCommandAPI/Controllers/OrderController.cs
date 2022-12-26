using Microsoft.AspNetCore.Mvc;
using OrderCommandAPI.Producers;
using OrderCommandAPI.Schemas;

namespace OrderCommandAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderTopicProducer _orderTopicProducer;
    private readonly ProductTopicProducer _productTopicProducer;
    private readonly string _orderTopic;
    private readonly string _productTopic;

    public OrderController(IConfiguration configuration)
    {
        this._orderTopicProducer = new OrderTopicProducer(configuration);
        this._productTopicProducer = new ProductTopicProducer(configuration);
        this._orderTopic = configuration.GetValue<string>("Kafka:Topic:Order");
        this._productTopic = configuration.GetValue<string>("Kafka:Topic:Product");
    }
    
    [HttpPost]
    public ActionResult<OrderRequest> CreateOrder(OrderRequest request)
    {
        this._orderTopicProducer.EmitMessage(this._orderTopic, request);
        this._orderTopicProducer.EmitMessage(this._productTopic, request);
        return request;
    }
}