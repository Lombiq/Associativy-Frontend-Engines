using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Mvc.Routes;

namespace Associativy.Frontends.EngineDiscovery
{
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

        public void DescribeEngine(string name, LocalizedString displayName, RouteDescriptor route)
        {
            if (String.IsNullOrEmpty(name) || displayName == null)
            {
                throw new ArgumentException("Associativy frontend engines should have their Name and DisplayName set properly.");
            }

            if (route == null)
            {
                throw new ArgumentException("Associativy frontend engines should have their route set properly");
            }

            _descriptors.Add(new EngineDescriptor(name, displayName, route));
        }
    }
}