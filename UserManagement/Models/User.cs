using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Repository.Interface;

namespace UserManagement.Models
{
    public class User : IEntity
    {
        [Key]
        public int ID { get; set; }
        public string NAMA_USER { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string EMAIL { get; set; }
        public string? NO_HP { get; set; }
        public string? PIN { get; set; }
        public string? STATUS_USER { get; set; }
        public bool? DELETE_MARK { get; set; }
        public string? CREATE_BY { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public string? UPDATE_BY { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
    }
}
