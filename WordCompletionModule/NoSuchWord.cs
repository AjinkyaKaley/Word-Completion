using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCompletionModule
{
    class NoSuchWord : Exception
    {
        public NoSuchWord(string except)
        {
            Console.WriteLine("No Such Word Found!!!");
        }
    }
}
