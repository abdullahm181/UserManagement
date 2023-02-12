using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Repository.Interface;

namespace UserManagement.Repository
{
    public class GeneralRepository<Entity, TContext> : IGeneralRepository<Entity>
        where Entity : class, IEntity
        where TContext : DbContext
    {
        TContext myContext;
        private readonly IHttpContextAccessor _contextAccessor;
        public GeneralRepository(TContext myContext)
        {
            this.myContext = myContext;
            this._contextAccessor = new HttpContextAccessor();
        }

        public int Delete(int id)
        {
            var data = Get(id);
            if (data == null)
                return -1;
            var datanew = Get(id);
            if (data == null)
                return -1;
            datanew.DELETE_MARK = true;
            myContext.Entry(data).CurrentValues.SetValues(datanew);

            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<Entity> Get()
        {
            var data = myContext.Set<Entity>().Where(w=>w.DELETE_MARK!=true).ToList();
            return data;
        }

        public Entity Get(int? id)
        {
            var data = myContext.Set<Entity>().Find(id);
            return data;
        }

        public int Post(Entity objectName)
        {
            myContext.Set<Entity>().Add(objectName);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Put(Entity objectName)
        {
            var data = myContext.Set<Entity>().Find(objectName.ID);
            if (data == null)
                return -1;

            myContext.Entry(data).CurrentValues.SetValues(objectName);
            //myContext.Entry(objectName).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
        public void RoleAcces(ControllerContext context, string route, string action)
        {
            var CurrentRoleID = _contextAccessor.HttpContext.Session.GetString("RoleID");
            if (String.IsNullOrWhiteSpace(CurrentRoleID)) context.HttpContext.Response.Redirect("../auth/Logout");
            var result = myContext.Set<MenuWithRole>().Where(w => w.Role_Id == Int32.Parse(CurrentRoleID)
                        && w.Menu.MENU_CONTROLLER == route && w.Menu.MENU_VIEW == action).FirstOrDefault();
            if(result==null) context.HttpContext.Response.Redirect("../home/forbidden");
            /*var result= (from e in myContext.Set<MenuWithRole>()
                         join r in myContext.Set<Menu>() on e.Menu_Id equals r.ID
                         select new { MenuWithRole = e, Menu = r }).ToList();
            result.ForEach(d => { d.MenuWithRole.Menu_Id = d.Menu.ID; });*/
        }
    }
}
