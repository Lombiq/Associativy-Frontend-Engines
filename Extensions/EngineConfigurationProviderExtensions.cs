using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Models.Mind;
using Associativy.Frontends.ConfigurationDiscovery;

namespace Associativy.Frontends.Extensions
{
    public static class EngineConfigurationProviderExtensions
    {
        public static IMindSettings MakeDefaultMindSettings(this IEngineConfigurationProvider configurationProvider)
        {
            return new MindSettings()
            {
                ZoomLevel = 0,
                MaxZoomLevel = configurationProvider.MaxZoomLevel,
                QueryModifier = configurationProvider.GraphQueryModifier
            };
        }
    }
}