using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Associativy.Frontends.Models
{
    public interface IFrontendEngineContext
    {
        string Name { get; }
        string SearchFormContentType { get; }
    }
}
