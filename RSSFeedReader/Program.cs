using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSSFeedReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var companyFeeds = new Dictionary<string, string>
            {
                { "Apology Line",        "https://rss.art19.com/apology-line"            },
                { "New York Times",      "http://rss.art19.com/the-daily"                },
                { "Bible",               "https://feeds.fireside.fm/bibleinayear/rss"    },
                { "Crime Junky",         "https://feeds.megaphone.fm/ADL9840290619"      },
                { "The Experiment",      "http://feeds.wnyc.org/experiment_podcast"      },
                { "Dan Bongino",         "https://feeds.megaphone.fm/WWO3519750118"      },
                { "Unrivaled",           "https://rss.acast.com/unraveled"               },
                { "Morbid",              "https://audioboom.com/channels/4997220.rss"    },
                { "NBC",                 "https://podcastfeeds.nbcnews.com/dateline-nbc" },
                { "The Lincoln Project", "https://lincolnproject.libsyn.com/rss"         },
            };

            var feedReader = new RSSFeedReader();
            var inactiveCompanyFeeds = feedReader.FindInactiveFeeds(companyFeeds, 5).Result;

            Console.WriteLine("INACTIVE FEEDS");
            foreach (var company in inactiveCompanyFeeds)
            {
                Console.WriteLine($" - {company}");
            }
        }
    }
}
