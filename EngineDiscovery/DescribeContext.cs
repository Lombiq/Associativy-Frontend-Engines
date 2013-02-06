﻿using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc.Routes;

namespace Associativy.Frontends.EngineDiscovery
{
    public delegate LocalizedString DisplayNameGetter();

    [OrchardFeature("Associativy.Frontends")]
    public class DescribeContext
    {
        private readonly List<EngineDescriptor> _descriptors;

        public IEnumerable<EngineDescriptor> Descriptors
        {
            get
            {
                return _descriptors.AsEnumerable();
            }
        }

        public DescribeContext()
        {
            _descriptors = new List<EngineDescriptor>();
        }

        // We need a getter delegate so the localized name is only fetched when needed. This makes it possible to fetch the RouteDescriptors
        // from an IRouteProvider: otherwise, since there's no work context when building routes localizing will fail.
        // See: http://orchard.codeplex.com/workitem/19430
        public void DescribeEngine(string name, DisplayNameGetter displayNameGetter, RouteDescriptor route)
        {
            if (String.IsNullOrEmpty(name) || displayNameGetter == null)
            {
                throw new ArgumentException("Associativy frontend engines should have their Name and DisplayName set properly.");
            }

            if (route == null)
            {
                throw new ArgumentException("Associativy frontend engines should have their route set properly");
            }

            _descriptors.Add(new EngineDescriptor(name, displayNameGetter, route));
        }
    }
}