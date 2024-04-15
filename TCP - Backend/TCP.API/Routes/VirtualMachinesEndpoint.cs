using System.Net;
using Carter;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Vm;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TCP.Application.Commands;
using TCP.Application.Queries;
using TCP.Core.Models;

namespace TCP.API.Routes;

public class VirutalMachinesEndpoint(ISender sender) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/vm/lxc", ListAllLxcMachines);
        app.MapGet("/vm/lxc/{node}", ListAllLxcMachinesInNode);
        app.MapGet("/vm/lxc/{node}/{vmId}", GetLxcMachine);
        app.MapPost("/vm/lxc/{node}/{vmId}/clone", CloneLxcMachine);
        app.MapGet("/vm/qemu", ListAllQemuMachines);
        app.MapGet("/vm/qemu/{node}", ListAllQemuMachinesInNode);
        app.MapGet("/vm/qemu/{node}/{vmId}", GetQemuMachine);
        app.MapPost("/vm/qemu/{node}/{vmId}/clone", CloneQemuMachine);
        app.MapPost("/vm/qemu/{node}/{vmId}/config", CreateOrUpdateQemuConfig);

    }

    [ProducesResponseType(typeof(IEnumerable<NodeVmLxc>), 200)]
    private async Task<IEnumerable<NodeVmLxc>> ListAllLxcMachines()
    {
        var result = await sender.Send(new ListAllLxcMachines());
        return result;
    }

    [ProducesResponseType(typeof(IEnumerable<NodeVmLxc>), 200)]
    private async Task<IEnumerable<NodeVmLxc>> ListAllLxcMachinesInNode(string node)
    {
        var result = await sender.Send(new ListLxcMachinesInNode(node));
        return result;
    }

    [ProducesResponseType(typeof(IEnumerable<NodeVmQemu>), 200)]
    private async Task<IEnumerable<NodeVmQemu>> ListAllQemuMachines()
    {
        var result = await sender.Send(new ListAllQemuMachines());
        return result;
    }

    [ProducesResponseType(typeof(IEnumerable<NodeVmQemu>), 200)]
    private async Task<IEnumerable<NodeVmQemu>> ListAllQemuMachinesInNode(string node)
    {
        var result = await sender.Send(new ListQemuMachinesInNode(node));
        return result;
    }

    [ProducesResponseType(typeof(VmConfigLxc), 200)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    private async Task<IResult> GetLxcMachine(string node, int vmId)
    {
        var result = await sender.Send(new GetLxcMachine(node, vmId));
        return result == null ? Results.NotFound() : Results.Ok(result);
    }
    
    [ProducesResponseType(typeof(VmConfigQemu), 200)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    private async Task<IResult> GetQemuMachine(string node, int vmId)
    {
        var result = await sender.Send(new GetQemuMachine(node, vmId));
        return result == null ? Results.NotFound() : Results.Ok(result);
    }

    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    private async Task<IResult> CloneLxcMachine(string node, int vmId, CloneConfiguration configuration)
    {
        var result = await sender.Send(new CloneLxcMachineCommand(node, vmId, configuration));
        return result.Data != null ? Results.Ok(result.Data) : Results.Problem(result.ErrorMessage);
    }
    
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    private async Task<IResult> CloneQemuMachine(string node, int vmId, CloneConfiguration configuration)
    {
        var result = await sender.Send(new CloneQemuMachineCommand(node, vmId, configuration));
        return result.Data != null ? Results.Ok(result.Data) : Results.Problem(result.ErrorMessage);
    }
    
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
    private async Task<IResult> CreateOrUpdateQemuConfig(string node, int vmId, QemuConfiguration configuration)
    {
        var result = await sender.Send(new CreateOrUpdateQemuConfigurationCommand(node, vmId, configuration));
        return result.Data != null ? Results.Ok(result.Data) : Results.Problem(result.ErrorMessage);
    }
}