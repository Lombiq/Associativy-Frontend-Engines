using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Associativy.Models;

namespace Associativy.FrontendEngines.Models
{
    [OrchardFeature("Associativy.FrontendEngines")]
    public class SearchFormPart : ContentPart
    {
        public IGraphDescriptor GraphDescriptor { get; set; }

        [Required]
        public string Terms
        {
            get
            {
                if (TermsArray == null) return "";
                return String.Join(", ", TermsArray);
            }

            set
            {
                if (value != null)
                {
                    TermsArray = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    TermsArray = (from p in TermsArray where p.Trim() != "" select p.Trim()).ToArray();
                }
            }
        }

        public string[] TermsArray { get; private set; }

        public SearchFormPart()
        {
            TermsArray = new string[0];
        }
    }
}