﻿using Associativy.Models.Mind;
using Orchard.ContentManagement;
using QuickGraph;

namespace Associativy.Frontends.Models
{
    public delegate IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> GraphRetriever(IMindSettings settings);

    public interface IGraphRetrieverAspect : IContent
    {
        GraphRetriever RetrieveGraph { get; }
    }
}