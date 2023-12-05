using CoreWCF;

namespace KafkaDemo;

[ServiceContract]
public interface IMyService
{
    [OperationContract(IsOneWay = true)]
    void Message(string message);
}

public partial class MyService : IMyService
{
    public void Message(string message, [Injected] ILogger<MyService> logger)
    {
        logger.LogInformation($"Message: {message}");
    }
}
