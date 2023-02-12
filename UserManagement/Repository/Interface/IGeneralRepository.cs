using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace UserManagement.Repository.Interface
{
    public interface IGeneralRepository<Entity>
         where Entity : class, IEntity
    {
        public IEnumerable<Entity> Get();
        public Entity Get(int? id);
        public int Post(Entity entity);
        public int Put(Entity entity);
        public int Delete(int id);
        public void RoleAcces(ControllerContext context, string route, string action);
    }
}
