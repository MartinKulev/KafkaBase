using Confluent.Kafka;
using KafkaTest;
using KafkaTestCommon;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KafkaOptions>(
builder.Configuration.GetSection("Kafka"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddHostedService<KafkaConsumerService>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.Run();
