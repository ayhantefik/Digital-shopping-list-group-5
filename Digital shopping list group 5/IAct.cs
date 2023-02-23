using System;
using System.Collections.Generic;

namespace Digital_shopping_list_group_5
{

    // All of the 3 actions are common for both an item (en vara) and for a purchase (inköpslistan).
    // Therefore, we do an interface for those concrete classes Item and Purchase
    public interface IAct
    { 
        
        void SaveToDb(Object item);
        List<Object> LoadFromDb();
        void Remove();
        void Display();


    }

}
