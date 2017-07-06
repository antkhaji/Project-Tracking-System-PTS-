using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSLibrary
{
    class Customer : User
    {
       public Customer (string name, int id)
        {
            // these name and customer are not declared in the Customer class but rather inherited from the 
            // User class
            this.id = id;
            this.name = name; 
        }

    }
}
