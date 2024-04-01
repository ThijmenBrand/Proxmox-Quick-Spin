using MediatR;
using Newtonsoft.Json;
using TCP.Core.Models;
using TCP.Core.Utils;
using TCP.ProxmoxInteractor;

namespace TCP.Application.Queries;


    public record ListProxmoxNodes() : IRequest<IEnumerable<string>>;

    public class ListProxmoxNodesHandler : IRequestHandler<ListProxmoxNodes, IEnumerable<string>>
    {
        private readonly INodesRepository _nodesRepository;

        public ListProxmoxNodesHandler(INodesRepository nodesRepository)
        {
            _nodesRepository = nodesRepository;
        }
    
        public async Task<IEnumerable<string>> Handle(ListProxmoxNodes request, CancellationToken cancellationToken)
        {
            var nodesObject = await _nodesRepository.ListNodes();
            IEnumerable<Node> parsedResult = ProxmoxClientResultUnwrapper.Unwrap<IEnumerable<Node>>(nodesObject);

            IEnumerable<string> nodesList = new List<string>();

            return parsedResult.Aggregate(nodesList, (current, node) => current.Append(node.node));
        }
    }
