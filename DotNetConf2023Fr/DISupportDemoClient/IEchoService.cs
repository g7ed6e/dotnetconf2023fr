using System.ServiceModel;

namespace DISupportDemoClient;

[ServiceContract]
public interface IEchoService
{
    [OperationContract]
    string Echo(string message);
} 
