using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement.Handlers;
using Associativy.Frontends.Models;
using Associativy.Services;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Engines.JIT.Handlers
{
    [OrchardFeature("Associativy.Frontends.JIT")]
    public class JITHandler : EngineHandlerBase
    {
        public JITHandler(IAssociativyServices associativyServices)
            : base(associativyServices)
        {
            var engineContext = DefaultJITConfigurationProvider.DescribedEngineContext;

            AddCommonPartsToBasicContentTypes(engineContext);
            AddPart<SearchFormPart>(engineContext, "FetchAssociations");
            AddPart<EngineCommonPart>(engineContext, "FetchAssociations");
        }
    }
}