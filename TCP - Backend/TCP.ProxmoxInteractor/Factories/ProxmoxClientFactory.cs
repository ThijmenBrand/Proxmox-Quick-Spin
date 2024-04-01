using Corsinvest.ProxmoxVE.Api;
using Microsoft.Extensions.Configuration;

namespace TCP.ProxmoxInteractor.Factories;

public class ProxmoxClientFactory : IProxmoxClientFactory
{

    private readonly string _ipAddress;
    private readonly string? _apiKey;  

    public ProxmoxClientFactory(IConfiguration configuration)
    {
        _ipAddress  = "192.168.128.200";
        _apiKey = configuration["proxmox_api_key"];
    }
    public PveClient Create(int port = 8006)
    {
        PveClient client = new(_ipAddress, port)
        {
            ApiToken = _apiKey
        };

        return client;
    }

    public PveClient Create(string ip, string username, string password, int port = 8006)
    {
        PveClient client = new(ip, port);
        client.Login(username, password);

        return client;
    }
}