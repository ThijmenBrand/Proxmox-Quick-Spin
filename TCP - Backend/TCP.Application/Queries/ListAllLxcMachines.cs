using MediatR;
using TCP.Core.Models;
using TCP.Core.Utils;
using TCP.ProxmoxInteractor;
using TCP.ProxmoxInteractor.Repositories;

namespace TCP.Application.Queries;

public record ListAllLxcMachines() : IRequest<IEnumerable<Lxc>>;

public class ListAllLxcMachinesHandler : IRequestHandler<ListAllLxcMachines, IEnumerable<Lxc>>
{
    private readonly INodesRepository _nodesRepository;
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public ListAllLxcMachinesHandler(INodesRepository nodesRepository,
        IVirtualMachineRepository virtualMachineRepository)
    {
        _nodesRepository = nodesRepository;
        _virtualMachineRepository = virtualMachineRepository;
    }

    public async Task<IEnumerable<Lxc>> Handle(ListAllLxcMachines request, CancellationToken cancellationToken)
    {
        var allNodesRaw = await _nodesRepository.ListNodes();
        IEnumerable<Node> nodes = ProxmoxClientResultUnwrapper.Unwrap<IEnumerable<Node>>(allNodesRaw);

        IEnumerable<Lxc> allMachines = new List<Lxc>();
        foreach (var node in nodes)
        {
            var allLxcsInNode = await _virtualMachineRepository.ListIxcMachines(node.node);
            IEnumerable<Lxc> unwrappedLxcList = ProxmoxClientResultUnwrapper.Unwrap<IEnumerable<Lxc>>(allLxcsInNode);
            allMachines = allMachines.Concat(unwrappedLxcList);
        }

        return allMachines;
    }
}