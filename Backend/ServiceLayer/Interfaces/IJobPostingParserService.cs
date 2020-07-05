using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Services
{
    public interface IJobPostingParserService
    {
        JobPosting ScrapeAndReturnPostingInfo(string url);
    }
}
