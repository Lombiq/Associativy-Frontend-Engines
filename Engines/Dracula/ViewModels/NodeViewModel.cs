﻿using Associativy.Frontends.ViewModels;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Engines.Dracula.ViewModels
{
    [OrchardFeature("Associativy.Frontends.Dracula")]
    public class NodeViewModel : NodeViewModelBase
    {
        public string Label { get; set; }
    }
}