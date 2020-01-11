using System;

namespace DocsAPI.Entities
{
    public class Document
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public User Creator { get; set; }
        public string CreatorId { get; set; }
        public string RevisionId { get; set; }
        public Revision CurrentRevision { get; set; }
        public Revision LastRevision { get; set; }
    }
}
