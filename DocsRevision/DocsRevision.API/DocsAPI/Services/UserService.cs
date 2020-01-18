using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocsAPI.Entities;
using LiteDB;

namespace DocsAPI.Services
{
    public interface IUserService
    {
        int Delete(string id);
        IEnumerable<User> FindAll();
        User FindOne(string email);
        Task Insert(User User);
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

        public async Task Insert(User forecast)
        {
            await Task.Run(() => _liteDb.GetCollection<User>("User")
                .Upsert(forecast));
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
