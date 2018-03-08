using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.News.Archive.Models
{
    public class DataAccess
    {
        MongoClient _client;
        IMongoDatabase _db;

        public DataAccess(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase(databaseName); 
        }

        public List<Models.Collection> GetCollections()
        {
            return _db.GetCollection<Models.Collection>("Collections").Find(new BsonDocument()).ToList();
        }

        public List<Models.Archive> GetCollectionArchives(ObjectId id)
        {
            return _db.GetCollection<Models.Archive>("Archives").Find(c => c.Collection.Id == id).ToList(); 
        }


        public Models.Collection GetCollection(ObjectId id)
        {
            return _db.GetCollection<Models.Collection>("Collections").Find(c => c.Id == id).FirstOrDefault(); ;
        }

        public Models.Collection GetCollection(DateTime startDate, DateTime endDate, string name)
        { 
            return _db.GetCollection<Models.Collection>("Collections").Find(c => c.StartDate == startDate && c.EndDate == endDate && c.Name == name).FirstOrDefault(); 
        }

        public Models.Collection CreateCollection(Models.Collection c)
        {
            _db.GetCollection<Models.Collection>("Collections").InsertOne(c);
            return c;
        }

        public void UpdateCollection(ObjectId id, Models.Collection c)
        {
            var filter = new BsonDocument("Id", id);
            _db.GetCollection<Models.Collection>("Collections").ReplaceOne (filter, c);
        }
        public void RemoveCollection(ObjectId id)
        {
            var filter = new BsonDocument("Id", id);
            var operation = _db.GetCollection<Models.Collection>("Collections").DeleteOne(filter);
        }

        public List<Models.Archive> GetArchives()
        {
            return _db.GetCollection<Models.Archive>("Archives").Find(new BsonDocument()).ToList();
        }


        public Models.Archive GetArchive(ObjectId id)
        {
            return _db.GetCollection<Models.Archive>("Archives").Find(x => x.Id == id).FirstOrDefault(); ;
        }

        public Models.Archive CreateArchive(Models.Archive archive)
        {
            _db.GetCollection<Models.Archive>("Archives").InsertOne(archive);
            return archive;
        }

        public void UpdateArchive(ObjectId id, Models.Archive archive)
        {
            var filter = new BsonDocument("Id", id);
            _db.GetCollection<Models.Archive>("Archives").ReplaceOne(filter, archive);
        }
        public void RemoveArchive(ObjectId id)
        {
            var filter = new BsonDocument("Id", id);
            var operation = _db.GetCollection<Models.Archive>("Archives").DeleteOne(filter);
        }
    }
}
