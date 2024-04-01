using MediatR;
using TCP.Core.Models;
using TCP.Core.Utils;
using TCP.ProxmoxInteractor;
using TCP.ProxmoxInteractor.Repositories;

namespace TCP.Application.Queries;

public record ListQemuMachinesInNode(string node) : IRequest<IEnumerable<Qemu>>;

public class ListQemuMachinesInNodeHandler : IRequestHandler<ListQemuMachinesInNode, IEnumerable<Qemu>>
{
    private readonly INodesRepository _nodesRepository;
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public ListQemuMachinesInNodeHandler(INodesRepository nodesRepository,
        IVirtualMachineRepository virtualMachineRepository)
    {
        _nodesRepository = nodesRepository;
        _virtualMachineRepository = virtualMachineRepository;
    }

    public async Task<IEnumerable<Qemu>> Handle(ListQemuMachinesInNode request, CancellationToken cancellationToken)
    {
        var allQemuInNode = await _virtualMachineRepository.ListQemuMachines(request.node);
        IEnumerable<Qemu> parsedResult = ProxmoxClientResultUnwrapper.Unwrap<IEnumerable<Qemu>>(allQemuInNode);

        return parsedResult;
    }
}