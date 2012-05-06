using System.Collections.Generic;
using Associativy.Frontends.ViewModels;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Jit.ViewModels
{
    [OrchardFeature("Associativy.Frontends.Jit")]
    public class NodeViewModel : NodeViewModelBase
    {
        // Naming is Jit naming for easy JSON serialization
        public string id { get; set; }
        public string name { get; set; }
        public List<string> adjacencies { get; set; }
        public Dictionary<string, string> data { get; set; }

        public NodeViewModel()
        {
            adjacencies = new List<string>();
            data = new Dictionary<string, string>();
        }
    }
}