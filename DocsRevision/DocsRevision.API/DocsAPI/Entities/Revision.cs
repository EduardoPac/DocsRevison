using System;
using DocsAPI.Enum;

namespace DocsAPI.Entities
{
    public class Revision
    {
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public string NumRevision { get; set; }
        public string RevisorId { get; set; }
        public User Revisor { get; set; }
        public string ApproverId { get; set; }
        public User Approver { get; set; }
        public EStatus Status { get; set; }
        public DateTimeOffset? RevisonDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public DateTimeOffset? DeadlineDate { get; set; }
        public bool IsObsolete { get => ExpirationDate > DateTimeOffset.Now; }
    }
}
