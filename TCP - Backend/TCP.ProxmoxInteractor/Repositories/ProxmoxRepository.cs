using Corsinvest.ProxmoxVE.Api;
using TCP.ProxmoxInteractor.Factories;

namespace TCP.ProxmoxInteractor;

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