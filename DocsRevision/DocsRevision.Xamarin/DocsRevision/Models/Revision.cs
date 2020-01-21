using System;
using DocsRevision.Enum;

namespace DocsRevision.Models
{ 
    public class Revision
    {
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public int NumRevision { get; set; }
        public string RevisorId { get; set; }
        public EStatus Status { get; set; }
        public DateTime? RevisonDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsObsolete { get; set; }
    }
}
