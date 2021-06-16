using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Model.Repositories
{
    public class TwitterRepository : ITwitterRepository
    {
        private TwitterDatabaseContext twitterDatabaseContext = null;
        public TwitterRepository()
        {
            twitterDatabaseContext = new TwitterDatabaseContext();
        }
        public Tweet GetTweet(string id)
        {
            return (from c in twitterDatabaseContext.Tweets where c.UserId == id select c).FirstOrDefault();
        }
        public void AddTweet(Tweet tweet)
        {
            twitterDatabaseContext.Add(tweet);
            twitterDatabaseContext.SaveChanges();
        }

        public List<Tweet> GetTweets(string userid)
        {
            List<Tweet> tweets = (from c in twitterDatabaseContext.Tweets where c.UserId == userid select c).ToList();
            return tweets;

        }

        public List<Following> GetFollowing(string userid)
        {
            List<Following> followings = (from c in twitterDatabaseContext.Followings where c.UserId == userid select c).ToList();
            return followings;
        }
        public List<Following> GetFollowers(string userid)
        {
            List<Following> followings = (from c in twitterDatabaseContext.Followings where c.FollowingId == userid select c).ToList();
            return followings;

        }
    }
}
