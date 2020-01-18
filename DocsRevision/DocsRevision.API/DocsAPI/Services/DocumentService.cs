using System;
using System.Collections.Generic;
using System.Linq;
using DocsAPI.Entities;
using LiteDB;

namespace DocsAPI.Services
{
    public interface IDocumentService
    {
        int Delete(string id);
        IEnumerable<Document> FindAll();
        Document FindOne(string id);
        int Insert(Document document);
        bool Update(Document document);
    }

    public class DocumentService : IDocumentService
    {
        private LiteDatabase _liteDb;

        public DocumentService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public IEnumerable<Document> FindAll()
        {
            var result = _liteDb.GetCollection<Document>("Document")
                .FindAll();
            return result;
        }

        public Document FindOne(string id)
        {
            return _liteDb.GetCollection<Document>("Document")
                .Find(x => x.Id == id).FirstOrDefault();
        }

        public int Insert(Document document)
        {
            return _liteDb.GetCollection<Document>("Document")
                .Insert(document);
        }

        public bool Update(Document document)
        {
            return _liteDb.GetCollection<Document>("Document")
                .Update(document);
        }

        public int Delete(string id)
        {
            return _liteDb.GetCollection<Document>("Document")
                .Delete(x => x.Id == id);
        }
    }
}
