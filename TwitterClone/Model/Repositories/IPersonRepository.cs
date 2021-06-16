using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Model.Repositories
{
    public interface IPersonRepository
    {
        void AddPerson(Person person);

        bool ValidatePerson(string username, string Password);

        

        public Person GetPerson(string id);
    }
}
