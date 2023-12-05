// See https://aka.ms/new-console-template for more information

using System.ServiceModel;
using CoreWCF.ServiceModel.Channels;
using KafkaDemoClient;

var channelFactory = new ChannelFactory<IMyService>(new KafkaBinding(), new EndpointAddress("net.kafka://localhost:9092/my-topic"));

var channel = channelFactory.CreateChannel();

while (Console.ReadKey().Key != ConsoleKey.Escape)
{
    channel.Message("Hello World!");    
}
