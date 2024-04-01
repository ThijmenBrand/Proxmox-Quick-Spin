using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TCP.Application.Queries;

namespace TCP.API.Routes;

public class NodesEndpoint(ISender sender) : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/nodes", ListAllNodes);
    }

    [ProducesResponseType(typeof(IEnumerable<string>), 200)]
    private async Task<IEnumerable<string>> ListAllNodes()
    {
        var result = await sender.Send(new ListProxmoxNodes());
        return result;
    }
}