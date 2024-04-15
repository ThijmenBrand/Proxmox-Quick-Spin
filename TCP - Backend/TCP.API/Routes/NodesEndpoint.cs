using Carter;
using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
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

    [ProducesResponseType(typeof(IEnumerable<NodeItem>), 200)]
    private async Task<IEnumerable<NodeItem>> ListAllNodes()
    {
        var result = await sender.Send(new ListProxmoxNodes());
        return result;
    }
}