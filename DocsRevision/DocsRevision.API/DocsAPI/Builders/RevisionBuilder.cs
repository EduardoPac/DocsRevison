using System;
using System.Collections.Generic;
using Bogus;
using BurgerMonkeys.Tools;
using DocsAPI.Entities;

namespace DocsAPI.Builders
{
    public class RevisionBuilder
    {
        string _id;
        User _aprover;
        User _revisor;

        public static RevisionBuilder New()
        {
            return new RevisionBuilder();
        }

        public RevisionBuilder WithDocument(string id)
        {
            _id = id;
            return this;
        }

        public RevisionBuilder WithAprover(User aprover)
        {
            _aprover = aprover;
            return this;
        }

        public RevisionBuilder WithRevisor(User revisor)
        {
            _revisor = revisor;
            return this;
        }

        public Revision Build()
        {
            var revision = new Revision();

            var faker = new Faker();
            revision.Id = Generator.GetId(8);
            revision.DocumentId = _id;
            revision.NumRevision = faker.Random.Int(1, 10);
            revision.RevisorId = _revisor.Id;
            revision.Revisor = _revisor;
            revision.ApproverId = _aprover.Id;
            revision.Approver = _aprover;
            revision.Status = Enum.EStatus.Ok;
            revision.RevisonDate = faker.Date.Soon();
            revision.ExpirationDate = faker.Date.Soon();
            revision.DeadlineDate = faker.Date.Soon();

            return revision;
        }

        public List<Revision> BuildList(int num)
        {
            List<Revision> Revisions = new List<Revision>();

            for (int i = 0; i < num; i++)
            {
                Revisions.Add(Build());
            }

            return Revisions;
        }
    }
}
