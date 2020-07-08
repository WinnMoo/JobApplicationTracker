using MongoDB.Driver;
using ServiceLayer.Services;

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
