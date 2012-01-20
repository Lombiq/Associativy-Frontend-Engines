using Associativy.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;
using Associativy.Frontends.Models;

namespace Associativy.Frontends.Handlers
{
    [OrchardFeature("Associativy.Frontends")]
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