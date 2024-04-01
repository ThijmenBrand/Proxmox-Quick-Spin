using Corsinvest.ProxmoxVE.Api;

namespace TCP.ProxmoxInteractor;

public interface INodesRepository
{
    Task<dynamic> ListNodes();
}