using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Login
    {
        public Login(string name, string password)
        {
            username = name;
            pwd = password;
        }

        public string username { get; set; }
        public string pwd { get; set; }
    }
}
