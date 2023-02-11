using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Model
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }

        public bool IsAdmin { get; set; }

        public User(int iD, string userName, string userPass, bool isAdmin)
        {
            ID = iD;
            UserName = userName;
            UserPass = userPass;
            IsAdmin = isAdmin;
        }

        public User() { }
    }
}
