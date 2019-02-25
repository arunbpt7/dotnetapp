using System;
using System.Collections.Generic;

namespace PDFTestCore.Models
{
    public partial class Ll30Snippets
    {
        public int SnippetId { get; set; }
        public int FormSnippetCodeId { get; set; }
        public int LangId { get; set; }
        public string SnippetText { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
