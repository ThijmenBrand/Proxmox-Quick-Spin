using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using MediatR;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.Application.Queries;

public record ListAllLxcMachines() : IRequest<IEnumerable<NodeVmLxc>>;

public class ListAllLxcMachinesHandler : IRequestHandler<ListAllLxcMachines, IEnumerable<NodeVmLxc>>
{
    private readonly INodesRepository _nodesRepository;
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public ListAllLxcMachinesHandler(INodesRepository nodesRepository,
        IVirtualMachineRepository virtualMachineRepository)
    {
        _nodesRepository = nodesRepository;
        _virtualMachineRepository = virtualMachineRepository;
    }

    public async Task<IEnumerable<NodeVmLxc>> Handle(ListAllLxcMachines request, CancellationToken cancellationToken)
    {
        var nodes = await _nodesRepository.ListNodes();

        IEnumerable<NodeVmLxc> allMachines = new List<NodeVmLxc>();
        foreach (var node in nodes)
        {
            var allLxcsInNode = await _virtualMachineRepository.ListLxcMachines(node.Node);
            allMachines = allMachines.Concat(allLxcsInNode);
        }

        return allMachines;
    }
}