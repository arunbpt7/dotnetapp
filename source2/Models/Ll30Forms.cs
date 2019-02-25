using System;
using System.Collections.Generic;

namespace PDFTestCore.Models
{
    public partial class Ll30Forms
    {
        public int FormId { get; set; }
        public string FormNumber { get; set; }
        public string Description { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
