using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using ProductQueryAPI.Handlers;
using ProductQueryAPI.Models;
using ProductQueryAPI.Utils;

namespace ProductQueryAPI.Consumers;

public class ProductCreatedConsumer : BackgroundService
{
    private readonly string _topic;
    private readonly IConsumer<Ignore, Product> _consumer;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ProductCreatedConsumer(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
    {
        var consumerConfig = new ConsumerConfig();
        configuration.GetSection("Kafka:ConsumerSettings").Bind(consumerConfig);
        this._topic = configuration.GetValue<string>("Kafka:Topic");
        this._consumer = new ConsumerBuilder<Ignore, Product>(consumerConfig)
            .SetValueDeserializer(new ProductDeserializer<Product>().AsSyncOverAsync())
            .Build();
        this._serviceScopeFactory = serviceScopeFactory;
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
                using var scope = _serviceScopeFactory.CreateScope();
                var insertProduct = scope.ServiceProvider.GetRequiredService<IInsertProduct>();
                var payload = this._consumer.Consume(cancellationToken);
                var data = payload.Message.Value;

                // Handle message...
                insertProduct?.CreateProduct(data);
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