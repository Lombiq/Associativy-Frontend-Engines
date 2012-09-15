﻿using Associativy.Frontends.Controllers;
using Associativy.Frontends.Services;
using Associativy.Services;
using Orchard;
using Orchard.Environment.Extensions;
using Piedone.HelpfulLibraries.Contents.DynamicPages;

namespace Associativy.Frontends.Engines.Dracula.Controllers
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class DraculaEngineController : EngineControllerBase
    {
        private readonly IEngineContext _engineContext = new EngineContext { EngineName = "Dracula" };
        protected override IEngineContext EngineContext
        {
            get { return _engineContext; }
        }

        public DraculaEngineController(
            IAssociativyServices associativyServices,
            IFrontendServices frontendServices,
            IPageEventHandler eventHandler,
            IOrchardServices orchardServices)
            : base(associativyServices, frontendServices, eventHandler, orchardServices)
        {
        }
    }
}