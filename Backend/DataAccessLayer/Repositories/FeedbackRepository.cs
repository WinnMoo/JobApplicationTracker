using DataAccessLayer.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class FeedbackRepository
    {
        private readonly MongoClient dbClient;
        private readonly IMongoCollection<Feedback> _feedback;
        public FeedbackRepository(MongoClient dbClient)
        {
            this.dbClient = dbClient;
            var database = this.dbClient.GetDatabase("Database");
            _feedback = database.GetCollection<Feedback>("Feedback");
        }

        public async Task<bool> InsertFeedback(Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFeedback(Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateFeedback(Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Feedback>> GetFeedback()
        {
            throw new NotImplementedException();
        }
    }
}
