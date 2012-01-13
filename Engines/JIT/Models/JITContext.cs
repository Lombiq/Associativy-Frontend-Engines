using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.FrontendEngines.Models;
using Orchard.Environment.Extensions;

namespace Associativy.FrontendEngines.Engines.JIT.Models
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class JITContext : IFrontendEngineContext
    {
        public string Name
        {
            get { return "JIT"; }
        }

        public string SearchFormContentType
        {
            get { return "JITSearchForm"; }
        }
    }
}