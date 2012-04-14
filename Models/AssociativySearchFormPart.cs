using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Associativy.Models;

namespace Associativy.Frontends.Models
{
    [OrchardFeature("Associativy.Frontends")]
    public class AssociativySearchFormPart : ContentPart
    {
        [Required]
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

        public AssociativySearchFormPart()
        {
            LabelsArray = new string[0];
        }
    }
}