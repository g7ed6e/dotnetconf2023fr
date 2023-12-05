
using AuthNAuthZDemo;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceModelServices();
builder.Services.AddTransient<EchoService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    JwtBearerDefaults.AuthenticationScheme,
    options =>
    {
        options.Authority = "https://demo.duendesoftware.com";
        options.Audience = "api";
    });
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .RequireAssertion(context =>
        {
            var scopes = context.User.FindFirst("scope")?.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries) ??
                Array.Empty<string>();
            return scopes.Contains("api", StringComparer.Ordinal);
        })
        .Build();
});

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<EchoService>();

    var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
    binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.InheritedFromHost;
    serviceBuilder.AddServiceEndpoint<EchoService, IEchoService>(binding, "/EchoService.svc");
});

app.Start();

app.WaitForShutdown();