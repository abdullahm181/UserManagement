using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Context;
using UserManagement.Models;

namespace UserManagement.Repository.Data
{
    public class UserRoleRepository:GeneralRepository<UserRole,MyContext>
    {
        public UserRoleRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
