using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Associativy.GraphDiscovery;
using Piedone.HelpfulLibraries.Serialization;

namespace Associativy.Frontends.Services
{
    public class GraphContextEncoder : IGraphContextEncoder
    {
        private readonly ISimpleSerializer _simpleSerializer;

        public GraphContextEncoder(ISimpleSerializer simpleSerializer)
        {
            _simpleSerializer = simpleSerializer;
        }

        public string EncodeGraphContext(IGraphContext graphContext)
        {
            return _simpleSerializer.Base64Serialize(graphContext);
        }

        public IGraphContext DecodeGraphContext(string encodedValue)
        {
            return _simpleSerializer.Base64Deserialize<IGraphContext>(encodedValue);
        }
    }
}