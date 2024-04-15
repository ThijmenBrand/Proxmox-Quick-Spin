using Corsinvest.ProxmoxVE.Api.Shared.Models.Node;
using MediatR;
using Newtonsoft.Json;
using TCP.Core.Utils;
using TCP.ProxmoxInteractor;
using TCP.ProxmoxInteractor.Repositories.Interfaces;

namespace TCP.Application.Queries;


    public record ListProxmoxNodes() : IRequest<IEnumerable<NodeItem>>;

    public class ListProxmoxNodesHandler : IRequestHandler<ListProxmoxNodes, IEnumerable<NodeItem>>
    {
        private readonly INodesRepository _nodesRepository;

        public ListProxmoxNodesHandler(INodesRepository nodesRepository)
        {
            _nodesRepository = nodesRepository;
        }
    
        public async Task<IEnumerable<NodeItem>> Handle(ListProxmoxNodes request, CancellationToken cancellationToken)
        {
            var allNodes = await _nodesRepository.ListNodes();
            
            return allNodes;
        }
    }
