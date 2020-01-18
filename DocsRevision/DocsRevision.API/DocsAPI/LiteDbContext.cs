using System;
using LiteDB;
using Microsoft.Extensions.Options;

namespace DocsAPI
{
    public class LiteDbContext : ILiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext(IOptions<LiteDbOptions> options)
        {
            Database = new LiteDatabase(options.Value.DatabaseLocation);
        }
    }

    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }

    public class LiteDbOptions
    {
        public string DatabaseLocation { get; set; }
    }
}
