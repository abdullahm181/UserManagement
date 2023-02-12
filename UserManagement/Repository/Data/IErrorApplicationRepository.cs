using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Context;
using UserManagement.Models;

namespace UserManagement.Repository.Data
{
    public class IErrorApplicationRepository:GeneralRepository<IErrorApplication,MyContext>
    {
        public IErrorApplicationRepository(MyContext myContext) : base(myContext)
        {

        }
    }

}
