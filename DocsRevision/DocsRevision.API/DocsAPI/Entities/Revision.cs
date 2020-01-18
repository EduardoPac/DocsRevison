using System;
using DocsAPI.Enum;
using LiteDB;

namespace DocsAPI.Entities
{
    public class Revision
    {
        [BsonId]
        public string Id { get; set; }
        public string DocumentId { get; set; }
        public int NumRevision { get; set; }
        public string RevisorId { get; set; }
        public EStatus Status { get; set; }
        public DateTime? RevisonDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsObsolete { get => RevisonDate > DateTimeOffset.Now; }
    }
}
