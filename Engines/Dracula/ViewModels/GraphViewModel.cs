using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.ViewModels;

namespace Associativy.Frontends.Engines.Dracula.ViewModels
{
    [OrchardFeature("Associativy.Frontends")]
    public class GraphViewModel : GraphViewModelBase
    {
        public Dictionary<int, NodeViewModel> Nodes { get; set; }
    }
}