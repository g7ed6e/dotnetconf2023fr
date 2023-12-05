
using Confluent.Kafka;
using CoreWCF.Channels;
using CoreWCF.Configuration;
using CoreWCF.Queue.Common.Configuration;
using KafkaDemo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceModelServices();
builder.Services.AddQueueTransport();
builder.Services.AddTransient<MyService>();

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<MyService>();
    var binding = new KafkaBinding()
    {
        GroupId = "my-group",
        AutoOffsetReset = AutoOffsetReset.Earliest
    };
    serviceBuilder.AddServiceEndpoint<MyService, IMyService>(binding, "net.kafka://localhost:9092/my-topic");
});

app.Start();

app.WaitForShutdown();