using System;
using Associativy.GraphDiscovery;
using Orchard;
using Orchard.ContentManagement;
using QuickGraph;
using QuickGraph.Graphviz;

namespace Associativy.Frontends.Engines.Graphviz.Services
{
    public interface IGraphImageService : IDependency
    {
        string ToSvg(IGraphDescriptor graphDescriptor, IUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph, Action<GraphvizAlgorithm<IContent, IUndirectedEdge<IContent>>> initialization);
    }
}
