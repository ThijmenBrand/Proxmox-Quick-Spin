using Corsinvest.ProxmoxVE.Api;
using Corsinvest.ProxmoxVE.Api.Extension;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using Microsoft.Extensions.Configuration;
using TCP.ProxmoxInteractor.Factories;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.ProxmoxInteractor.Repositories;

public class NodesRepository : BaseProxmoxRepository, INodesRepository
{
    private readonly IConfiguration _configuration;
    
    public NodesRepository(IProxmoxClientFactory proxmoxClientFactory, IConfiguration configuration) : base(proxmoxClientFactory)
    {
        _configuration = configuration;
    }
    
    public async Task<IEnumerable<NodeItem>> ListNodes()
    {
        var client = _proxmoxClientFactory.Create();
        var nodes = await client.Nodes.Get();

        return nodes;
    }
}