using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace RSSFeedReader
{
    class CompanyRSSFeed
    {
        public string Company { get; set; }
        public string Url { get; set; }
    }

    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">
        /// First argument: File path to a headerless CSV (First column: Name, second column: URL)
        /// Second argument: Number of days to check the feed against
        /// </param>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please enter the correct arguments.");
                ShowUsage();
                return;
            }

            Dictionary<string, List<string>> companyFeeds;
            int days;

            if (File.Exists(args[0]))
            {
                // Valid file path provided, parse feeds and group by company into dictionary
                using (TextReader reader = new StreamReader(args[0]))
                {
                    var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                    {
                        HasHeaderRecord = false,
                    };
                    var csvReader = new CsvReader(reader, config);
                    companyFeeds = csvReader.GetRecords<CompanyRSSFeed>()
                        .GroupBy(x => x.Company)
                        .ToDictionary(k => k.Key, v => v.Select(x => x.Url).ToList());
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid file path.");
                ShowUsage();
                return;
            }

            if (int.TryParse(args[1], out var numDays))
            {
                // Valid number of days provided
                days = numDays;
            }
            else
            {
                Console.WriteLine("Please enter a valid number of days.");
                ShowUsage();
                return;
            }

            // All arguments are valid, check feeds and display
            Console.WriteLine("Checking feeds...");
            var feedReader = new RSSFeedReader();
            var inactiveFeedNames = feedReader.FindInactiveFeeds(companyFeeds, days).Result;

            if (inactiveFeedNames.Any())
            {
                Console.WriteLine($"Companies that have not updated their RSS feeds for {days} or more days:");
                foreach (var name in inactiveFeedNames)
                {
                    Console.WriteLine($" - {name}");
                }
            }
            else
            {
                Console.WriteLine($"No companies have left their RSS feeds inactive for {days} or more days.");
            }
        }

        static void ShowUsage()
        {
            Console.WriteLine("Usage: RSSFeedReader [csv-file-path] [number-of-days]");
        }
    }
}
