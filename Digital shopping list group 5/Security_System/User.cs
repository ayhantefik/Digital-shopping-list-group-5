using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital_shopping_list_group_5
{
    public class User   // CHANGED ACCESSIBILITY TO PUBLIC
    {
        string name, email, password;
        bool loggedIn;

        public User(string name, string email, string password, bool loggedIn)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.loggedIn = loggedIn;
        }

        //=============================================================================================================
        //Getters, setters, TBD
        public string Name => name;
        public string Email => email;
        public string Password => password;
        //=============================================================================================================


        
    }
}
