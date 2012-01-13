using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.FrontendEngines.Handlers;
using Associativy.FrontendEngines.Engines.Graphviz.Models;

namespace Associativy.FrontendEngines.Engines.Graphviz.Handlers
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class SearchFormHandler : SearchFormHandlerBase<GraphvizContext>
    {
    }
}