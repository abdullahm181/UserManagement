using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Repository.Interface;

namespace UserManagement.Models
{
    public class UserFoto:IEntity
    {
        [Key]
        public int ID { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("User")]
        public int ID_USER { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        [MaxLength]
        public byte[] DataFiles { get; set; }

        public bool? DELETE_MARK { get; set; }
        public string? CREATE_BY { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public string? UPDATE_BY { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
    }
}
