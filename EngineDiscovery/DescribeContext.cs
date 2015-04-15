using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.Localization;
using Orchard.Mvc.Routes;

namespace Associativy.Frontends.EngineDiscovery
{
    public delegate LocalizedString DisplayNameGetter();

    public class DescribeContext
    {
        private readonly List<IEngineDescriptor> _descriptors;

        public IEnumerable<IEngineDescriptor> Descriptors
        {
            get
            {
                return _descriptors.AsEnumerable();
            }
        }


        public DescribeContext()
        {
            _descriptors = new List<IEngineDescriptor>();
        }


        // We need a getter delegate so the localized name is only fetched when there is already a work context, so the current culture
        // can be determined.
        public virtual void DescribeEngine(string name, DisplayNameGetter displayNameGetter, RouteDescriptor route)
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