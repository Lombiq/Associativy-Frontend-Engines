using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.FrontendEngines.Handlers;
using Associativy.FrontendEngines.Engines.Dracula.Models;

namespace Associativy.FrontendEngines.Engines.Dracula.Handlers
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class SearchFormHandler : SearchFormHandlerBase<DraculaContext>
    {
    }
}