using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSSFeedReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var companyFeeds = new Dictionary<string, List<string>>
            {
                { "ART19", new List<string> {
                    "https://rss.art19.com/apology-line",
                    "http://rss.art19.com/the-daily",
                }},
                { "Fireside", new List<string> {
                    "https://feeds.fireside.fm/bibleinayear/rss",
                }},
                { "Megaphone", new List<string> {
                    "https://feeds.megaphone.fm/ADL9840290619",
                    "https://feeds.megaphone.fm/WWO3519750118",
                }},
                { "WNYC", new List<string> {
                    "http://feeds.wnyc.org/experiment_podcast",
                }},
                { "Acast", new List<string> {
                    "https://rss.acast.com/unraveled",
                }},
                { "Audioboom", new List<string> {
                    "https://audioboom.com/channels/4997220.rss",
                }},
                { "NBC News", new List<string> {
                    "https://podcastfeeds.nbcnews.com/dateline-nbc"
                }},
                { "Libsyn.com", new List<string> {
                    "https://lincolnproject.libsyn.com/rss"
                }},
            };

            var feedReader = new RSSFeedReader();
            var inactiveCompanyFeeds = feedReader.FindInactiveFeeds(companyFeeds, 5).Result;

            Console.WriteLine("Inactive Companies");
            foreach (var company in inactiveCompanyFeeds)
            {
                Console.WriteLine($" - {company}");
            }
        }
    }
}
