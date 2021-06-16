using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Model.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private TwitterDatabaseContext twitterDatabaseContext = null;
        public FollowingRepository()
        {
            twitterDatabaseContext = new TwitterDatabaseContext();
        }
        public void AddFollowing(Following follow)
        {
            twitterDatabaseContext.Followings.Add(follow);
            twitterDatabaseContext.SaveChanges();
        }
    }
}
