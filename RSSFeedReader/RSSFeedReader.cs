using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSSFeedReader
{
    public class RSSFeedReader
    {
        /// <summary>
        /// Given a dictionary of companies and their RSS feed urls and a number of days this method 
        /// will return a list of the companies that have had no activity for that length of time.
        /// </summary>
        /// <param name="companyFeeds">Dictionary keyed by Company and valued by RSS feed urls</param>
        /// <param name="days">Number of days to be defined as "inactive"</param>
        /// <returns>
        /// A list of the companies that have had no activity on any of their feeds for the given number of days
        /// </returns>
        public async Task<List<string>> FindInactiveFeeds(Dictionary<string, List<string>> companyFeeds, int days)
        {
            var inactiveCompanies = new List<string>();
            foreach (var company in companyFeeds)
            {
                var isActive = false;
                foreach (var url in company.Value)
                {
                    var feed = await FeedReader.ReadAsync(url);
                    if (isActive = IsFeedActive(feed, days, DateTime.Now))
                    {
                        break;
                    }
                }

                if (!isActive)
                {
                    inactiveCompanies.Add(company.Key);
                }
            }

            return inactiveCompanies;
        }

        // Helper Methods -------------------------------------------------------------------------

        /// <summary>
        /// Given a CodeHollow.FeedReader.Feed object
        /// </summary>
        /// <param name="feed">The deserialized RSS feed</param>
        /// <param name="days">The number of days prior to the reference date to consider feeds "inactive"</param>
        /// <param name="refDate">
        /// The reference date to be used. 
        /// This will practically always be the current date/time, but for testing purposes 
        /// it needs to be passed in as a parameter to ensure consistent results.
        /// </param>
        /// <returns>
        /// True if the feed is still active.
        /// (last published date falls after the number of days subtracted from the reference date)
        /// </returns>
        public bool IsFeedActive(Feed feed, int days, DateTime refDate)
        {
            if (feed == null || feed.Items == null || !feed.Items.Any()) return false;

            var last = feed.Items.Max(x => x.PublishingDate);

            try
            {
                refDate = refDate.AddDays(-days);
            }
            catch
            {
                refDate = DateTime.MinValue;
            }

            return last > refDate;
        }
    }
}