using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.FrontendEngines.ViewModels;

namespace Associativy.FrontendEngines.Engines.Dracula.ViewModels
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class GraphViewModel : GraphViewModelBase
    {
        public Dictionary<int, NodeViewModel> Nodes { get; set; }
    }
}