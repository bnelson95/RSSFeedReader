# RSS Feed Reader

RSS Feed Reader is a .NET Core console application built to identify (from a given set of data) companies that have not updated their RSS feeds for a certain number of days.

## Usage

Using the the [.NET CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/) run the following commands from the root directory of the project:

```bash
dotnet build
dotnet run --project RSSFeedReader [csv-file-path] [number-of-days]
```
Where `csv-file-path` is a path to a file which contains names and RSS feed URLs in a comma-separated format (see Example section below) and `number-of-days` is a whole number used to indicate how many days 

## Example

Given the following file `feeds.txt` is in the root directory of the project :
```csv
Art19,http://rss.art19.com/the-daily
Art19,https://rss.art19.com/apology-line
Fireside,https://feeds.fireside.fm/bibleinayear/rss
Megaphone,https://feeds.megaphone.fm/ADL9840290619
Megaphone,https://feeds.megaphone.fm/WWO3519750118
WNYC,http://feeds.wnyc.org/experiment_podcast
Acast,https://rss.acast.com/unraveled
Audioboom,https://audioboom.com/channels/4997220.rss
NBC News,https://podcastfeeds.nbcnews.com/dateline-nbc
Libsyn,https://lincolnproject.libsyn.com/rss
```

Running the commands (as of March 15, 2021) will produce the following output:
```
> dotnet build
...
> dotnet run --project RSSFeedReader feeds.txt 5
Checking feeds...
Companies that have not updated their RSS feeds for 5 or more days:
 - Acast 
```

## Dependencies

 - [CodeHollow.FeedReader](https://github.com/codehollow/FeedReader)
	 - Before finding this package I developed a more manual solution of creating a client, grabbing the full XML string of the RSS feed, and deserializing it to my own models I created. After doing all that I thought surely somebody has already done this, and sure enough, I found this CodeHollow.FeedReader NuGet Package and decided to use it instead.
 - [CsvHelper](https://github.com/JoshClose/CsvHelper)
