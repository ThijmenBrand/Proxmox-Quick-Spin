using MediatR;
using TCP.Core.Models;
using TCP.Core.Utils;
using TCP.ProxmoxInteractor;
using TCP.ProxmoxInteractor.Repositories;

namespace TCP.Application.Queries;

public record ListLxcMachinesInNode(string node) : IRequest<IEnumerable<Lxc>>;

public class ListLxcMachinesInNodeHandler : IRequestHandler<ListLxcMachinesInNode, IEnumerable<Lxc>>
{
    private readonly INodesRepository _nodesRepository;
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public ListLxcMachinesInNodeHandler(INodesRepository nodesRepository,
        IVirtualMachineRepository virtualMachineRepository)
    {
        _nodesRepository = nodesRepository;
        _virtualMachineRepository = virtualMachineRepository;
    }

    public async Task<IEnumerable<Lxc>> Handle(ListLxcMachinesInNode request, CancellationToken cancellationToken)
    {
        var allLxcInNode = await _virtualMachineRepository.ListIxcMachines(request.node);
        IEnumerable<Lxc> parsedResult = ProxmoxClientResultUnwrapper.Unwrap<IEnumerable<Lxc>>(allLxcInNode);

        return parsedResult;
    }
}