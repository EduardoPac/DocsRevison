using System;
using LiteDB;

namespace DocsAPI.Entities
{
    public class User
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
