using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Context;
using UserManagement.Middleware;
using UserManagement.Models;
using UserManagement.Repository.Data;
using UserManagement.ViewModels;

namespace UserManagement.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        AccountRepository authRepository;
        private readonly MyContext myContext;
        private readonly IConfiguration iconfiguration;
        public AuthController(AccountRepository accountRepository, IConfiguration iconfiguration, MyContext myContext)
        {
            this.authRepository = accountRepository;
            this.iconfiguration = iconfiguration;
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("login")]
        [HttpPost]
        public JsonResult Login(Login login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
                return Json(new { result = 400, message = "Username or password is blank", Token = "" });
            var result = authRepository.Login(login.Username, login.Password);
            if (result == null)
                return Json(new { result = 400, message = "Username or password is incorrect", Token = "" });
            var userRole = authRepository.GetRoleById(result.ID);
            var jwt = new JwtService(iconfiguration);
            Account account = new Account
            {
                ID = result.ID,
                NAMA_USER = result.NAMA_USER,
                USERNAME = result.USERNAME,
                EMAIL = result.EMAIL,
                RoleID = userRole.Role_Id
            };
            var tokenString = jwt.GenerateToken(account);
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken DecodeToken = tokenHandler.ReadJwtToken(tokenString);
            HttpContext.Session.SetString("Token", tokenString.ToString());
            HttpContext.Session.SetString("Name", result.NAMA_USER.ToString());
            HttpContext.Session.SetString("UserName", result.USERNAME.ToString());
            HttpContext.Session.SetString("ID", result.ID.ToString());
            HttpContext.Session.SetString("RoleID", userRole.Role_Id.ToString());
            HttpContext.Session.SetString("Email", result.EMAIL.ToString());
            HttpContext.Session.SetString("SideBar", GenerateSideBar(userRole.Role_Id));
            return Json(new { result = 200, message = "successfully Login", Token = tokenString });
        }
        private string GenerateSideBar(int RoleID) {
            string sidebar = "<div class='sidebar'><div class='sidebar__inner'><ul>";
            var result = (from e in myContext.MenuWithRole
                          join r in myContext.Menu on e.Menu_Id equals r.ID 
                          select new { MenuWithRole = e, Menu = r }).ToList();
            result.ForEach(d => { d.MenuWithRole.Menu_Id = d.Menu.ID; });
            var finalResult = result.Select(d => d.MenuWithRole).Where(w => w.Role_Id== RoleID&&w.DELETE_MARK != true).OrderBy(o => o.Menu.MENU_ORDER).ToList();
            foreach (var item in finalResult)
            {
                string tmpSide = "<li>";
                tmpSide += "<a href='/" + item.Menu.MENU_CONTROLLER + "/" + item.Menu.MENU_VIEW + "'>";
                tmpSide += "<span class='icon'><i class='" + item.Menu.MENU_ICON + "'></i></span><span class='title'>" + item.Menu.MENU_NAME + "</span></a>";
                tmpSide += "</li>";
                sidebar += tmpSide;
            }
            sidebar += "</ul></div></div>";
            return sidebar ;
        }
        [Route("register")]
        [HttpPost]
        public JsonResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var result = authRepository.Register(register);
                if (result > 0)
                    return Json(new { result = 200, message = "successfully Register" });
                else if (result == -2)
                    return Json(new { result = 400, message = "UserName/Email sudah digunakan" });
            }
            return Json(new { result = 400, message = "Error" });
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [Authorize]
        [Route("ChangePassword")]
        [HttpPut]
        public JsonResult ChangePassword(User user)
        {
            if (ModelState.IsValid)
            {
                var result = authRepository.ChangePassword(user);
                if (result > 0)
                    return Json(new { result = 200, message = "successfully Updated" });
                else if (result == -1)
                    return Json(new { result = 404, message = "Username not registered" });
                /*else if (result == -2)
                    return BadRequest(new { result = 400, message = "UserName sudah digunakan" });*/
            }
            return Json(new { result = 400, message = "Model is invalid" });
        }
    }
}
