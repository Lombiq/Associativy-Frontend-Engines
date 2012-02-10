using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement;
using Associativy.GraphDiscovery;
using Associativy.Frontends.Engines;
using Orchard.Core.Common.Utilities;
using Associativy.Frontends.ConfigurationDiscovery;

namespace Associativy.Frontends.Models
{
    [OrchardFeature("Associativy.Frontends")]
    public class EngineCommonPart : ContentPart
    {
        public IGraphContext GraphContext { get; set; }

        public IEngineContext EngineContext { get; set; }

        public EngineConfigurationDescriptor ConfigurationDescriptor { get; set; }
    }
}