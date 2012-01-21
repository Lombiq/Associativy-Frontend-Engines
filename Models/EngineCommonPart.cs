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

        private readonly LazyField<string> _graphContextBase64 = new LazyField<string>();
        public LazyField<string> GraphContextBase64Field { get { return _graphContextBase64; } }
        public string GraphContextBase64
        {
            get { return _graphContextBase64.Value; }
        }

        public IEngineContext EngineContext { get; set; }

        public IEngineConfigurationProvider ConfigurationProvider { get; set; }
    }
}