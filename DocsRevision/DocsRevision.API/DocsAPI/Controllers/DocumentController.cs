using System;
using System.Collections.Generic;
using BurgerMonkeys.Tools;
using DocsAPI.Builders;
using DocsAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DocsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController
    {
        // GET: /api/document
        [HttpGet]
        public IEnumerable<Document> Get(string id)
        {
            //Colocar aqui a busca no banco do firebase
            var revision = RevisionBuilder.New()
                                          .WithAprover(UserBuilder.New().Build())
                                          .WithRevisor(UserBuilder.New().Build())
                                          .Build();

            var list = DocumentBuilder.New()
                                      .WithCreator(id)
                                      .WithRevision(revision)
                                      .BuildList(5);
            return list;
        }

        //Post: /api/document
        [HttpPost]
        public bool Post(Document document, User user)
        {
            if (document == null || user == null)
                return false;

            document.Id = Generator.GetId(8);
            document.CreationDate = DateTime.Now;
            document.CreatorId = user.Id;

            Revision revision = new Revision();
            revision.Id = Generator.GetId(8);
            revision.DocumentId = document.Id;
            revision.NumRevision = 1;
            revision.RevisonDate = DateTime.Now;
            revision.Status = Enum.EStatus.Ok;
            revision.Approver = user;
            revision.ApproverId = user.Id;

            document.CurrentRevision = revision;

            //Colocar aqui para popular o banco no firebase

            return true;
        }

        //Remove: /api/document
        [HttpDelete]
        public bool Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            //Colocar o metodo de remover do banco do firebase

            return true;
        }
    }
}
