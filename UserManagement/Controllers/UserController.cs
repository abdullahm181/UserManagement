using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class UserController : BaseController<User, UserRepository>
    {
        UserRepository userRepository;
        AccountRepository accountRepository;
        UserFotoRepository userFotoRepository;
        MyContext myContext;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserController(UserRepository userRepository,AccountRepository accountRepository, MyContext myContext, UserFotoRepository userFotoRepository) : base(userRepository)
        {
            this.userRepository = userRepository;
            this.accountRepository = accountRepository;
            this.myContext = myContext;
            this.userFotoRepository = userFotoRepository;
            this._contextAccessor = new HttpContextAccessor();
        }
        public IActionResult Index()
        {
            RoleAcces(ControllerContext, ControllerContext.RouteData.Values["controller"].ToString()
                , ControllerContext.RouteData.Values["action"].ToString());
            /*throw new Exception();*/
            return View();
        }
        public IActionResult Profile()
        {
            RoleAcces(ControllerContext, ControllerContext.RouteData.Values["controller"].ToString()
                , ControllerContext.RouteData.Values["action"].ToString());
            var CurrentUserID = _contextAccessor.HttpContext.Session.GetString("ID");
            if (!String.IsNullOrWhiteSpace(CurrentUserID))
            {
                ViewBag.DataUser = userRepository.Get(Int32.Parse(CurrentUserID));
                ViewBag.DataUserFoto= myContext.UserFoto.Where(w => w.DELETE_MARK != true && w.ID_USER == Int32.Parse(CurrentUserID)).FirstOrDefault();
            }
            else {
                throw new Exception();
            }
            var view = "~/Views/User/Profile.cshtml";
            return View(view);
        }
        public IActionResult ModalCRUD(int? ID)
        {
            if (ID != null)
            {
                ViewBag.Data = userRepository.Get(ID);
            }
            var view = "~/Views/User/ModalCRUD.cshtml";
            return View(view);
        }
        public IActionResult ModalEditFullUser(int? ID)
        {
            if (ID != null)
            {
                ViewBag.Data = userRepository.Get(ID);
            }
            var view = "~/Views/User/ModalEditFullUser.cshtml";
            return View(view);
        }
        public IActionResult ModalChangePW(int? ID)
        {
            if (ID != null)
            {
                ViewBag.Data = userRepository.Get(ID);
            }
            var view = "~/Views/User/ModalChangePW.cshtml";
            return View(view);
        }
        public IActionResult ModalSetUserRole(int? ID)
        {
            ViewBag.CurrentRole = myContext.UserRole.Where(w => w.User_Id == ID).FirstOrDefault();
            ViewBag.UserID = ID;
            ViewBag.listOfItems = myContext.Role.Where(w => w.DELETE_MARK != true).ToList();
            var view = "~/Views/User/ModalSetRole.cshtml";
            return View(view);
        }

        public JsonResult ChangePassword(User user)
        {
            var result = accountRepository.ChangePassword(user);
            if (result > 0)
                return Json(new { result = 200, message = "successfully Updated" });
            else if (result == -1)
                return Json(new { result = 404, message = "Username not registered" });
            else return Json(new { result = 400, message = "Model invalid" });
        }
        public JsonResult SetUserRole(int ID,int RoleID)
        {
            var oldUserRole= myContext.UserRole.Where(w => w.User_Id == ID).FirstOrDefault();
            var newUserRole = myContext.UserRole.Where(w => w.User_Id == ID).FirstOrDefault();
            newUserRole.Role_Id = RoleID;
            myContext.Entry(oldUserRole).CurrentValues.SetValues(newUserRole);

            var result = myContext.SaveChanges();
            if (result > 0)
                return Json(new { result = result, message = "successfully Setiing Role" });
            else return Json(new { result = result, message = "Setting Role Failed" });
        }
        [HttpPost]
        public JsonResult ProcesCRUDUser(User entity)
        {

            int result = 0;
            string message = "";
            if (entity.ID == null || entity.ID == 0)
            {
                Register register = new Register() { 
                    USERNAME=entity.USERNAME,
                    EMAIL=entity.EMAIL,
                    NAMA_USER=entity.NAMA_USER,
                    PASSWORD=entity.PASSWORD
                };
                result = accountRepository.Register(register);
                message += "Register data User ";
            }
            else
            {
                var old = userRepository.Get(entity.ID);
                User entity1 = this.Merge<User>(old, entity);
                entity1.UPDATE_BY = HttpContext.Session.GetString("UserName");
                entity1.UPDATE_DATE = DateTime.Now;
                result = userRepository.Put(entity1);
                message += "Update data ";
            }
            if (result > 0) message += "Success";
            else message += "Failed";

            return Json(new { result = result, message = message });
        }

        [HttpPost]
        public JsonResult UploadFiles(IList<IFormFile> files)
        {
            int result = 0;
            foreach (IFormFile source in files)
            {
                if (source != null)
                {
                    if (source.Length > 0)
                    {

                        var CurrentUserID = _contextAccessor.HttpContext.Session.GetString("ID");
                        if (String.IsNullOrWhiteSpace(CurrentUserID)) return Json(result);
                        //prepare data
                        //Getting FileName
                        var fileName = Path.GetFileName(source.FileName);
                        //Getting file Extension
                        var fileExtension = Path.GetExtension(fileName);
                        // concatenating  FileName + FileExtension
                        var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                        var userFoto = new UserFoto()
                        {

                            Name = newFileName,
                            FileType = fileExtension,
                            ID_USER = Int32.Parse(CurrentUserID),
                            CREATE_DATE = DateTime.Now,
                            CREATE_BY = _contextAccessor.HttpContext.Session.GetString("UserName")
                        };

                        using (var target = new MemoryStream())
                        {
                            source.CopyTo(target);
                            userFoto.DataFiles = target.ToArray();
                        }
                        // getting old user foto
                        var oldUserFoto = myContext.UserFoto.Where(w => w.DELETE_MARK != true && w.ID_USER == Int32.Parse(CurrentUserID)).FirstOrDefault();
                        if (oldUserFoto != null)
                        {

                            oldUserFoto.UPDATE_BY = HttpContext.Session.GetString("UserName");
                            oldUserFoto.UPDATE_DATE = DateTime.Now;
                            oldUserFoto.Name = userFoto.Name;
                            oldUserFoto.DataFiles = userFoto.DataFiles;
                            oldUserFoto.FileType = userFoto.FileType;
                        }
                        else
                        {
                            //insert
                            myContext.UserFoto.Add(userFoto);
                        }

                        result = myContext.SaveChanges();

                    }
                }
            }
                
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserImageFile(int Id)
        {
            var user = myContext.UserFoto.Where(w=>w.DELETE_MARK!=true && w.ID_USER==Id).FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            FileResult imageUserFile = GetFileFromBytes(user.DataFiles);
            return imageUserFile;
        }
        public FileResult GetFileFromBytes(byte[] bytesIn)
        {
            return File(bytesIn, "image/png");
        }

    }
}
