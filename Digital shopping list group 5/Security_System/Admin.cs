using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital_shopping_list_group_5
{
    public class Admin : User
    {

        //Admin´s arcitecture TBD if time allows.
        string filePath;
        public Admin(string name, string email, string password, bool loggedIn, string filePath)
            : base(email, password, loggedIn,name)
        {
            this.filePath = filePath;
        }
    }
}
