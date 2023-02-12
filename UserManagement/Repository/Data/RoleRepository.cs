using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Context;
using UserManagement.Models;

namespace UserManagement.Repository.Data
{
    public class RoleRepository:GeneralRepository<Role,MyContext>
    {
        public RoleRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
