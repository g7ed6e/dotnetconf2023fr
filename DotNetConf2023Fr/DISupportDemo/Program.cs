using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Configuration;
using CoreWCF.Description;
using DISupportDemo;

var builder = WebApplication.CreateBuilder();
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();

builder.Services.AddTransient<EchoService>();
builder.Services.AddSingleton<IServiceBehavior, MyServiceBehavior>();

var app = builder.Build();
app.UseServiceModel(serviceBuilder =>
{
   serviceBuilder.AddService<EchoService>();
   serviceBuilder.AddServiceEndpoint<EchoService, IEchoService>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), 
      "/EchoService.svc");
   serviceBuilder.ConfigureServiceHostBase<EchoService>(serviceHostBase =>
   {
      serviceHostBase.Description.Behaviors.Find<MyServiceBehavior>().LoggingPrefix = "Demo";
   });
});

app.Start();

app.WaitForShutdown();