using System;
using System.Linq;
using Orchard.ContentManagement;

namespace Associativy.Frontends.Models.Pages.Frontends
{
    public class AssociativyFrontendSearchFormPart : ContentPart, IGraphRetrieverAspect
    {
        public string Labels
        {
            get
            {
                if (LabelsArray == null) return "";
                return String.Join(", ", LabelsArray);
            }

            set { LabelsArray = LabelsToArray(value); }
        }

        public string[] LabelsArray { get; private set; }

        public bool IsPartialGraph { get; set; }

        public GraphRetriever GraphRetrieverField { get; set; }
        public GraphRetriever RetrieveGraph
        {
            get { return GraphRetrieverField; }
        }


        public AssociativyFrontendSearchFormPart()
        {
            LabelsArray = new string[0];
        }


        public static string[] LabelsToArray(string labels)
        {
            if (string.IsNullOrEmpty(labels)) return new string[] { };
            var array = labels.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return (from p in array where !String.IsNullOrEmpty(p.Trim()) select p.Trim()).ToArray();
        }
    }
}