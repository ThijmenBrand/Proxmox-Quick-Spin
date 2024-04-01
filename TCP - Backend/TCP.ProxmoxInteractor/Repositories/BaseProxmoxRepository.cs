using TCP.ProxmoxInteractor.Factories;

namespace TCP.ProxmoxInteractor.Repositories;

public class BaseProxmoxRepository(IProxmoxClientFactory proxmoxClientFactory)
{
    protected readonly IProxmoxClientFactory _proxmoxClientFactory = proxmoxClientFactory;
}