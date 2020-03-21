using System;
using System.Collections.Generic;
using System.Text;

namespace Tutorial1Proj
{
    public class Person
    {
        private string firstName; // field
        //to create property of above:
        public string FirstName // property
        {
            get { return firstName; }
            set { firstName = value; }
        }// shortcut for all this^ :
        public string lastname { get; set; } // auto property
        // ctr + k, ctrl + c - comment selected
        // ctrl + k. ctrl + u - uncomment selected


    }
}
