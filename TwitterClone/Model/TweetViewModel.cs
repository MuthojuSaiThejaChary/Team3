using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Model
{
    public class TweetViewModel
    {
        public Tweet tweets { set; get; }
        public List<Tweet> tweetsList { set; get; }
    }
}
