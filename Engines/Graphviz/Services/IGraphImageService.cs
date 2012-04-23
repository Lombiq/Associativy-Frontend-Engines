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
        string ToSvg(IGraphContext graphContext, IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph, Action<GraphvizAlgorithm<IContent, IUndirectedEdge<IContent>>> initialization);
    }
}
