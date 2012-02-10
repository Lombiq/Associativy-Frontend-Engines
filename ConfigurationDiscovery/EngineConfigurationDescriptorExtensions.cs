using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Models.Mind;
using Associativy.Frontends.ConfigurationDiscovery;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.ConfigurationDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public static class EngineConfigurationDescriptorExtensions
    {
        public static IMindSettings MakeDefaultMindSettings(this EngineConfigurationDescriptor configurationDescriptor)
        {
            return new MindSettings()
            {
                ZoomLevel = 0,
                MaxZoomLevel = configurationDescriptor.MaxZoomLevel,
                QueryModifier = configurationDescriptor.GraphQueryModifier
            };
        }
    }
}