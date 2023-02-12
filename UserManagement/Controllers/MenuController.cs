using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.BaseController;
using UserManagement.Models;
using UserManagement.Repository.Data;

namespace UserManagement.Controllers
{
    [Authorize]
    public class MenuController : BaseController<Menu, MenuRepository>
    {
        MenuRepository menuRepository;
        public MenuController(MenuRepository menuRepository):base(menuRepository)
        {
            this.menuRepository = menuRepository;
        }
        public IActionResult Index()
        {
            RoleAcces(ControllerContext, ControllerContext.RouteData.Values["controller"].ToString()
                , ControllerContext.RouteData.Values["action"].ToString());
            return View();
        }

        public IActionResult ModalCRUD(int? ID)
        {
            if (ID != null) {
                ViewBag.Data = menuRepository.Get(ID);
            }
            var view = "~/Views/Menu/ModalCRUD.cshtml";
            return View(view);
        }
        
    }
}
