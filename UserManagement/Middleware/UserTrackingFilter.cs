using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Context;
using UserManagement.Models;

namespace UserManagement.Middleware
{
    public class UserTrackingFilter : IActionFilter
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly MyContext myContext;
        public UserTrackingFilter(MyContext myContext)
        {
            this.myContext = myContext;
            this._contextAccessor = new HttpContextAccessor();
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            /*var route = context.RouteData.Values["controller"].ToString();
            var action = context.RouteData.Values["action"].ToString();
            var CurrentRoleID = _contextAccessor.HttpContext.Session.GetString("RoleID");
            *//*if (String.IsNullOrWhiteSpace(CurrentRoleID)) context.HttpContext.Response.Redirect("../Auth/Index");*//*
            var result = (from e in myContext.MenuWithRole
                          join r in myContext.Menu on e.Menu_Id equals r.ID
                          select new { MenuWithRole = e, Menu = r }).ToList();
            result.ForEach(d => { d.MenuWithRole.Menu_Id = d.Menu.ID; });
            var finalResult = result.Select(d => d.MenuWithRole).Where(w => w.DELETE_MARK != true && w.Role_Id==Int32.Parse(CurrentRoleID) && w.Menu.MENU_CONTROLLER.ToUpper().Replace(" ", string.Empty) ==route.ToUpper().Replace(" ", string.Empty) && w.Menu.MENU_VIEW.ToUpper().Replace(" ", string.Empty)==action.ToUpper().Replace(" ", string.Empty));
            if (finalResult.FirstOrDefault() == null) context.HttpContext.Response.Redirect("../home/forbidden");
            *//* var result = myContext.MenuWithRole.Where(w => w.Role_Id == Int32.Parse(CurrentRoleID)
                         && w.Menu.MENU_CONTROLLER == route && w.Menu.MENU_VIEW == action).FirstOrDefault();
             */
            var route = context.RouteData.Values["controller"].ToString();
            var action = context.RouteData.Values["action"].ToString();
            var CurrentUserID = _contextAccessor.HttpContext.Session.GetString("ID");
            if (!String.IsNullOrWhiteSpace(CurrentUserID)) {
                var data = "";
                if (!string.IsNullOrEmpty(context.HttpContext.Request.QueryString.Value))
                {
                    data = context.HttpContext.Request.QueryString.Value;
                }
                else {
                    var userData = context.ActionArguments.FirstOrDefault();
                    var stringuserData = JsonConvert.SerializeObject(userData);
                    data = stringuserData;
                }
                StoreUserActivity(Int32.Parse(CurrentUserID), data, route,action);
                /*string sql = "EXEC SPCheckRoleInMenu @RoleID,@MenuRoute,@MenuAction";

                List<SqlParameter> parms = new List<SqlParameter>
                {
                    // Create parameter(s)    
                    new SqlParameter { ParameterName = "@RoleID", Value = Int32.Parse(CurrentRoleID) },
                    new SqlParameter { ParameterName = "@MenuRoute", Value = route },
                    new SqlParameter { ParameterName = "@MenuAction", Value = action }
                };

                var result = (from e in myContext.MenuWithRole
                              join r in myContext.Menu on e.Menu_Id equals r.ID
                              select new { MenuWithRole = e, Menu = r }).ToList();
                result.ForEach(d => { d.MenuWithRole.Menu_Id = d.Menu.ID; });
                var finalResult = result.Select(d => d.MenuWithRole).Where(w => w.DELETE_MARK != true && w.Role_Id == Int32.Parse(CurrentRoleID) && w.Menu.MENU_CONTROLLER.ToUpper().Replace(" ", string.Empty) == route.ToUpper().Replace(" ", string.Empty) && w.Menu.MENU_VIEW.ToUpper().Replace(" ", string.Empty) == action.ToUpper().Replace(" ", string.Empty)).FirstOrDefault();
                if (finalResult == null) context.HttpContext.Response.Redirect("../home/forbidden");*/
            }
            
        }
        private void StoreUserActivity(int ID,string param,string route,string action)
        {

            UserActivity userActivity = new UserActivity
            {
                User_ID = ID,
                Params =param,
                MENU_CONTROLLER = route,
                MENU_VIEW = action,
                CREATE_BY = String.IsNullOrWhiteSpace(_contextAccessor.HttpContext.Session.GetString("Name")) ? null : _contextAccessor.HttpContext.Session.GetString("Name"),
                CREATE_DATE = DateTime.Now,

            };
            myContext.UserActivity.Add(userActivity);
            myContext.SaveChanges();

        }
    }
}
