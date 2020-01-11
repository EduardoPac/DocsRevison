using System;
using System.Collections.Generic;
using Bogus;
using BurgerMonkeys.Tools;
using DocsAPI.Entities;

namespace DocsAPI.Builders
{
    public class DocumentBuilder
    {
        string _id;
        Revision _revision;

        public static DocumentBuilder New()
        {
            return new DocumentBuilder();
        }

        public DocumentBuilder WithCreator(string id)
        {
            _id = id;
            return this;
        }

        public DocumentBuilder WithRevision(Revision revision)
        {
            _revision = revision;
            return this;
        }

        public Document Build()
        {
            var document = new Document();

            var faker = new Faker();
            document.Id = Generator.GetId(8);
            document.Name = faker.Person.FullName;
            document.CreationDate = faker.Date.Soon();
            document.CreatorId = _id;
            document.Creator = UserBuilder.New().Build();
            document.RevisionId = _revision.Id;
            document.CurrentRevision = _revision;
            
            return document;
        }

        public List<Document> BuildList(int num)
        {
            List<Document> Documents = new List<Document>();

            for (int i = 0; i < num; i++)
            {
                Documents.Add(Build());
            }

            return Documents;
        }
    }
}
