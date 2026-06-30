using Confluent.Kafka;
using KafkaTest;
using Microsoft.Extensions.Options;

public class KafkaConsumerService : BackgroundService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<KafkaConsumerService> _logger;

    public KafkaConsumerService(IOptions<KafkaOptions> options, ILogger<KafkaConsumerService> logger)
    {
        _logger = logger;

        var config = new ConsumerConfig
        {
            BootstrapServers = options.Value.BootstrapServers,
            GroupId = options.Value.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();

        _consumer.Subscribe("orders");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);

                _logger.LogInformation("Received message: {Message}", result.Message.Value);

                await Task.CompletedTask;
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kafka consumer failed");
            }
        }
    }
}