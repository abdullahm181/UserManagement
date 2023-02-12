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
    public class RoleController : BaseController<Role, RoleRepository>
    {
        RoleRepository roleRepository;
        public RoleController(RoleRepository roleRepository):base(roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ModalCRUD(int? ID)
        {
            if (ID != null)
            {
                ViewBag.Data = roleRepository.Get(ID);
            }
            var view = "~/Views/MenuWithRole/ModalCRUD.cshtml";
            return View(view);
        }
    }
}
