using System;
using System.Collections.Generic;

namespace Digital_shopping_list_group_5
{

    //Disconnected class (löst kopplad klass).
    //When initiated, it takes advantage of the interface <<IAct>> through the concrete classes <Item>, <Purchase> and <Consumer>
    public class Do 
    {
        private readonly IAct act;
        public Do(IAct act)
        {
            this.act = act;
        }
        public void SaveToDb(object obj)
        {
            act.SaveToDb(obj);
        }
        public List<Object> LoadFromDb()
        {
            //Merge the function LoadFromDb()  from the classes Item,Purchase,Account.
            //The same code in all 3 classes!

            List<Object> list = act.LoadFromDb();
            return list;
        }

        //TO BE IMPLEMENTED...
        //...
        //...
    }
}
