using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace OrderQueryAPI.Utils;

public class ProductDeserializer<T> : IAsyncDeserializer<T> where T : class
{
    public Task<T> DeserializeAsync(ReadOnlyMemory<byte> data, bool isNull, SerializationContext context)
    {
        string json = Encoding.ASCII.GetString(data.Span);
        return Task.FromResult(JsonSerializer.Deserialize<T>(json))!;
    }
}