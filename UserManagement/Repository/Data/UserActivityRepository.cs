using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Context;
using UserManagement.Models;

namespace UserManagement.Repository.Data
{
    public class UserActivityRepository:GeneralRepository<UserActivity,MyContext>
    {
        public UserActivityRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
