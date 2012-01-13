using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.FrontendEngines.Models;
using Orchard.Environment.Extensions;

namespace Associativy.FrontendEngines.Engines.Dracula.Models
{
    [OrchardFeature("Associativy.FrontendEngines")]
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