using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;
using MediatR;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.Application.Queries;

public record GetLxcMachine(string node, int vmId) : IRequest<VmConfigLxc>;

public class GetLxcMachineHandler : IRequestHandler<GetLxcMachine, VmConfigLxc>
{
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public GetLxcMachineHandler(IVirtualMachineRepository virtualMachineRepository)
    {
        _virtualMachineRepository = virtualMachineRepository;
    }

    public async Task<VmConfigLxc> Handle(GetLxcMachine request, CancellationToken cancellationToken)
    {
        var machine = await _virtualMachineRepository.GetLxcMachine(request.node, request.vmId);

        return machine;

    }
}