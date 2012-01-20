using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickGraph;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.ViewModels
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class GraphViewModelBase : IGraphViewModel
    {
        public IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> Graph { get; set; }
    }
}