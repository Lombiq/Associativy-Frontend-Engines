using Associativy.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Associativy.FrontendEngines.Models;

namespace Associativy.FrontendEngines.Handlers
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public abstract class SearchFormHandlerBase<TFrontendEngineContext> : ContentHandler
        where TFrontendEngineContext : IFrontendEngineContext, new()
    {
        protected SearchFormHandlerBase()
        {
            var frontendEngineContext = new TFrontendEngineContext();
            Filters.Add(new ActivatingFilter<SearchFormPart>(frontendEngineContext.SearchFormContentType));
        }
    }
}