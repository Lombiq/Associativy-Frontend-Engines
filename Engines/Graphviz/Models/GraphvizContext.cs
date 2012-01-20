using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.Models;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Graphviz.Models
{
    [OrchardFeature("Associativy.Frontends")]
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