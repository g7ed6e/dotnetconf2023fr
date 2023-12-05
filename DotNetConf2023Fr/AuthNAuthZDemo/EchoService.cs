using System.Security.Claims;
using CoreWCF;

namespace AuthNAuthZDemo;

[ServiceContract]
public interface IEchoService
{
    [OperationContract]
    string Echo(string message);
}

public partial class EchoService : IEchoService
{
    public string Echo(string message, [Injected] ILogger<EchoService> logger, [Injected] HttpContext httpContext)
    {
        foreach (Claim claim in httpContext.User.Claims)
        {
            logger.LogInformation($"Claim: {claim.Type} = {claim.Value}");
        }
        
        return message;
    }
}