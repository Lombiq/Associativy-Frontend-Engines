using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Handlers;
using Associativy.Frontends.Engines.Dracula.Models;

namespace Associativy.Frontends.Engines.Dracula.Handlers
{
    [OrchardFeature("Associativy.Frontends")]
    public class SearchFormHandler : SearchFormHandlerBase<DraculaContext>
    {
    }
}