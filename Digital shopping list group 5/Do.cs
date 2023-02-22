using System;

namespace Digital_shopping_list_group_5
{

    //Disconnected class (löst kopplad klass).
    //When initiated, it takes advantage of the interface <<IAct>> through the concrete classes <Item> and <Purchase>
    public class Do
    {
        private readonly IAct act;
        public Do(IAct act)
        {
            this.act = act;
        }
        public void Add(Object obj)
        {
            act.Add(obj);
        }

        //TO BE IMPLEMENTED...
        //...
        //...
    }
}
