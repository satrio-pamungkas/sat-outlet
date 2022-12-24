using Confluent.Kafka;
using ProductCommandAPI.Models;
using ProductCommandAPI.Utils;

namespace ProductCommandAPI.Producers;

public class ProductCreatedProducer
{
    private readonly IProducer<Null, Product> _producer;
    
    public ProductCreatedProducer(IConfiguration configuration)
    {
        var producerConfig = new ProducerConfig();
        configuration.GetSection("Kafka:ProducerSettings").Bind(producerConfig);
        this._producer = new ProducerBuilder<Null, Product>(producerConfig)
            .SetValueSerializer(new PayloadSerializer<Product>())
            .Build();
    }
    
    public void EmitMessage(string topic, Product payload)
    {
        this._producer.ProduceAsync(topic, new Message<Null, Product>
        {
            Value = payload
        });
    }
}