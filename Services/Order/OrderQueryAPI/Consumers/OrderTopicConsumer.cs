using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using OrderQueryAPI.Utils;
using OrderQueryAPI.Schemas;

namespace OrderQueryAPI.Consumers;

public class OrderTopicConsumer : BackgroundService
{
    private readonly string _topic;
    private readonly IConsumer<Ignore, OrderRequest> _consumer;

    public OrderTopicConsumer(IConfiguration configuration)
    {
        var consumerConfig = new ConsumerConfig();
        configuration.GetSection("Kafka:ConsumerSettings").Bind(consumerConfig);
        this._topic = configuration.GetValue<string>("Kafka:Topic:Order");
        this._consumer = new ConsumerBuilder<Ignore, OrderRequest>(consumerConfig)
            .SetValueDeserializer(new ProductDeserializer<OrderRequest>().AsSyncOverAsync())
            .Build();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => StartConsumerLoop(stoppingToken), stoppingToken);
    }
    
    private void StartConsumerLoop(CancellationToken cancellationToken)
    {
        this._consumer.Subscribe(this._topic);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                // using var scope = _serviceScopeFactory.CreateScope();
                // var insertProduct = scope.ServiceProvider.GetRequiredService<IInsertProduct>();
                var payload = this._consumer.Consume(cancellationToken);
                var header = payload.Message.Headers[0].Key;
                var data = payload.Message.Value.Quantity;

                // Handle message...
                Console.WriteLine(header);
                Console.WriteLine(data);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (ConsumeException e)
            {
                // Consumer errors should generally be ignored (or logged) unless fatal.
                Console.WriteLine($"Consume error: {e.Error.Reason}");

                if (e.Error.IsFatal)
                {
                    // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e}");
                break;
            }
        }
    }

    public override void Dispose()
    {
        this._consumer.Close();
        this._consumer.Dispose();
        
        base.Dispose();
    }
}