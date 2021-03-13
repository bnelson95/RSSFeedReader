using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using Xunit;

namespace RSSFeedReader.Tests
{
    public class RSSFeedReader_IsFeedActive
    {
        [Fact]
        public void IsFeedActive_False()
        {
            var feed = new Feed
            {
                Items = new List<FeedItem>
                {
                    new FeedItem { PublishingDate = new DateTime(1, 1, 1) },
                    new FeedItem { PublishingDate = new DateTime(1, 2, 1) },
                }
            };
            var days = 50;
            var refDate = new DateTime(year: 1, month: 4, day: 1);

            var rssReader = new RSSFeedReader();
            var isActive = rssReader.IsFeedActive(feed, days, refDate);

            Assert.False(isActive);
        }

        [Fact]
        public void IsFeedActive_True()
        {
            var feed = new Feed
            {
                Items = new List<FeedItem>
                {
                    new FeedItem { PublishingDate = new DateTime(1, 1, 1) },
                    new FeedItem { PublishingDate = new DateTime(1, 2, 1) },
                    new FeedItem { PublishingDate = new DateTime(1, 3, 1) },
                }
            };
            var days = 50;
            var refDate = new DateTime(year: 1, month: 4, day: 1);

            var rssReader = new RSSFeedReader();
            var isActive = rssReader.IsFeedActive(feed, days, refDate);

            Assert.True(isActive);
        }
    }
}
