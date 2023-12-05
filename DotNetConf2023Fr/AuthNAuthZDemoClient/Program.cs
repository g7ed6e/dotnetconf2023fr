// See https://aka.ms/new-console-template for more information

using System.ServiceModel;
using System.ServiceModel.Channels;
using AuthNAuthZDemoClient;
using IdentityModel.Client;

using HttpClient httpClient = new HttpClient();
var discovery = await httpClient.GetDiscoveryDocumentAsync("https://demo.duendesoftware.com/.well-known/openid-configuration");
var token = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
{
    Address = discovery.TokenEndpoint,
    ClientId = "m2m",
    ClientSecret = "secret",
    Scope = "api"
});

var channelFactory = new ChannelFactory<IEchoService>(new BasicHttpBinding
{
    Security = new BasicHttpSecurity
    {
        Mode = BasicHttpSecurityMode.Transport
    }
}, new EndpointAddress("https://localhost:57179/EchoService.svc")); 

var channel = channelFactory.CreateChannel();

var requestMessageProperty = new HttpRequestMessageProperty();
requestMessageProperty.Headers["Authorization"] = $"Bearer {token.AccessToken}";

using (var scope = new OperationContextScope((IClientChannel)channel))
{
    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessageProperty;
    var response = channel.Echo("Hello World!");    

    Console.WriteLine(response);
}
