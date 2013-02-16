using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.EventHandlers;
using Associativy.GraphDiscovery;
using Orchard.Core.Common.Models;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.EventHandlers
{
    [OrchardFeature("Associativy.Frontends")]
    public class NodeManagerEventHander : INodeManagerEventHander
    {
        private readonly IGraphManager _graphManager;


        public NodeManagerEventHander(IGraphManager graphManager)
        {
            _graphManager = graphManager;
        }


        public void QueryBuilt(QueryBuiltContext context)
        {
            var graph = _graphManager.FindGraph(context.GraphContext);

            if (graph == null) return;

            var recordQuery = context.Query.Where<CommonPartRecord>(r => true);
            foreach (var contentType in graph.ContentTypes)
            {
                recordQuery.WithQueryHintsFor(contentType);
            }
        }
    }
}