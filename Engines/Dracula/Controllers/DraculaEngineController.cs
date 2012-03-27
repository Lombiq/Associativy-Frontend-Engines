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
using Associativy.Frontends.ConfigurationDiscovery;

namespace Associativy.Frontends.Engines.Dracula.Controllers
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class DraculaEngineController : EngineControllerBase<DraculaConfigurationDescriptor>
    {
        protected override IEngineContext EngineContext
        {
            get { return DefaultDraculaConfigurationProvider.DescribedEngineContext; }
        }

        public DraculaEngineController(
            IAssociativyServices associativyServices,
            IOrchardServices orchardServices,
            IConfigurationManager configurationManager)
            : base(associativyServices, orchardServices, configurationManager)
        {
        }
    }
}