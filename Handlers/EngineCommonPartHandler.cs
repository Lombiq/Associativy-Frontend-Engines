using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement.Handlers;
using Piedone.HelpfulLibraries.Serialization;
using Associativy.Frontends.Models;

namespace Associativy.Frontends.Handlers
{
    [OrchardFeature("Associativy.Frontends")]
    public class EngineCommonPartHandler : ContentHandler
    {
        public EngineCommonPartHandler(ISimpleSerializer simpleSerializer)
        {
            OnActivated<EngineCommonPart>((context, part) =>
            {
                part.GraphContextBase64Field.Loader(() => simpleSerializer.Base64Serialize(part.GraphContext));
            });
        }
    }
}