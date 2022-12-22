using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet]
    public ActionResult<Models.Product> GetAll()
    {
        var payload = new Models.Product
        {
            Id = Guid.NewGuid(),
            Name = "Mouse Gaming",
            Price = 10000,
            Quantity = 20,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };

        return payload;
    }
}