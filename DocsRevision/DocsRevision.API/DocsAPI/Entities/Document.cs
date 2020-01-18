using System;
using LiteDB;

namespace DocsAPI.Entities
{
    public class Document
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get => Name + "." + Extension; }
        public string Extension { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreatorId { get; set; }
        public string RevisionId { get; set; }
        public Revision CurrentRevision { get; set; }
        public Revision LastRevision { get; set; }
    }
}
