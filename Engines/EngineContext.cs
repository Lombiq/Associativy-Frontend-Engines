using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines
{
    [OrchardFeature("Associativy.Frontends")]
    public class EngineContext : IEngineContext
    {
        public string EngineName { get; set; }
    }
}