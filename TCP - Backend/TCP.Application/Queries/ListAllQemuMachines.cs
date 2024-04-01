using MediatR;
using TCP.Core.Models;
using TCP.Core.Utils;
using TCP.ProxmoxInteractor;
using TCP.ProxmoxInteractor.Repositories;

namespace TCP.Application.Queries;

public record ListAllQemuMachines() : IRequest<IEnumerable<Qemu>>;

public class ListAllQemuMachinesHandler : IRequestHandler<ListAllQemuMachines, IEnumerable<Qemu>>
{
    private readonly INodesRepository _nodesRepository;
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public ListAllQemuMachinesHandler(INodesRepository nodesRepository,
        IVirtualMachineRepository virtualMachineRepository)
    {
        _nodesRepository = nodesRepository;
        _virtualMachineRepository = virtualMachineRepository;
    }

    public async Task<IEnumerable<Qemu>> Handle(ListAllQemuMachines request, CancellationToken cancellationToken)
    {
        var allNodesRaw = await _nodesRepository.ListNodes();
        IEnumerable<Node> nodes = ProxmoxClientResultUnwrapper.Unwrap<IEnumerable<Node>>(allNodesRaw);

        IEnumerable<Qemu> allMachines = new List<Qemu>();
        foreach (var node in nodes)
        {
            var allQemus = await _virtualMachineRepository.ListQemuMachines(node.node);
            IEnumerable<Qemu> unwrappedLxcList = ProxmoxClientResultUnwrapper.Unwrap<IEnumerable<Qemu>>(allQemus);
            allMachines = allMachines.Concat(unwrappedLxcList);
        }

        return allMachines;
    }
}