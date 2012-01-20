﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.ViewModels;

namespace Associativy.Frontends.Engines.JIT.ViewModels
{
    public class NodeViewModel : NodeViewModelBase
    {
        // Naming is JIT naming for easy JSON serialization
        public string id { get; set; }
        public string name { get; set; }
        public List<string> adjacencies { get; set; }
        public IDictionary<string, string> data { get; set; }
    }
}