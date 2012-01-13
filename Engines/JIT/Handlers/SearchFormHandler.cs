using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.FrontendEngines.Handlers;
using Associativy.FrontendEngines.Engines.JIT.Models;

namespace Associativy.FrontendEngines.Engines.JIT.Handlers
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class SearchFormHandler : SearchFormHandlerBase<JITContext>
    {
    }
}