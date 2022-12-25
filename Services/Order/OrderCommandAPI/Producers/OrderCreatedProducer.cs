using Confluent.Kafka;
using OrderCommandAPI.Schemas;
using OrderCommandAPI.Utils;

namespace OrderCommandAPI.Producers;

public class OrderCreatedProducer
{
    private readonly IProducer<Null, OrderRequest> _producer;
    
    public OrderCreatedProducer(IConfiguration configuration)
    {
        var producerConfig = new ProducerConfig();
        configuration.GetSection("Kafka:ProducerSettings").Bind(producerConfig);
        this._producer = new ProducerBuilder<Null, OrderRequest>(producerConfig)
            .SetValueSerializer(new PayloadSerializer<OrderRequest>())
            .Build();
    }
    
    public void EmitMessage(string topic, OrderRequest payload)
    {
        this._producer.ProduceAsync(topic, new Message<Null, OrderRequest>
        {
            Value = payload
        });
    }
}