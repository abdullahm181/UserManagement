using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Repository.Interface;

namespace UserManagement.Models
{
    public class MenuWithRole:IEntity
    {
        [Key]
        public int ID { get; set; }
        public virtual Menu Menu { get; set; }
        [Required]
        [ForeignKey("Menu")]
        public int Menu_Id { get; set; }
        public virtual Role Role { get; set; }
        [Required]
        [ForeignKey("Role")]
        public int Role_Id { get; set; }
        public bool? DELETE_MARK { get; set; }
        public string? CREATE_BY { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public string? UPDATE_BY { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
    }
}
