using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement;
using Associativy.GraphDiscovery;
using Associativy.Frontends.Engines;
using Orchard.Core.Common.Utilities;
using Associativy.Models.Mind;

namespace Associativy.Frontends.Models
{
    /// <summary>
    /// All engine pages should contain this part or another part implementing the aspect
    /// </summary>
    [OrchardFeature("Associativy.Frontends")]
    public class EngineCommonPart : ContentPart, IEngineConfigurationAspect
    {
        public IGraphContext GraphContext { get; set; }
        public IEngineContext EngineContext { get; set; }
        public IMindSettings MindSettings { get; set; }
    }
}