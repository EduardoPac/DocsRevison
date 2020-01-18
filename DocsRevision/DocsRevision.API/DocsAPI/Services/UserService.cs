using System;
using System.Collections.Generic;
using System.Linq;
using DocsAPI.Entities;
using LiteDB;

namespace DocsAPI.Services
{
    public interface IUserService
    {
        int Delete(string id);
        IEnumerable<User> FindAll();
        User FindOne(string email);
        int Insert(User User);
        bool Update(User User);
    }

    public class UserService : IUserService
    {
        private LiteDatabase _liteDb;

        public UserService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public IEnumerable<User> FindAll()
        {
            var result = _liteDb.GetCollection<User>("User")
                .FindAll();
            return result;
        }

        public User FindOne(string email)
        {
            return _liteDb.GetCollection<User>("User")
                .Find(x => x.Email.Contains(email)).FirstOrDefault();
        }

        public int Insert(User forecast)
        {
            return _liteDb.GetCollection<User>("User")
                .Insert(forecast);
        }

        public bool Update(User forecast)
        {
            return _liteDb.GetCollection<User>("User")
                .Update(forecast);
        }

        public int Delete(string id)
        {
            return _liteDb.GetCollection<User>("User")
                .Delete(x => x.Id == id);
        }
    }
}
