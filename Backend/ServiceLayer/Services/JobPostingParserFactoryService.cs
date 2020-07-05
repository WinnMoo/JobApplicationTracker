using ServiceLayer.Services.JobParserServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Services
{
    public class JobPostingParserFactoryService
    {
        public  IJobPostingParserService getParser(string url)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                return null;
            }
            string[] urlSplit = url.Split('.');

            switch (urlSplit[1])
            {
                case "linkedin":
                    return new LinkedInParserService();
                case "indeed":
                    return new IndeedParserService();
                default:
                    return null;
            }
        }
    }
}
