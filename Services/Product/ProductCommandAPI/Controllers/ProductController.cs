using Microsoft.AspNetCore.Mvc;
using ProductCommandAPI.Models;
using ProductCommandAPI.Producers;
using ProductCommandAPI.Schemas;

namespace ProductCommandAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductCreatedProducer _productCreatedProducer;
    private readonly string _topic;
    
    public ProductController(IConfiguration configuration)
    {
        this._productCreatedProducer = new ProductCreatedProducer(configuration);
        this._topic = configuration.GetValue<string>("Kafka:Topic");
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
        this._productCreatedProducer.EmitMessage(this._topic, payload);
        
        return payload;
    }
}