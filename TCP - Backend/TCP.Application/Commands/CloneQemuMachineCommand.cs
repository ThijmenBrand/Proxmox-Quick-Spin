using MediatR;
using TCP.Core.Models;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.Application.Commands;

public record CloneQemuMachineCommand(string Node, int VmId, CloneConfiguration Configuration) : IRequest<Response<string>>;

public class CloneQemuMachineCommandHandler : IRequestHandler<CloneQemuMachineCommand, Response<string>>
{
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public CloneQemuMachineCommandHandler(IVirtualMachineRepository virtualMachineRepository)
    {
        _virtualMachineRepository = virtualMachineRepository;
    }
    
    public async Task<Response<string>> Handle(CloneQemuMachineCommand request, CancellationToken cancellationToken)
    {
        var result = await _virtualMachineRepository.CloneQemuMachine(request.Node, request.VmId, request.Configuration);

        return new Response<string>()
        {
            StatusCode = result.StatusCode,
            ErrorMessage = result.ReasonPhrase,
            Data = result.Response.data,
        };
    }
}