using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;

namespace MobileWebsite.Core.Models
{
    public class PersonPart : OrchardCore.ContentManagement.ContentPart
    {
        public string Name { get; set; }
        public Handedness Handedness { get; set; }
        public DateTime? BirthDateUtc { get; set; }
        public TextField Biography { get; set; }
    }

    public enum Handedness
    {
        Left,
        Right,
    }
}
