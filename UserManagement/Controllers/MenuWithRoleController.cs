using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.BaseController;
using UserManagement.Context;
using UserManagement.Models;
using UserManagement.Repository.Data;
using UserManagement.ViewModels;

namespace UserManagement.Controllers
{
    [Authorize]
    public class MenuWithRoleController : BaseController<MenuWithRole, MenuWithRoleRepository>
    {
        MenuWithRoleRepository MenuWithRoleRepository;
        MyContext myContext;
        public MenuWithRoleController(MenuWithRoleRepository MenuWithRoleRepository, MyContext myContext) :base(MenuWithRoleRepository)
        {
            this.MenuWithRoleRepository = MenuWithRoleRepository;
            this.myContext = myContext;
        }

        public IActionResult Index()
        {
            RoleAcces(ControllerContext, ControllerContext.RouteData.Values["controller"].ToString()
                , ControllerContext.RouteData.Values["action"].ToString());
            return View();
        }

        public JsonResult GetMenuWithRoleID(int ID) {
            /*var result = (from e in myContext.MenuWithRole
                          join r in myContext.Menu on e.Menu_Id equals r.ID
                          select new { MenuWithRole = e, Menu = r }).ToList();
            result.ForEach(d => { d.MenuWithRole.Menu_Id = d.Menu.ID; });*/
            //var finalResult = result.Select(d => d.MenuWithRole).Where(w => w.DELETE_MARK != true);
            List<VMMenuWithRole> finalResult = new List<VMMenuWithRole>();
            var Menus = myContext.Menu.Where(w => w.DELETE_MARK != true).ToList();
            foreach (var item in Menus)
            {
                var check = myContext.MenuWithRole.Where(w => w.Role_Id == ID && w.Menu_Id == item.ID).FirstOrDefault();
                bool checkResult = check == null ? false : true;
                VMMenuWithRole tmp = new VMMenuWithRole { 
                    ID=item.ID,
                    MENU_NAME=item.MENU_NAME,
                    MENU_CONTROLLER=item.MENU_CONTROLLER,
                    MENU_VIEW=item.MENU_VIEW,
                    MENU_CHECKED= checkResult
                };
                finalResult.Add(tmp);
            }
            return Json(finalResult);
        }
        public IActionResult ModalSetMenuPermission(int ID)
        {
            ViewBag.DataRoleID = ID;
            var view = "~/Views/MenuWithRole/ModalSetMenuPermission.cshtml";
            return View(view);
        }

        public JsonResult SetMenuPermission(int RoleID,List<VMMenuWithRole> menus) {
            int result = 0;
            foreach (var item in menus)
            {
                var checkExist = myContext.MenuWithRole.Where(w => w.Role_Id == RoleID && w.Menu_Id == item.ID).FirstOrDefault();
                if (checkExist != null)
                {
                    if (!item.MENU_CHECKED)
                    {
                        myContext.MenuWithRole.Remove(checkExist);
                        
                    }
                }
                else {
                    if (item.MENU_CHECKED) {
                        MenuWithRole tmp = new MenuWithRole
                        {
                            Menu_Id = item.ID,
                            Role_Id = RoleID
                        };
                        result += MenuWithRoleRepository.Post(tmp);
                    }
                    
                }

                result+=myContext.SaveChanges();
            }
            return Json(result);
        }
    }
}
