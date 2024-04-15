using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using MediatR;
using TCP.Core.Utils;
using TCP.ProxmoxInteractor;
using TCP.ProxmoxInteractor.Repositories;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.Application.Queries;

public record ListQemuMachinesInNode(string node) : IRequest<IEnumerable<NodeVmQemu>>;

public class ListQemuMachinesInNodeHandler : IRequestHandler<ListQemuMachinesInNode, IEnumerable<NodeVmQemu>>
{
    private readonly INodesRepository _nodesRepository;
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public ListQemuMachinesInNodeHandler(INodesRepository nodesRepository,
        IVirtualMachineRepository virtualMachineRepository)
    {
        _nodesRepository = nodesRepository;
        _virtualMachineRepository = virtualMachineRepository;
    }

    public async Task<IEnumerable<NodeVmQemu>> Handle(ListQemuMachinesInNode request, CancellationToken cancellationToken)
    {
        var allQemuInNode = await _virtualMachineRepository.ListQemuMachines(request.node);

        return allQemuInNode;
    }
}