using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitParser
{
    class Commit
    {        
        public string Author = "";
        public string Date = "";
        public string Summary = "";
        public string Description = "";
        public string CommitText = "";

        public bool isFull()
        {
            if ((Author != "") && (Date != "") && (Summary != "") && (Description != "") && (CommitText != ""))
                return true;
            else return false;
        }        
    }
}
