using Confluent.Kafka;
using OrderCommandAPI.Schemas;
using OrderCommandAPI.Utils;

namespace OrderCommandAPI.Producers;

public class ProductTopicProducer
{
    private readonly IProducer<Null, ProductQuantityUpdateRequest> _producer;
    
    public ProductTopicProducer(IConfiguration configuration)
    {
        var producerConfig = new ProducerConfig();
        configuration.GetSection("Kafka:ProducerSettings").Bind(producerConfig);
        this._producer = new ProducerBuilder<Null, ProductQuantityUpdateRequest>(producerConfig)
            .SetValueSerializer(new PayloadSerializer<ProductQuantityUpdateRequest>())
            .Build();
    }
    
    public void EmitMessage(string topic, ProductQuantityUpdateRequest payload)
    {
        var headers = new Headers();
        headers.Add("ProductQuantityUpdated", new byte[] { 100 });
        this._producer.ProduceAsync(topic, new Message<Null, ProductQuantityUpdateRequest>
        {
            Headers = headers,
            Value = payload
        });
    }
}