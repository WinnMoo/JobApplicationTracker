using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Managers
{
    public class ContactManager
    {
        private readonly FeedbackService feedbackService;

        public ContactManager(MongoClient dbClient)
        {
            feedbackService = new FeedbackService(dbClient);
        }

        public ActionResult InsertFeedback()
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteFeedback()
        {
            throw new NotImplementedException();
        }

        public ActionResult UpdateFeedback()
        {
            throw new NotImplementedException();
        }

        public ActionResult GetFeedback()
        {
            throw new NotImplementedException();
        }
    }
}
