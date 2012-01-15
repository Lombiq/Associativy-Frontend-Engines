using System;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using QuickGraph;
using QuickGraph.Graphviz;
using Associativy.Models;

namespace Associativy.FrontendEngines.Engines.Graphviz.Services
{
    public interface IGraphImageService<TAssociativyGraphDescriptor> : IAssociativyService
        where TAssociativyGraphDescriptor : IAssociativyGraphDescriptor
    {
        string ToSvg(IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph, Action<GraphvizAlgorithm<IContent, IUndirectedEdge<IContent>>> initialization);
    }

    public interface IGraphImageService : IGraphImageService<IAssociativyGraphDescriptor>, IDependency
    {
    }
}
