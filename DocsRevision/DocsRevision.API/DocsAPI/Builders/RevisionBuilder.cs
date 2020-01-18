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

        public Revision Build()
        {
            var revision = new Revision();

            var faker = new Faker();
            revision.Id = Generator.GetId(8);
            revision.DocumentId = _id;
            revision.NumRevision = 1;
            revision.Status = Enum.EStatus.Ok;
            revision.ExpirationDate = DateTime.Now.AddDays(60);

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
