using Associativy.Frontends.Controllers;
using Associativy.Services;
using Orchard;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using QuickGraph;
using Orchard.ContentManagement;
using Associativy.Frontends.Engines.Dracula.ViewModels;
using System.Collections.Generic;
using Associativy.Frontends.Models;
using Associativy.Frontends.Services;
using Associativy.Frontends.EventHandlers;

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
            IFrontendEngineEventHandler eventHandler,
            IOrchardServices orchardServices)
            : base(associativyServices, frontendServices, eventHandler, orchardServices)
        {
        }
    }
}