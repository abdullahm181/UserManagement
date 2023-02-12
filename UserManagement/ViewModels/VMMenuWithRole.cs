using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.ViewModels
{
    public class VMMenuWithRole
    {
        public int ID { get; set; }
        public string MENU_NAME { get; set; }
        public string MENU_CONTROLLER { get; set; }
        public string MENU_VIEW { get; set; }
        public bool MENU_CHECKED { get; set; }
        
    }
}
