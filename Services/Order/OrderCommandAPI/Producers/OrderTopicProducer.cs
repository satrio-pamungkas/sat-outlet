using Confluent.Kafka;
using OrderCommandAPI.Schemas;
using OrderCommandAPI.Utils;

namespace OrderCommandAPI.Producers;

public class OrderTopicProducer
{
    private readonly IProducer<Null, OrderRequest> _producer;
    
    public OrderTopicProducer(IConfiguration configuration)
    {
        var producerConfig = new ProducerConfig();
        configuration.GetSection("Kafka:ProducerSettings").Bind(producerConfig);
        this._producer = new ProducerBuilder<Null, OrderRequest>(producerConfig)
            .SetValueSerializer(new PayloadSerializer<OrderRequest>())
            .Build();
    }
    
    public void EmitMessage(string topic, OrderRequest payload)
    {
        var headers = new Headers();
        headers.Add("OrderCreated", new byte[] { 100 });
        this._producer.ProduceAsync(topic, new Message<Null, OrderRequest>
        {
            Headers = headers,
            Value = payload
        });
    }
}