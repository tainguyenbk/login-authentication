using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class loginResponse
    {
        public bool isValid { get; set; }
        public string token { get; set; }
        public string exp { get; set; }
    }
}
