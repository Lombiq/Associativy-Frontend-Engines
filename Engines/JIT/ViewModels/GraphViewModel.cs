using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.FrontendEngines.ViewModels;

namespace Associativy.FrontendEngines.Engines.JIT.ViewModels
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class GraphViewModel : GraphViewModelBase
    {
        public int MaxZoomLevel { get; set; }
    }
}