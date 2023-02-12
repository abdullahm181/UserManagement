using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Repository.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using UserManagement.Context;

namespace UserManagement.BaseController
{
    public class BaseController<TEntity, TRepository> : Controller
           where TEntity : class, IEntity
           where TRepository : IGeneralRepository<TEntity>
    {
        TRepository repository;
        private readonly IHttpContextAccessor _contextAccessor;

        public BaseController(TRepository repository)
        {
            this._contextAccessor = new HttpContextAccessor();
            this.repository = repository;
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = repository.Get();
            return Json(result);
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = repository.Get(id);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Post(TEntity entity)
        {
            entity.CREATE_BY = HttpContext.Session.GetString("UserName");
            entity.CREATE_DATE = DateTime.Now;
            var result = repository.Post(entity);
            return Json(result);
        }

        [HttpPut]
        public JsonResult Put(TEntity entity)
        {
            var old = repository.Get(entity.ID);
            TEntity entity1 = this.Merge<TEntity>(old, entity);
            entity.UPDATE_BY= HttpContext.Session.GetString("UserName");
            entity.UPDATE_DATE = DateTime.Now;
            var result = repository.Put(entity);
            return Json(result);
        }

        [HttpDelete]
        public JsonResult DeleteEntity(int id)
        {
            var result = repository.Delete(id);
            return Json(result);
        }
        [HttpPost]
        public JsonResult ProcesCRUD(TEntity entity)
        {

            int result=0;
            string message="";
            if (entity.ID == null || entity.ID==0)
            {
                entity.CREATE_BY = HttpContext.Session.GetString("UserName");
                entity.CREATE_DATE = DateTime.Now;
                result = repository.Post(entity);
                message += "Insert data ";
            }
            else {
                var old = repository.Get(entity.ID);
                TEntity entity1 = this.Merge<TEntity>(old, entity);
                entity1.UPDATE_BY = HttpContext.Session.GetString("UserName");
                entity1.UPDATE_DATE = DateTime.Now;
                result = repository.Put(entity1);
                message += "Update data ";
            }
            if(result>0) message += "Success";
            else message += "Failed";

            return Json(new { result =result,message= message });
        }
        public T Merge<T>(T target, T source)
        {
            typeof(T)
            .GetProperties()
            .Select((PropertyInfo x) => new KeyValuePair<PropertyInfo, object>(x, x.GetValue(source, null)))
            .Where((KeyValuePair<PropertyInfo, object> x) => x.Value != null).ToList()
            .ForEach((KeyValuePair<PropertyInfo, object> x) => x.Key.SetValue(target, x.Value, null));

            //return the modified copy of Target
            return target;
        }
        public void RoleAcces(ControllerContext context, string route, string action) {
            repository.RoleAcces(context, route, action);
        }

    }
}
