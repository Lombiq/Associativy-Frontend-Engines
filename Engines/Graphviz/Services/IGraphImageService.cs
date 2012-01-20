using System;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using QuickGraph;
using QuickGraph.Graphviz;
using Associativy.Models;
using Associativy.GraphDiscovery;

namespace Associativy.FrontendEngines.Engines.Graphviz.Services
{
    public interface IGraphImageService
    {
        string ToSvg(IGraphContext graphContext, IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph, Action<GraphvizAlgorithm<IContent, IUndirectedEdge<IContent>>> initialization);
    }
}
