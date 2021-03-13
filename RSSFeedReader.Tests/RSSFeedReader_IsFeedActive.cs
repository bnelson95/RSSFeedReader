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

        [Fact]
        public void IsFeedActive_Unordered_False()
        {
            var feed = new Feed
            {
                Items = new List<FeedItem>
                {
                    new FeedItem { PublishingDate = new DateTime(1, 2, 1) }, // Most recent, but not last in list
                    new FeedItem { PublishingDate = new DateTime(1, 1, 1) },
                }
            };
            var days = 50;
            var refDate = new DateTime(year: 1, month: 4, day: 1); // two months since last item

            var rssReader = new RSSFeedReader();
            var isActive = rssReader.IsFeedActive(feed, days, refDate);

            Assert.False(isActive);
        }

        [Fact]
        public void IsFeedActive_Unordered_True()
        {
            var feed = new Feed
            {
                Items = new List<FeedItem>
                {
                    new FeedItem { PublishingDate = new DateTime(1, 1, 1) },
                    new FeedItem { PublishingDate = new DateTime(1, 3, 1) }, // Most recent, but not last in list
                    new FeedItem { PublishingDate = new DateTime(1, 2, 1) },
                }
            };
            var days = 50;
            var refDate = new DateTime(year: 1, month: 4, day: 1); // one month since last item

            var rssReader = new RSSFeedReader();
            var isActive = rssReader.IsFeedActive(feed, days, refDate);

            Assert.True(isActive);
        }

        /// <summary>
        /// A feed with no items should not be considered active regardless of number of days given
        /// </summary>
        [Fact]
        public void IsFeedActive_FeedEmpty_False()
        {
            var feed = new Feed
            {
                Items = new List<FeedItem>(),
            };
            var days = 0;
            var refDate = new DateTime(year: 1, month: 1, day: 1);

            var rssReader = new RSSFeedReader();
            var isActive = rssReader.IsFeedActive(feed, days, refDate);

            Assert.False(isActive);
        }

        /// <summary>
        /// A null feed should not be considered active regardless of number of days given
        /// </summary>
        [Fact]
        public void IsFeedActive_FeedNull_False()
        {
            Feed feed = null;
            var days = 0;
            var refDate = new DateTime(year: 1, month: 1, day: 1);

            var rssReader = new RSSFeedReader();
            var isActive = rssReader.IsFeedActive(feed, days, refDate);

            Assert.False(isActive);
        }

        [Fact]
        public void IsFeedActive_Days0_True()
        {
            var feed = new Feed
            {
                Items = new List<FeedItem>
                {
                    new FeedItem { PublishingDate = new DateTime(1, 1, 1) },
                    new FeedItem { PublishingDate = new DateTime(1, 2, 1) },
                }
            };
            var days = 0;
            var refDate = new DateTime(year: 1, month: 1, day: 1);

            var rssReader = new RSSFeedReader();
            var isActive = rssReader.IsFeedActive(feed, days, refDate);

            Assert.True(isActive);
        }

        [Fact]
        public void IsFeedActive_RefDateBeforeLastItem_True()
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
            var days = 10;
            var refDate = new DateTime(year: 1, month: 2, day: 15);

            var rssReader = new RSSFeedReader();
            var isActive = rssReader.IsFeedActive(feed, days, refDate);

            Assert.True(isActive);
        }
    }
}
