using System;
using DocsAPI.Enum;

namespace DocsAPI.Entities
{
    public class Revision
    {
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public int NumRevision { get; set; }
        public string RevisorId { get; set; }
        public User Revisor { get; set; }
        public string ApproverId { get; set; }
        public User Approver { get; set; }
        public EStatus Status { get; set; }
        public DateTime? RevisonDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public bool IsObsolete { get => ð > DateTimeOffset.Now; }
    }
}
