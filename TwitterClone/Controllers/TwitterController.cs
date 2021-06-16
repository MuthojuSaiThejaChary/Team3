using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterClone.Model;
using TwitterClone.Model.Repositories;

namespace TwitterClone.Controllers
{
    public class TwitterController : Controller
    {
        //IPersonRepository _personRepository = null;
        //public TwitterController(IPersonRepository repository)
        //{
        //    _personRepository = repository;
        //}
        private PersonRepository personRepository = null;
        private TwitterRepository twitterRepository = null;
        private FollowingRepository followingRepository = null;
         public TwitterController()
        {
            twitterRepository = new TwitterRepository();
            personRepository = new PersonRepository();
            followingRepository = new FollowingRepository();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Person person)
        {
            personRepository.AddPerson(person);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(Person person)
        {
            var status=personRepository.ValidatePerson(person.UserId, person.Password);
            if (status)
            {
                HttpContext.Session.SetString("userid", person.UserId);

                return RedirectToAction("UserDashboard");
            }
            else
            {
                ViewBag.ErrMsg = "Invalid Credentials";
                return View();
            }
        }
        [HttpGet]
        public IActionResult UserDashboard()
        {
            string id = HttpContext.Session.GetString("userid");
            Person person = personRepository.GetPerson(id);
            Tweet tweet = new Tweet();
            List<Tweet> tweets = twitterRepository.GetTweets(id);
            List<Person> persons = personRepository.GetPersons();
            ViewBag.Tweets = tweets.Count;
            ViewBag.Following = twitterRepository.GetFollowing(id).Count;
            ViewBag.Followers = twitterRepository.GetFollowers(id).Count;

            PersonTweetViewModel personTweetViewModel = new PersonTweetViewModel()
            {
                Person = person,
                Tweet = tweet,
                TweetsList = tweets.ToList(),
                Persons=persons

            };
          
            return View(personTweetViewModel);
         
        }
        [HttpPost]
        public IActionResult UserDashboard(PersonTweetViewModel personTweetViewModel)
        {
           
            Tweet tweet = new Tweet();
            ViewBag.Msg = "No tweets";
            string id = HttpContext.Session.GetString("userid");
            tweet.UserId= HttpContext.Session.GetString("userid");
            tweet.Created = DateTime.Now;
            tweet.Message = personTweetViewModel.Tweet.Message;
            twitterRepository.AddTweet(tweet);
            
            return RedirectToAction("UserDashboard");
        }
        [HttpGet]
        public IActionResult Details(string userid)
        {
            Person person = personRepository.GetPerson(userid);
           HttpContext.Session.SetString("followingid", userid); 
            Following following = new Following();
            PersonFollowViewModel personFollowViewModel = new PersonFollowViewModel()
            {
                Person = person,
                Following = following
            };
            return View(personFollowViewModel);
        }
        [HttpPost]
        public IActionResult Details(PersonFollowViewModel personFollowViewModel)
        {
            Random r = new Random();
            Following following = new Following();
            following.Id = r.Next(10, 10000000);
            following.UserId = HttpContext.Session.GetString("userid");
            following.FollowingId = HttpContext.Session.GetString("followingid");
            followingRepository.AddFollowing(following);
            return RedirectToAction("UserDashboard");
        }

        public IActionResult Profile()
        {
            string id = HttpContext.Session.GetString("userid");
            Person person = personRepository.GetPerson(id);
            return View(person);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear(); //clears all the session data.
            return RedirectToAction("Login");
        }


    }
}
