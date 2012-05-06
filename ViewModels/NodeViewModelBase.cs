using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System.Web.Script.Serialization;

namespace Associativy.Frontends.ViewModels
{
    [OrchardFeature("Associativy.Frontends")]
    public abstract class NodeViewModelBase
    {
        [ScriptIgnore]
        public IContent ContentItem { get; set; }
    }
}