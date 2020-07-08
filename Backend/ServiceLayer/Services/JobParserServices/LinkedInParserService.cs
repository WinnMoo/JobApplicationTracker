using DataAccessLayer.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Services.JobParserServices
{
    public class LinkedInParserService : IJobPostingParserService
    {
        const string BASE_JOB_URL = "https://www.linkedin.com/jobs/view/";
        const string BASE_JOB_SEARCH_URL = "https://www.linkedin.com/jobs/search/";
        public JobPosting ScrapeAndReturnPostingInfo(string url)
        {
            JobPosting scrapedInfo = null;
            try
            {
                string urlToScrapeFrom = "";
                if (url.Contains(BASE_JOB_SEARCH_URL)) // Link is from search page
                {
                    string[] infos = url.Split('=');
                    string jobId = infos[1].Substring(0, infos[1].IndexOf('&'));
                    urlToScrapeFrom = BASE_JOB_URL + jobId;
                }
                else if (url.Contains(BASE_JOB_URL) && url.Length != 46) // Link is direct link to job posting, details in URL
                {
                    urlToScrapeFrom = url.Substring(0, 45);
                }
                else // Link is direct link to job posting, scrubbed details from URL
                {
                    urlToScrapeFrom = url;
                }
                var web = new HtmlWeb();
                var htmlDoc = web.Load(urlToScrapeFrom);

            
                string jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section[1]/section[2]/div[1]/div[1]/h1").InnerText;
                string companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section[1]/section[2]/div[1]/div[1]/h3[1]/span[1]/a").InnerHtml;
                string location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/main/section[1]/section[2]/div[1]/div[1]/h3[1]/span[2]").InnerHtml;

                string locationToParse = location;
                locationToParse = locationToParse.Replace(",", "");
                string[] parsed = locationToParse.Split(" ");
                string city = parsed[0];
                string state = parsed[1];
                scrapedInfo = new JobPosting(jobTitle, companyName, city, state, urlToScrapeFrom);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                return scrapedInfo;
            }
            return scrapedInfo;
        }
    }
}
