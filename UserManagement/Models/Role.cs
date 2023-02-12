using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Repository.Interface;

namespace UserManagement.Models
{
    public class Role:IEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool? DELETE_MARK { get; set; }
        public string? CREATE_BY { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public string? UPDATE_BY { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
        //public object LevelId { get; internal set; }
        //public virtual List<User> Users { get; set; }
    }
}
