using System;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using QuickGraph;
using QuickGraph.Graphviz;

namespace Associativy.FrontendEngines.Engines.Graphviz.Services
{
    public interface IGraphImageService : IAssociativyService, IDependency
    {
        string ToSvg(IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph, Action<GraphvizAlgorithm<IContent, IUndirectedEdge<IContent>>> initialization);
    }
}
