using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement.Handlers;
using Associativy.Frontends.Models;
using Associativy.Services;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Engines.Graphviz.Handlers
{
    [OrchardFeature("Associativy.Frontends.Graphviz")]
    public class GraphvizHandler : EngineHandlerBase
    {
        public GraphvizHandler(IAssociativyServices associativyServices)
            : base(associativyServices)
        {
            var engineContext = DefaultGraphvizConfigurationProvider.DescribedEngineContext;

            AddCommonPartsToBasicContentTypes(engineContext);
            AddPart<SearchFormPart>(engineContext, "Render");
            AddPart<EngineCommonPart>(engineContext, "Render");
        }
    }
}