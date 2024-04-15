
using Corsinvest.ProxmoxVE.Api;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;

namespace TCP.ProxmoxInteractor.Repositories.Interfaces;

public interface INodesRepository
{
    Task<IEnumerable<NodeItem>> ListNodes();
}