using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SavePacket.Models;
using System;
using System.Threading.Tasks;

namespace SavePacket.Service
{
    
    public class DBService : IDBService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public static IMongoCollection<Packet> collection;
        public DBService(IConfiguration configuration, ILogger<DBService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            var client = new MongoClient();
            var database = client.GetDatabase(_configuration["AppSettings:Database"]);
            collection = database.GetCollection<Packet>(_configuration["AppSettings:Collection"]);
        }

        public async Task Add(Packet packet)
        {
            try
            {
                await collection.InsertOneAsync(packet);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error in saving to Database: {0}", ex.Message);
            }
            return;
        }
    }

    public interface IDBService
    {
        Task Add(Packet packet);
    }
}
