using MediatR;
using TCP.Core.Models;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.Application.Commands;

public record CloneLxcMachineCommand(string Node, int VmId, CloneConfiguration Configuration) : IRequest<Response<string>>;

public class CloneLxcMachineCommandHandler : IRequestHandler<CloneLxcMachineCommand, Response<string>>
{
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public CloneLxcMachineCommandHandler(IVirtualMachineRepository virtualMachineRepository)
    {
        _virtualMachineRepository = virtualMachineRepository;
    }
    
    public async Task<Response<string>> Handle(CloneLxcMachineCommand request, CancellationToken cancellationToken)
    {
        var result = await _virtualMachineRepository.CloneLxcMachine(request.Node, request.VmId, request.Configuration);
        
        return new Response<string>()
        {
            StatusCode = result.StatusCode,
            ErrorMessage = result.ReasonPhrase,
            Data = result.Response.data,
        };
    }
}