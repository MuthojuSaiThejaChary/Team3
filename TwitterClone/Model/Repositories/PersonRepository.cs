using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Model.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private TwitterDatabaseContext twitterDatabaseContext = null;
        public PersonRepository()
        {
            twitterDatabaseContext = new TwitterDatabaseContext();
        }
        public void AddPerson(Person person)
        {
            twitterDatabaseContext.People.Add(person);
            twitterDatabaseContext.SaveChanges();
        }

        public Person GetPerson(string id)
        {
            return (from c in twitterDatabaseContext.People where c.UserId == id select c).SingleOrDefault();
        }
        public List<Person> GetPersons()
        {
            return twitterDatabaseContext.People.ToList();
        }

        public bool ValidatePerson(string username, string Password)
        {
            bool validUser = false;
           
          
                var query = from user in twitterDatabaseContext.People where (user.UserId == username && user.Password == Password) select user;
                if(query.Count()!=0)
                {
                    return true;
                }

           
            return validUser;
        }

        //private TwitterDatabaseContext _twitterDatabaseContext;
        //public PersonRepository(TwitterDatabaseContext context)
        //{
        //    _twitterDatabaseContext = context;
        //}
    }
}
