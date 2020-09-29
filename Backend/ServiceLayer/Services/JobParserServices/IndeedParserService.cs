using DataAccessLayer.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Services.JobParserServices
{
    public class IndeedParserService : IJobPostingParserService
    {
        const string BASE_JOB_URL = "https://www.indeed.com/viewjob?jk=";
        const string BASE_JOB_URL_ALT = "https://www.indeed.com/viewjob?cmp=";
        const string BASE_JOB_SEARCH_URL = "https://www.indeed.com/jobs";
        public JobPosting ScrapeAndReturnPostingInfo(string url)
        {
            JobPosting scrapedInfo = null;
            string city = "";
            string state = "";
            string zipCode = "";
            string urlToScrapeFrom = "";
            string jobTitle = "";
            string companyName = "";
            string location = "";

            if (url.Contains(BASE_JOB_SEARCH_URL)) // Link is from search page
            {
                string jobId = "";
                string[] queries = url.Split('&');
                foreach (var query in queries)
                {
                    if (query.Contains("vjk"))
                    {
                        jobId = query.Substring(query.Length - 16);
                    }
                }
                urlToScrapeFrom = BASE_JOB_URL + jobId;
            }
            else if (url.Contains(BASE_JOB_URL))// Link is direct link to job posting with extra information in URL
            {
                urlToScrapeFrom = url.Substring(0, 50);
            }
            else if (url.Contains(BASE_JOB_URL_ALT)) // Link is direct link to job posting with extra information in URL, but contains alternative query parameters
            {
                string jobId = "";
                string[] queries = url.Split('&');
                foreach (var query in queries)
                {
                    if (query.Contains("jk"))
                    {
                        jobId = query.Substring(query.Length - 16);
                    }
                }
                urlToScrapeFrom = BASE_JOB_URL + jobId;
            }

            var web = new HtmlWeb();
            var htmlDoc = web.Load(urlToScrapeFrom);
            try
            {
                var rating = htmlDoc.DocumentNode.SelectSingleNode(
                "/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[2]/div/div/div/div[2]/div/a"); // Selects Review
                var banner = htmlDoc.DocumentNode.SelectSingleNode(
                    "/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/img[1]"); // Selects banner
                if (rating != null && banner != null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[2]/h3").InnerText;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[3]/div/div/div[1]/a").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[2]/div[3]/div/div/div[4]").InnerText;
                }
                else if (rating != null && banner == null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[1]").InnerText;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[2]/div/div/div/div[1]/a").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[2]/div/div/div/div[4]").InnerText;
                }
                else if (rating == null && banner != null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[4]/div[1]/h1").InnerText;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[4]/div[2]/div/div/div[1]/div[1]").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[4]/div[2]/div/div/div[1]/div[3]").InnerText;
                }
                else if (rating == null && banner == null)
                {
                    jobTitle = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[1]/h1").InnerHtml;
                    companyName = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[2]/div/div/div/div[1]").InnerText;
                    location = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[3]/div/div/div[1]/div[1]/div[3]/div[2]/div/div/div/div[3]").InnerHtml;
                }
                //TODO: Parse location into city, state, zipcode
                if (!location.Equals("-"))
                {
                    var splitByComma = location.Split(",");
                    city = splitByComma[0];
                    location = splitByComma[1];
                    location = location.Trim();
                    var splitLocation = location.Split(" ");
                    if (splitLocation.Length == 1)
                    {
                        state = splitLocation[0];
                        scrapedInfo = new JobPosting(jobTitle, companyName, city, state, urlToScrapeFrom);
                    }
                    else if (splitLocation.Length == 2)
                    {
                        state = splitLocation[0];
                        zipCode = splitLocation[1];
                        scrapedInfo = new JobPosting(jobTitle, companyName, city, state, zipCode, urlToScrapeFrom);
                    }
                }
                else
                {
                    scrapedInfo = new JobPosting(jobTitle, companyName, urlToScrapeFrom);
                }
                return scrapedInfo;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
