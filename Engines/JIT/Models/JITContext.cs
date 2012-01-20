using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.Models;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.JIT.Models
{
    [OrchardFeature("Associativy.Frontends")]
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