using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;
using Orchard.ContentManagement;

namespace Associativy.FrontendEngines.ViewModels
{
    public interface IGraphViewModel
    {
        IUndirectedGraph<IContent, IUndirectedEdge<IContent>> Graph { get; set; }
    }
}
