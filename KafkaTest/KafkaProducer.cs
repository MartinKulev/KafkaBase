using Confluent.Kafka;
using KafkaTest;
using Microsoft.Extensions.Options;
using System.Text.Json;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> _producer;

    public KafkaProducer(IOptions<KafkaOptions> options)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = options.Value.BootstrapServers
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task PublishAsync<T>(string topic, string key, T message, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(message);

        var kafkaMessage = new Message<string, string>
        {
            Key = key,
            Value = json
        };

        await _producer.ProduceAsync(topic, kafkaMessage, cancellationToken);
    }
}