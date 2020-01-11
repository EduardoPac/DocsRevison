using System;
using System.Collections.Generic;
using BurgerMonkeys.Tools;
using DocsAPI.Builders;
using DocsAPI.Entities;
using DocsAPI.Enum;
using Microsoft.AspNetCore.Mvc;

namespace DocsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RevisionController
    {
        // GET: api/revision?id=
        [HttpGet]
        public IEnumerable<Revision> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            var list = RevisionBuilder.New()
                                      .WithDocument(id)
                                      .WithAprover(UserBuilder.New().Build())
                                      .WithRevisor(UserBuilder.New().Build())
                                      .BuildList(5);

            return list;
        }

        //Post: /api/revision
        [HttpPost]
        public bool Post(Document document, User revisor)
        {
            if (document == null || revisor == null)
                return false;

            document.LastRevision = document.CurrentRevision;

            Revision revision = new Revision();
            revision.Id = Generator.GetId(8);
            revision.DocumentId = document.Id;
            revision.NumRevision = document.LastRevision.NumRevision + 1;
            revision.Approver = document.Creator;
            revision.ApproverId = document.CreatorId;
            revision.Revisor = revisor;
            revision.RevisorId = revisor.Id;
            revision.ExpirationDate = DateTime.Now.AddDays(60);
            revision.DeadlineDate = DateTime.Now.AddDays(5);
            revision.Status = EStatus.Pending;

            document.CurrentRevision = revision;
            document.RevisionId = revision.Id;

            //Colocar aqui para popular o banco no firebase

            return true;
        }

        // PUT: /api/revision?status=1
        [HttpPost]
        public bool Post(Document document, EStatus status)
        {
            if (document == null)
                return false;

            if(status == EStatus.Aproved)
            {
                document.CurrentRevision.Status = EStatus.Aproved;
                document.CurrentRevision.ExpirationDate = DateTime.Now.AddDays(60);
                document.CurrentRevision.RevisonDate = DateTime.Now;
            }
            else if(status == EStatus.Reproved)
            {
                document.CurrentRevision = document.LastRevision;
            }

            //Salvar no firebase

            return true;
        }
    }
}
