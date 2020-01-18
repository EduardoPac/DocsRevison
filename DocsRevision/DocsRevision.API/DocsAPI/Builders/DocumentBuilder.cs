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

        public static DocumentBuilder New()
        {
            return new DocumentBuilder();
        }

        public DocumentBuilder WithCreator(string id)
        {
            _id = id;
            return this;
        }

        public Document Build()
        {
            var document = new Document();

            var faker = new Faker();
            document.Id = Generator.GetId(8);
            document.Name = faker.Random.Word();
            document.Extension = GetRandomExtension(faker.Random.Int(0, 4));
            document.CreationDate = faker.Date.Soon(faker.Random.Int(1,15));
            document.CreatorId = _id;

            var revision = RevisionBuilder.New().WithDocument(document.Id).Build();
            document.RevisionId = revision.Id;
            document.CurrentRevision = revision;
            
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

        private string GetRandomExtension(int index)
        {
            string[] extension = { "doc", "png", "pdf", "ppt", "xls" };
            return extension[index];
        }
    }
}
