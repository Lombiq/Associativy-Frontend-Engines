using Associativy.EventHandlers;
using Orchard.Core.Common.Models;

namespace Associativy.Frontends.EventHandlers
{
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