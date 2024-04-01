namespace TCP.ProxmoxInteractor;

public interface IProxmoxRepository
{
    public Task<string> GetProxmoxVersion();
}