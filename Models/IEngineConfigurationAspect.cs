using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;
using Associativy.Models.Mind;
using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.Models
{
    public interface IEngineConfigurationAspect : IContent
    {
        IGraphContext GraphContext { get; set; }
        IEngineContext EngineContext { get; set; }
        IMindSettings MindSettings { get; set; }
    }
}
