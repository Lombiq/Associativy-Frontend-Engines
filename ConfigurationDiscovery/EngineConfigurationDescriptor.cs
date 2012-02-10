using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Associativy.GraphDiscovery;
using Associativy.Frontends.Engines;
using Orchard.ContentManagement;

namespace Associativy.Frontends.ConfigurationDiscovery
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class EngineConfigurationDescriptor : Freezable
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

        private Func<IContentQuery<ContentItem>, IContentQuery<ContentItem>> _graphQueryModifier;
        public Func<IContentQuery<ContentItem>, IContentQuery<ContentItem>> GraphQueryModifier
        {
            get { return _graphQueryModifier; }
            set
            {
                ThrowIfFrozen();
                _graphQueryModifier = value;
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
}