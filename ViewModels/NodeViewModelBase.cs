using System.Web.Script.Serialization;
using Orchard.ContentManagement;

namespace Associativy.Frontends.ViewModels
{
    public abstract class NodeViewModelBase
    {
        [ScriptIgnore]
        public IContent ContentItem { get; set; }
    }
}