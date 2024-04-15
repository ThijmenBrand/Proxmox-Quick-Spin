using TCP.ProxmoxInteractor.Factories;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.ProxmoxInteractor.Repositories;

public class ProxmoxRepository: IProxmoxRepository
{
    private readonly IProxmoxClientFactory _proxmoxClientFactory;

    public ProxmoxRepository(IProxmoxClientFactory proxmoxClientFactory)
    {
        _proxmoxClientFactory = proxmoxClientFactory;
    }
    
    public async Task<string> GetProxmoxVersion()
    {
        var proxmoxClient =
            _proxmoxClientFactory.Create();

        var version = await proxmoxClient.Version.Version();

        return version.Response;
    }
}