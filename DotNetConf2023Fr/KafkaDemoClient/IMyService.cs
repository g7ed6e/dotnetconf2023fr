using System.ServiceModel;

namespace KafkaDemoClient;

[ServiceContract]
public interface IMyService
{
    [OperationContract(IsOneWay = true)]
    void Message(string message);
}