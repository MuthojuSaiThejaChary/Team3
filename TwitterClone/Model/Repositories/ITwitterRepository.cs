using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Model.Repositories
{
   public  interface ITwitterRepository
    {
        public Tweet GetTweet(string id);
        public void AddTweet(Tweet tweet);
        public List<Tweet> GetTweets(string userid);
        public List<Following> GetFollowing(string userid);
    }
}
