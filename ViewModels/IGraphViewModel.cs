﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;
using Orchard.ContentManagement;

namespace Associativy.Frontends.ViewModels
{
    public interface IGraphViewModel
    {
        IMutableUndirectedGraph<IContent, IUndirectedEdge<IContent>> Graph { get; set; }
    }
}
