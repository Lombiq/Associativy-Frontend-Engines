﻿using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;
using Associativy.Models.Services;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Models.Pages.Frontends
{
    /// <summary>
    /// All engine pages should contain this part or another part implementing the aspect
    /// </summary>
    public class AssociativyFrontendCommonPart : ContentPart, IEngineConfigurationAspect
    {
        public IGraphDescriptor GraphDescriptor { get; set; }
        public IEngineContext EngineContext { get; set; }
        public IMindSettings MindSettings { get; set; }
        public GraphSettings GraphSettings { get; set; }


        public AssociativyFrontendCommonPart()
        {
            MindSettings = Associativy.Models.Services.MindSettings.Default;
            GraphSettings = GraphSettings.Default;
        }
    }
}