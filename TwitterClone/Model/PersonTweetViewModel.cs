using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Model
{
    public class PersonTweetViewModel
    {
        public Person Person { set; get; }
       
        public Tweet Tweet { set; get; }
        public List<Tweet> TweetsList { set; get; }

        public List<Person> Persons { set; get; }
    }
}
