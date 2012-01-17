using System;
using Associativy.Services;
using Orchard;
using Orchard.ContentManagement;
using QuickGraph;
using QuickGraph.Graphviz;
using Associativy.Models;

namespace Associativy.FrontendEngines.Engines.Graphviz.Services
{
    public interface IGraphImageService<TGraphDescriptor> : IAssociativyService
        where TGraphDescriptor : IGraphDescriptor
    {
        string ToSvg(IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> graph, Action<GraphvizAlgorithm<IContent, IUndirectedEdge<IContent>>> initialization);
    }

    public interface IGraphImageService : IGraphImageService<IGraphDescriptor>, IDependency
    {
    }
}
