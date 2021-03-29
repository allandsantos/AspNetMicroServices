using Catalog.API.Entities;
using Catalog.API.Settings;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration configuration)
        {
            var mongoDbSettings = configuration.GetSection("DatabaseSettings").Get<MongoDbSettings>();

            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.DatabaseName);

            Products = database.GetCollection<Product>(mongoDbSettings.CollectionName);
            CatalogContextSeed.SeedData(Products);
        }
    }
}
