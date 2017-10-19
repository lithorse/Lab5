using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class User
    {
        public String Name { get; set; }
        public String Email { get; set; }
        public User(String Name, String Email)
        {
            this.Name = Name;
            this.Email = Email;
        }
    }
}
