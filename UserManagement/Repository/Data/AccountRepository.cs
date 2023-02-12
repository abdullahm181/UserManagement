
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Context;
using UserManagement.Middleware;
using UserManagement.Models;
using UserManagement.ViewModels;

namespace UserManagement.Repository.Data
{
    public class AccountRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;
        MyContext myContext;
        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
            this._contextAccessor = new HttpContextAccessor();
        }
        public User Login(string username, string password)
        {
            var user = myContext.User.SingleOrDefault(x => x.USERNAME == username);

            // check if username exists
            if (user == null)
                return null;

            //check if password is correct
            if (!Cryptograph.ValidatePassword(password, user.PASSWORD))
                return null;

            // authentication successful
            return user;
        }
        public int Register(Register register)
        {

            //preparation default role
            var DefaultRole = myContext.Role.SingleOrDefault(x => x.ID.Equals(2));
            //check if username or email available
            if (myContext.User.Any(x => x.USERNAME.ToUpper().Trim() == register.USERNAME.ToUpper().Trim() || x.EMAIL.ToUpper().Trim() == register.EMAIL.ToUpper().Trim()) )
                return -2;
            //create model employe 
            //Create Bcrypt hasing ppaswrod
            var passwordHash = Cryptograph.HashPassword(register.PASSWORD);
            User user = new User()
            {
                NAMA_USER = register.NAMA_USER,
                USERNAME = register.USERNAME,
                PASSWORD = passwordHash,
                EMAIL = register.EMAIL,
            };
            user.CREATE_BY = String.IsNullOrWhiteSpace(_contextAccessor.HttpContext.Session.GetString("Name")) ? null : _contextAccessor.HttpContext.Session.GetString("Name");
            user.CREATE_DATE = DateTime.Now;

            //add employee  
            myContext.User.Add(user);
            var RegisterUserResult = myContext.SaveChanges();

            //check if adding employee succes
            if (RegisterUserResult > 0)
            {
                UserRole userRole = new UserRole()
                {
                    Role_Id = DefaultRole.ID,
                    User_Id = user.ID
                };
                myContext.UserRole.Add(userRole);
                var RegisterUserRoleResult = myContext.SaveChanges();
                if (RegisterUserRoleResult > 0)
                    return RegisterUserRoleResult;
                else {
                    //Delete User 
                    var DataUser = myContext.User.Find(user.ID);
                    myContext.User.Remove(DataUser);
                    myContext.SaveChanges();
                }
            }
            return 0;
        }
        public int ChangePassword(User userParam)
        {
            var user = myContext.User.SingleOrDefault(x => x.USERNAME == userParam.USERNAME);
            if (user == null)
                return -1;
            if (!string.IsNullOrWhiteSpace(userParam.PASSWORD) && !Cryptograph.ValidatePassword(userParam.PASSWORD, user.PASSWORD))
            {
                //Create Bcrypt hasing ppaswrod
                var passwordHash = Cryptograph.HashPassword(userParam.PASSWORD);
                user.PASSWORD = passwordHash;
            }
            var result = myContext.SaveChanges();
            return result;
        }
        /*
        public int ForgotPassword(string Email)
        {
            var data = myContext.Employees.SingleOrDefault(x => x.Email.Equals(Email));
            if (data == null)
            {
                return -1;
            }
            if (!string.IsNullOrWhiteSpace(Password))
            {
                user.Password = userParam.Password;
            }
            var result = myContext.SaveChanges();
            return 1;

        }*/
        public UserRole GetRoleById(int UserId)
        {
            var user = myContext.UserRole.FirstOrDefault(x => x.User_Id == UserId);

            return user;

        }
    }
}
