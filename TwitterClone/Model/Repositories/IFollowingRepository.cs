using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClone.Model.Repositories
{
    public interface IFollowingRepository
    {
        public void AddFollowing(Following follow);
    }
}
