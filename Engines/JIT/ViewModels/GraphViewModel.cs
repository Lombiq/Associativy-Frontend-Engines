using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.ViewModels;

namespace Associativy.Frontends.Engines.JIT.ViewModels
{
    [OrchardFeature("Associativy.Frontends")]
    public class GraphViewModel : GraphViewModelBase
    {
        public int MaxZoomLevel { get; set; }
    }
}