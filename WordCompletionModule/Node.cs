using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCompletionModule
{
    class Node
    {
        public Node[] letter_pointers;
        public string value;

        public Node()
        {
            this.letter_pointers = new Node[26];
           
        }
    }
}
