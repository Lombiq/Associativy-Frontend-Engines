﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.Frontends.Engines;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.EventHandlers
{
    public class FrontendContext
    {
        public IEngineContext EngineContext { get; set; }
        public IGraphContext GraphContext { get; set; }
    }
}