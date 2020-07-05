using MongoDB.Driver;
using ServiceLayer.Services;
using ServiceLayer.Services.JobParserServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Managers
{
    public class JobApplicationManager
    {
        private JobApplicationService jobAppService;
        public JobApplicationManager(MongoClient dbClient)
        {
            jobAppService = new JobApplicationService(dbClient);
        }
    }
}
