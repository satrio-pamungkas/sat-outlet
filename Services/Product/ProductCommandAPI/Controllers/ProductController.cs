using Microsoft.AspNetCore.Mvc;
using ProductCommandAPI.Models;
using ProductCommandAPI.Producers;
using ProductCommandAPI.Schemas;

namespace ProductCommandAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductTopicProducer _productTopicProducer;
    private readonly string _topic;
    
    public ProductController(IConfiguration configuration)
    {
        this._productTopicProducer = new ProductTopicProducer(configuration);
        this._topic = configuration.GetValue<string>("Kafka:Topic:Product");
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct(ProductRequest request)
    {
        var payload = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };
        this._productTopicProducer.EmitMessage(this._topic, payload);
        
        return payload;
    }
}