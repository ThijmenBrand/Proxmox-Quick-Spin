using System.Data;
using Corsinvest.ProxmoxVE.Api;
using TCP.ProxmoxInteractor.Factories;
using TCP.ProxmoxInteractor.Repositories;
using Microsoft.Extensions.Configuration;

namespace TCP.ProxmoxInteractor;

public class NodesRepository : BaseProxmoxRepository, INodesRepository
{
    private readonly IConfiguration _configuration;
    
    public NodesRepository(IProxmoxClientFactory proxmoxClientFactory, IConfiguration configuration) : base(proxmoxClientFactory)
    {
        _configuration = configuration;
    }
    
    public async Task<dynamic> ListNodes()
    {
        var client = _proxmoxClientFactory.Create();
        var nodes = await client.Get("/nodes");

        return nodes.Response;
    }
}