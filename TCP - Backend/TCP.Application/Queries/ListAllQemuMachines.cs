using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using MediatR;
using TCP.Core.Utils;
using TCP.ProxmoxInteractor;
using TCP.ProxmoxInteractor.Repositories;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.Application.Queries;

public record ListAllQemuMachines() : IRequest<IEnumerable<NodeVmQemu>>;

public class ListAllQemuMachinesHandler : IRequestHandler<ListAllQemuMachines, IEnumerable<NodeVmQemu>>
{
    private readonly INodesRepository _nodesRepository;
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public ListAllQemuMachinesHandler(INodesRepository nodesRepository,
        IVirtualMachineRepository virtualMachineRepository)
    {
        _nodesRepository = nodesRepository;
        _virtualMachineRepository = virtualMachineRepository;
    }

    public async Task<IEnumerable<NodeVmQemu>> Handle(ListAllQemuMachines request, CancellationToken cancellationToken)
    {
        var nodes = await _nodesRepository.ListNodes();

        IEnumerable<NodeVmQemu> allMachines = new List<NodeVmQemu>();
        foreach (var node in nodes)
        {
            var allQemus = await _virtualMachineRepository.ListQemuMachines(node.Node);
            allMachines = allMachines.Concat(allQemus);
        }

        return allMachines;
    }
}