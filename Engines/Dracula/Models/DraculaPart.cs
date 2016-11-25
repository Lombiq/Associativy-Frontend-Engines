using System.Collections.Generic;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Dracula.Models
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class DraculaPart : ContentPart
    {
        private readonly LazyField<Dictionary<int, NodeViewModel>> _nodes = new LazyField<Dictionary<int, NodeViewModel>>();
        internal LazyField<Dictionary<int, NodeViewModel>> NodesField { get { return _nodes; } }
        public Dictionary<int, NodeViewModel> Nodes
        {
            get { return _nodes.Value; }
        }
    }
}