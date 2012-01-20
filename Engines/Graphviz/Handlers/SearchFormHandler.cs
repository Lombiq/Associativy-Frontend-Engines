using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Handlers;
using Associativy.Frontends.Engines.Graphviz.Models;

namespace Associativy.Frontends.Engines.Graphviz.Handlers
{
    [OrchardFeature("Associativy.Frontends")]
    public class SearchFormHandler : SearchFormHandlerBase<GraphvizContext>
    {
    }
}