using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.ViewModels
{
    public class Account
    {
        public int ID { get; set; }
        public string NAMA_USER { get; set; }
        public string USERNAME { get; set; }
        public string EMAIL { get; set; }
        public int RoleID { get; set; }
    }
}
