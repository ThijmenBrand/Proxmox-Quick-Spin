using MediatR;
using TCP.Core.Models;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.Application.Commands;

public record CreateOrUpdateQemuConfigurationCommand(string Node, int VmId, QemuConfiguration Configuration)
    : IRequest<Response<string>>;

public class CreateOrUpdateQemuConfigurationCommandHandler : IRequestHandler<CreateOrUpdateQemuConfigurationCommand, Response<string>>
{
    private readonly IVirtualMachineRepository _virtualMachineRepository;

    public CreateOrUpdateQemuConfigurationCommandHandler(IVirtualMachineRepository virtualMachineRepository)
    {
        _virtualMachineRepository = virtualMachineRepository;
    }

    public async Task<Response<string>> Handle(CreateOrUpdateQemuConfigurationCommand request,
        CancellationToken cancellationToken)
    {
        
        var result =
            await _virtualMachineRepository.CreateOrUpdateQemuConfig(request.Node, request.VmId, request.Configuration);

        return new Response<string>()
        {
            StatusCode = result.StatusCode,
            ErrorMessage = result.ReasonPhrase,
            Data = result.Response.data,
        };
    }
}