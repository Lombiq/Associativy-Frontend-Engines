using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;
using QuickGraph;
using Associativy.Models.Mind;

namespace Associativy.Frontends.Models
{
    public delegate IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> GraphRetriever(IMindSettings settings);

    public interface IGraphRetrieverAspect : IContent
    {
        GraphRetriever RetrieveGraph { get; }
    }
}
