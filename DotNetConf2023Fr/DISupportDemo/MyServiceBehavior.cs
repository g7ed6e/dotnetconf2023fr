using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using System.Collections.ObjectModel;

namespace DISupportDemo;

public class MyServiceBehavior : IServiceBehavior, ICloneable
{
    private ILogger<MyServiceBehavior> _logger;

    public MyServiceBehavior(ILogger<MyServiceBehavior> logger)
    {
       _logger = logger;
    }

    public string LoggingPrefix { get; set; } = nameof(MyServiceBehavior);

    public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
    {
        _logger.LogInformation("{LoggingPrefix}: AddBindingParameters called", LoggingPrefix);
    }

    public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
    {
        _logger.LogInformation("{LoggingPrefix}: ApplyDispatchBehavior called", LoggingPrefix);
    }

    public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
    {
        _logger.LogInformation("{LoggingPrefix}: Validate called", LoggingPrefix);
    }

    public object Clone()
    {
        return new MyServiceBehavior(_logger)
        {
            LoggingPrefix = LoggingPrefix
        };
    }
}
