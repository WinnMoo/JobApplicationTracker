using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Services
{
    public class FeedbackService
    {
        private FeedbackRepository _feedbackRepo;

        public FeedbackService(MongoClient dbClient)
        {
            _feedbackRepo = new FeedbackRepository(dbClient);
        }

        public bool InsertFeedback(Feedback feedback)
        {
            return _feedbackRepo.InsertFeedback(feedback).Result;
        }

        public bool DeleteFeedback(Feedback feedback)
        {
            return _feedbackRepo.DeleteFeedback(feedback).Result;
        }

        public bool UpdateFeedback(Feedback feedback)
        {
            return _feedbackRepo.UpdateFeedback(feedback).Result;
        }

        public List<Feedback> GetFeedback()
        {
            return _feedbackRepo.GetFeedback().Result;
        }
    }
}
