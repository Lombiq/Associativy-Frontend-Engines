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
        public void QueryBuilt(QueryBuiltContext context)
        {
            var recordQuery = context.Query.Where<CommonPartRecord>(r => true);
            foreach (var contentType in context.GraphDescriptor.ContentTypes)
            {
                recordQuery.WithQueryHintsFor(contentType);
            }
        }
    }
}