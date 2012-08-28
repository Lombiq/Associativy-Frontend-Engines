﻿using System.Web.Script.Serialization;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.ViewModels
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class NodeViewModelBase
    {
        [ScriptIgnore]
        public IContent ContentItem { get; set; }
    }
}