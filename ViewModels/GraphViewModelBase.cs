using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuickGraph;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Associativy.FrontendEngines.ViewModels
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public abstract class GraphViewModelBase : IGraphViewModel
    {
        public IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> Graph { get; set; }
    }
}