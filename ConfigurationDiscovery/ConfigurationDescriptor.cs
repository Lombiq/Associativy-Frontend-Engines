using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.GraphDiscovery;
using Associativy.Frontends.Engines;
using Orchard.ContentManagement;
using Associativy.Models.Mind;
using Piedone.HelpfulLibraries.Utilities;

namespace Associativy.Frontends.ConfigurationDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class ConfigurationDescriptor : FreezableBase
    {
        private IEngineContext _engineContext;
        public IEngineContext EngineContext
        {
            get { return _engineContext; }
            set
            {
                ThrowIfFrozen();
                _engineContext = value;
            }
        }

        private IGraphContext _graphContext;
        public IGraphContext GraphContext
        {
            get { return _graphContext; }
            set
            {
                ThrowIfFrozen();
                _graphContext = value;
            }
        }

        public delegate void GraphQueryModifier(IContentQuery<ContentItem> query);

        private GraphQueryModifier _modifyGraphQuery;
        public GraphQueryModifier ModifyGraphQuery
        {
            get { return _modifyGraphQuery; }
            set
            {
                ThrowIfFrozen();
                _modifyGraphQuery = value;
            }
        }

        private int _maxZoomLevel;
        public int MaxZoomLevel
        {
            get { return _maxZoomLevel; }
            set
            {
                ThrowIfFrozen();
                _maxZoomLevel = value;
            }
        }
    }

    [OrchardFeature("Associativy.Frontends")]
    public static class EngineConfigurationDescriptorExtensions
    {
        public static IMindSettings MakeDefaultMindSettings(this ConfigurationDescriptor configurationDescriptor)
        {
            return new MindSettings()
            {
                ZoomLevel = 0,
                MaxZoomLevel = configurationDescriptor.MaxZoomLevel,
                ModifyQuery = configurationDescriptor.ModifyGraphQuery.Invoke
            };
        }
    }
}