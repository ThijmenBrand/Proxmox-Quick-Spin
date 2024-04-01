using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TCP.Application.Queries;
using TCP.Core.Models;

namespace TCP.API.Routes;

public class VirutalMachinesEndpoint(ISender sender) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/vm/lxc", ListAllLxcMachines);
        app.MapGet("/vm/lxc/{node}", ListAllLxcMachinesInNode);
        app.MapGet("/vm/qemu", ListAllQemuMachines);
        app.MapGet("/vm/qemu/{node}", ListAllQemuMachinesInNode);
    }

    [ProducesResponseType(typeof(IEnumerable<Lxc>), 200)]
    private async Task<IEnumerable<Lxc>> ListAllLxcMachines()
    {
        var result = await sender.Send(new ListAllLxcMachines());
        return result;
    }

    [ProducesResponseType(typeof(IEnumerable<Lxc>), 200)]
    private async Task<IEnumerable<Lxc>> ListAllLxcMachinesInNode(string node)
    {
        var result = await sender.Send(new ListLxcMachinesInNode(node));
        return result;
    }

    [ProducesResponseType(typeof(IEnumerable<Qemu>), 200)]
    private async Task<IEnumerable<Qemu>> ListAllQemuMachines()
    {
        var result = await sender.Send(new ListAllQemuMachines());
        return result;
    }

    [ProducesResponseType(typeof(IEnumerable<Qemu>), 200)]
    private async Task<IEnumerable<Qemu>> ListAllQemuMachinesInNode(string node)
    {
        var result = await sender.Send(new ListQemuMachinesInNode(node));
        return result;
    }
}