using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using MediatR;
using TCP.Core.Utils;
using TCP.ProxmoxInteractor;
using TCP.ProxmoxInteractor.Repositories;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.Application.Queries;

public record ListLxcMachinesInNode(string node) : IRequest<IEnumerable<NodeVmLxc>>;

public class ListLxcMachinesInNodeHandler : IRequestHandler<ListLxcMachinesInNode, IEnumerable<NodeVmLxc>>
{
    private readonly INodesRepository _nodesRepository;
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public ListLxcMachinesInNodeHandler(INodesRepository nodesRepository,
        IVirtualMachineRepository virtualMachineRepository)
    {
        _nodesRepository = nodesRepository;
        _virtualMachineRepository = virtualMachineRepository;
    }

    public async Task<IEnumerable<NodeVmLxc>> Handle(ListLxcMachinesInNode request, CancellationToken cancellationToken)
    {
        var allLxcInNode = await _virtualMachineRepository.ListLxcMachines(request.node);

        return allLxcInNode;
    }
}