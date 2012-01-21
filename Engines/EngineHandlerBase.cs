using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement.Handlers;
using Associativy.Frontends.Models;
using Associativy.Services;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Engines
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class EngineHandlerBase : ContentHandler
    {
        protected readonly IAssociativyServices _associativyServices;
        protected readonly List<string> _basicContentTypes;

        protected EngineHandlerBase(IAssociativyServices associativyServices)
        {
            _associativyServices = associativyServices;

            _basicContentTypes = new List<string>
            {
                "ShowWholeGraph",
                "ShowAssociations"
            };
        }

        protected virtual void AddCommonPartsToBasicContentTypes(IEngineContext engineContext)
        {
            AddCommonParts(engineContext, _basicContentTypes.ToArray());
        }

        protected virtual void AddPartToBasicContentTypes<TPart>(IEngineContext engineContext)
            where TPart : ContentPart, new()
        {
            AddPart<TPart>(engineContext, _basicContentTypes.ToArray());
        }

        protected virtual void AddCommonParts(IEngineContext engineContext, params string[] contentTypes)
        {
            AddPart<EngineCommonPart>(engineContext, contentTypes);
            AddPart<GraphPart>(engineContext, contentTypes);
            OnActivated<GraphPart>((context, part) =>
            {
                part.ZoomLevelCountField.Loader(() => _associativyServices.GraphService.CalculateZoomLevelCount(part.As<GraphPart>().Graph, part.As<EngineCommonPart>().ConfigurationProvider.MaxZoomLevel));
            });
            AddPart<SearchFormPart>(engineContext, contentTypes);
        }

        protected virtual void AddPart<TPart>(IEngineContext engineContext, params string[] contentTypes)
            where TPart : ContentPart, new()
        {
            Filters.Add(new ActivatingFilter<TPart>(contentTypes.Select(type => engineContext.EngineName + type).ToArray()));
        }
    }
}