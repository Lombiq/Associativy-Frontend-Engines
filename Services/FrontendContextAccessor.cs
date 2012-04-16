﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.GraphDiscovery;
using Orchard.Mvc;

namespace Associativy.Frontends.Services
{
    public class FrontendContextAccessor : IFrontendContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FrontendContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IGraphContext GetCurrentGraphContext()
        {
            var request = _httpContextAccessor.Current().Request;
            var dataTokens = request.RequestContext.RouteData.DataTokens;
            if (dataTokens != null && dataTokens.ContainsKey("GraphContext"))
            {
                return (IGraphContext)dataTokens["GraphContext"];
            }
            else if (!String.IsNullOrEmpty((string)request.RequestContext.RouteData.Values["GraphName"]))
            {
                return new GraphContext { GraphName = (string)request.RequestContext.RouteData.Values["GraphName"] };
            }

            return null;
        }
    }
}