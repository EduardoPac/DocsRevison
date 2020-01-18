using System;
using System.Collections.Generic;
using System.Linq;
using DocsAPI.Entities;
using LiteDB;

namespace DocsAPI.Services
{
    public interface IRevisionService
    {
        int Delete(string id);
        IEnumerable<Revision> FindAll();
        Revision FindOne(string id);
        int Insert(Revision Revision);
        bool Update(Revision Revision);
    }

    public class RevisionService : IRevisionService
    {
        private LiteDatabase _liteDb;

        public RevisionService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public IEnumerable<Revision> FindAll()
        {
            var result = _liteDb.GetCollection<Revision>("Revision")
                .FindAll();
            return result;
        }

        public Revision FindOne(string id)
        {
            return _liteDb.GetCollection<Revision>("Revision")
                .Find(x => x.Id == id).FirstOrDefault();
        }

        public int Insert(Revision forecast)
        {
            return _liteDb.GetCollection<Revision>("Revision")
                .Insert(forecast);
        }

        public bool Update(Revision forecast)
        {
            return _liteDb.GetCollection<Revision>("Revision")
                .Update(forecast);
        }

        public int Delete(string id)
        {
            return _liteDb.GetCollection<Revision>("Revision")
                .Delete(x => x.Id == id);
        }
    }
}
