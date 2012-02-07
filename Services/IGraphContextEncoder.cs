using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard;
using Associativy.GraphDiscovery;

namespace Associativy.Frontends.Services
{
    public interface IGraphContextEncoder : IDependency
    {
        string EncodeGraphContext(IGraphContext graphContext);
        IGraphContext DecodeGraphContext(string encodedValue);
    }
}
