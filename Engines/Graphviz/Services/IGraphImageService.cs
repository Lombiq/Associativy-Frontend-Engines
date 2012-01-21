using System;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using QuickGraph;
using QuickGraph.Graphviz;
using Associativy.Models;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.Engines.Graphviz.Services
{
    public interface IGraphImageService : IDependency
    {
        string ToSvg(IGraphContext graphContext, IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph, Action<GraphvizAlgorithm<IContent, IUndirectedEdge<IContent>>> initialization);
    }
}
