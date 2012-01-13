using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.FrontendEngines.Models;
using Orchard.Environment.Extensions;

namespace Associativy.FrontendEngines.Engines.Graphviz.Models
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class GraphvizContext : IFrontendEngineContext
    {
        public string Name
        {
            get { return "Graphviz"; }
        }

        public string SearchFormContentType
        {
            get { return "GraphvizSearchForm"; }
        }
    }
}