using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.Models;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Dracula.Models
{
    [OrchardFeature("Associativy.Frontends")]
    public class DraculaContext : IFrontendEngineContext
    {
        public string Name
        {
            get { return "Dracula"; }
        }

        public string SearchFormContentType
        {
            get { return "DraculaSearchForm"; }
        }
    }
}