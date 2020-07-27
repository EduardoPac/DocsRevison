using System.Collections.Generic;
using System.Threading.Tasks;
using DocsRevision.Models;
using Refit;

namespace DocsRevision.Services
{
    public interface IApiService
    {
        Task<User> GetUser(string email);
        Task<List<User>> GetUsers();

        Task<List<Document>> GetDocuments();
        Task<bool> NewRevisionSend(string documentId, string revisorId);
        Task<bool> AproveDocument(string documentId);
        Task<bool> ReproveDocument(string documentId);
        Task<bool> RemoveDocument(string id);
    }

    #region Interfaces APIs
    [Headers("Content-Type: application/json")]
    public interface IDocsFunctions
    {
        [Get("/Users?email={email}")]
        Task<List<User>> GetUser(string email);

        [Get("/Users")]
        Task<List<User>> GetUsers();

        [Get("/Document?idUser={IdUser}")]
        Task<List<Document>> GetDocuments(string IdUser);

        [Post("/Document?documentId={documentId}&revisorId={revisorId}")]
        Task<bool> SendToRevision(string documentId, string revisorId);

        [Put("/Document?documentId={documentId}&status=2")]
        Task<bool> AproveDocument(string documentId);

        [Put("/Document?documentId={documentId}&status=3")]
        Task<bool> ReproveDocument(string documentId);

        [Delete("/Document?id={documentId}")]
        Task<bool> DeleteDocument(string documentId);
    }
    #endregion

    public class ApiService : IApiService
    {
        readonly IDocsFunctions DocsFunctions;

        public ApiService() => DocsFunctions = RestService.For<IDocsFunctions>("http://api1.somee.com");

        public async Task<List<Document>> GetDocuments() => await DocsFunctions.GetDocuments(App.User.Id);

        public async Task<User> GetUser(string email)
        {
            var user = await DocsFunctions.GetUser(email);
            return user[0];
        }

        public async Task<List<User>> GetUsers() => await DocsFunctions.GetUsers();

        public async Task<bool> NewRevisionSend(string documentId, string revisorId) => await DocsFunctions.SendToRevision(documentId, revisorId);

        public async Task<bool> RemoveDocument(string documentId) => await DocsFunctions.DeleteDocument(documentId);

        public async Task<bool> AproveDocument(string documentId) => await DocsFunctions.AproveDocument(documentId);

        public async Task<bool> ReproveDocument(string documentId) => await DocsFunctions.ReproveDocument(documentId);
    }
}