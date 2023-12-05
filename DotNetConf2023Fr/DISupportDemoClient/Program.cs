// See https://aka.ms/new-console-template for more information

using System.ServiceModel;
using DISupportDemoClient;

var channelFactory = new ChannelFactory<IEchoService>(new BasicHttpBinding
{
    Security = new BasicHttpSecurity
    {
        Mode = BasicHttpSecurityMode.Transport
    }
}, new EndpointAddress("https://localhost:57275/EchoService.svc")); 

var channel = channelFactory.CreateChannel();

var response = channel.Echo("Hello World!");

Console.WriteLine(response);