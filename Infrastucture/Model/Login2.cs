using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Model
{
    public class Login2
    {
        public Login2(string name, string password)
        {
            userName = name;
            pwd = password;
        }

        public string userName { get; set; }
        public string pwd { get; set; }
    }
}
