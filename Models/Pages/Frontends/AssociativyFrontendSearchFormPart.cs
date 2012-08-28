using System;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Associativy.Frontends.Models.Pages.Frontends
{
    [OrchardFeature("Associativy.Frontends")]
    public class AssociativyFrontendSearchFormPart : ContentPart, IGraphRetrieverAspect
    {
        public string Labels
        {
            get
            {
                if (LabelsArray == null) return "";
                return String.Join(", ", LabelsArray);
            }

            set
            {
                if (value != null)
                {
                    LabelsArray = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    LabelsArray = (from p in LabelsArray where !String.IsNullOrEmpty(p.Trim()) select p.Trim()).ToArray();
                }
            }
        }

        public string[] LabelsArray { get; private set; }

        public bool IsPartialGraph { get; set; }

        public AssociativyFrontendSearchFormPart()
        {
            LabelsArray = new string[0];
        }

        public GraphRetriever GraphRetrieverField { get; set; }
        public GraphRetriever RetrieveGraph
        {
            get { return GraphRetrieverField; }
        }
    }
}