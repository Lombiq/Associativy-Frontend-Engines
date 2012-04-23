using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement;
using System.ComponentModel.DataAnnotations;
using QuickGraph;
using Orchard.Core.Common.Utilities;

namespace Associativy.Frontends.Models
{
    [OrchardFeature("Associativy.Frontends")]
    public class GraphPart : ContentPart, IGraphAspect
    {
        public IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> Graph { get; set; }

        private readonly LazyField<int> _zoomLevelCount = new LazyField<int>();
        public LazyField<int> ZoomLevelCountField { get { return _zoomLevelCount; } }
        public int ZoomLevelCount
        {
            get { return _zoomLevelCount.Value; }
        }
    }
}