using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement;

namespace Associativy.Frontends.ViewModels
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class NodeViewModelBase
    {
        public IContent ContentItem { get; set; }
    }
}