using Corsinvest.ProxmoxVE.Api;

namespace TCP.ProxmoxInteractor.Factories;

public interface IProxmoxClientFactory
{
    PveClient Create(int port = 8006);
    PveClient Create(string ip, string username, string password, int port = 8006);
}