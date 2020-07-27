using System;
using System.Collections.Generic;
using BurgerMonkeys.Tools;
using DocsAPI.Entities;
using DocsAPI.Enum;
using DocsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DocsAPI.Controllers
{
    [Route("[controller]")]
    public class DocumentController
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IDocumentService documentService, ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }

        // GET: /api/document?idUser=
        [HttpGet]
        public IEnumerable<Document> Get(string idUser)
        {
            //Busca no banco todos os documentos com o id de criador e revisor sendo o usuario
            var list = _documentService.FindAll(idUser);

            return list;
        }

        //Remove: /api/document?id=
        [HttpDelete]
        public bool Delete(string id) //Deleta o documento
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            _documentService.Delete(id);

            return true;
        }

        //Post: /api/document?documentId=&revisorId=
        [HttpPost]
        public bool Post(string documentId, string revisorId) // Cria uma nova revisão do documento
        {
            if (string.IsNullOrWhiteSpace(documentId) || string.IsNullOrWhiteSpace(revisorId))
                return false;

            var document = _documentService.FindOne(documentId);

            document.LastRevision = document.CurrentRevision;

            Revision revision = new Revision
            {
                Id = Generator.GetId(8),
                DocumentId = document.Id,
                NumRevision = document.LastRevision.NumRevision + 1,
                RevisorId = revisorId,
                ExpirationDate = DateTime.Now.AddDays(60),
                Status = EStatus.Pending
            };

            document.CurrentRevision = revision;
            document.RevisionId = revision.Id;

            _documentService.Update(document);

            return true;
        }

        // PUT: /api/document?documentId=&status=1
        [HttpPut]
        public bool Put(string documentId, EStatus status) //Aprova ou desaprova a revisão
        {
            if (string.IsNullOrWhiteSpace(documentId))
                return false;

            var document = _documentService.FindOne(documentId);

            if (status == EStatus.Aproved)
            {
                document.CurrentRevision.Status = EStatus.Ok;
                document.CurrentRevision.ExpirationDate = DateTime.Now.AddDays(60);
                document.CurrentRevision.RevisonDate = DateTime.Now;
                document.CurrentRevision.RevisorId = "";
            }
            else if (status == EStatus.Reproved)
            {
                document.CurrentRevision = document.LastRevision;
                document.CurrentRevision.Status = EStatus.Ok;
            }

            _documentService.Update(document);

            return true;
        }


    }
}
