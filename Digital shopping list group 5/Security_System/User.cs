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

        public User(string email, string password, bool loggedIn, string name)
        {            
            this.email = email;
            this.password = password;
            this.loggedIn = loggedIn;
            this.name = name;
        }

        //=============================================================================================================
        //Getters, setters, TBD
        public string Email => email;  public void SetEmail(string value) => email = value;

        public string Password => password; public void SetPassword(string value) => password = value;
        public string Name => name; public void SetName(string value) => name = value;
        public bool LoggedIn => loggedIn; public void SetLoggedIn(bool value) => loggedIn = value;
        //=============================================================================================================



    }
}
