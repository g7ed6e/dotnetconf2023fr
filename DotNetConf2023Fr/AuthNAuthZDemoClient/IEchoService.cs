using System.ServiceModel;

namespace AuthNAuthZDemoClient;

[ServiceContract]
public interface IEchoService
{
    [OperationContract]
    string Echo(string message);
}