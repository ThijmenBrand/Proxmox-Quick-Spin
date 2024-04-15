using Corsinvest.ProxmoxVE.Api;

namespace TCP.ProxmoxInteractor.Repositories.Interfaces;

public interface IProxmoxRepository
{
    public Task<string> GetProxmoxVersion();
}