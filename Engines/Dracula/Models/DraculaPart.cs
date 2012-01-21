using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using Orchard.Core.Common.Utilities;

namespace Associativy.Frontends.Engines.Dracula.Models
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class DraculaPart : ContentPart
    {
        private readonly LazyField<Dictionary<int, NodeViewModel>> _nodes = new LazyField<Dictionary<int, NodeViewModel>>();
        public LazyField<Dictionary<int, NodeViewModel>> NodesField { get { return _nodes; } }
        public Dictionary<int, NodeViewModel> Nodes
        {
            get { return _nodes.Value; }
        }
    }
}