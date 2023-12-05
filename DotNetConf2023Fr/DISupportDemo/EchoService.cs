using CoreWCF;

namespace DISupportDemo;

[ServiceContract]
public interface IEchoService
{
    [OperationContract]
    string Echo(string message);
}

public class EchoService : IEchoService
{
    public string Echo(string message)
    {
        return message;
    }
}
