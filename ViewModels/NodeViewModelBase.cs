using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement;

namespace Associativy.FrontendEngines.ViewModels
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public abstract class NodeViewModelBase
    {
        public IContent ContentItem { get; set; }
    }
}