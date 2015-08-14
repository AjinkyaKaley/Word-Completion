using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WordCompletionModule


{

    /// <summary>
    /// This class  implements the word completion module
    /// To run this please use the data set of english words saved it in current directory, and name
    /// the file "english-words.txt" i
    /// 
    /// Author : Ajinkya Kale
    /// </summary>
    class Trie
    {
        private Node root;      // This stores the root of the trie
        private static Hashtable letter_indices = new Hashtable();  // this is the symbol table, where symbols are the english alphabets A-Z associated with corresponding to indices 0-25

        /// <summary>
        /// This static block initializes the look up hashtable
        /// with a pair of char and index
        /// </summary>
        static Trie()
        {
            for (int i = 0; i < 26; i++ )
            {
                letter_indices.Add(((char)(65+i)), i);
            }
        }

        /// <summary>
        /// 
        /// This default constructor creates the root node 
        /// for the trie
        /// </summary>
        public Trie()
        {
            this.root = new Node();
        }

        /// <summary>
        /// This method inserts the word 
        /// into tries data structure
        ///  
        /// Implementation sucessfull
        /// handled all the cases
        /// 
        /// </summary>
        /// 
        private Node insert(Node root, char[] word, int index)
        {
            if(root == null)            // Base condition
            {
                root = new Node();
            }

            if (index == word.Length)       // second base condition
            {
                root.value = new string(word);
                return root;
            }

            char c = word[index];
            index++;
            char x = char.ToUpper(c);
            string temp = letter_indices[x].ToString();
            // creates the new node for that char c i.e letter
            root.letter_pointers[Convert.ToInt32(temp)] = insert(root.letter_pointers[Convert.ToInt32(temp)], word, index);
            return root;
        }

        /// <summary>
        /// 
        /// Finds the word in tries
        /// 
        /// tested for two cases
        /// worked fine
        /// </summary>
        /// <param name="root"></param>
        /// <param name="word"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private Node find(Node root, char[] word, int index)
        {
            if(root ==  null)
            {
                return null;
            }

            if(index == word.Length)
            {
                return root;
            }

            char c = word[index];
            index++;
            char x = char.ToUpper(c);
            string temp = letter_indices[x].ToString();
            return find(root.letter_pointers[Convert.ToInt32(temp)], word, index);

        }

        /// <summary>
        /// 
        /// This method finds the prefix nodes for the
        /// given string
        /// bascially it iterates over the trie to reach a point where
        /// prefix ends and distinct strings start
        /// </summary>
        /// <param name="root"></param>
        /// <param name="prefix"></param>
        /// <param name="que"></param>
        /// <returns></returns>
        private Queue<String> find_prefix(Node root, char [] prefix, Queue<String> que)
        {
            int index_for_first_letter = Convert.ToInt32(letter_indices[char.ToUpper(prefix[0])].ToString());
            if (root.letter_pointers[index_for_first_letter] == null)
            {
                return que;
            }

            //Queue<String> q = new Queue<string>();
            foreach(char c in prefix)
            {
                int index = Convert.ToInt32(letter_indices[char.ToUpper(c)].ToString());
              
                if(root.letter_pointers[index]!=null)
                {
                    root = root.letter_pointers[index];
                }

             }

            return Collect(root, que);
        }

        /// <summary>
        /// This method collects all the strings that starts with the
        /// given prefix
        /// </summary>
        /// <param name="root"></param>
        /// <param name="que"></param>
        /// <returns></returns>
        private Queue<String> Collect(Node root , Queue<String> que)
        {
            
            if(root.value != null)
            {
                que.Enqueue(root.value);
            }

            foreach(Node c in root.letter_pointers)
            {
                if(c != null)
                {
                    root = c;
                    Collect(root, que);
                }
            }
            return que;
        }



        static void Main(string[] args)
        {
            Trie t = new Trie();

            string[] lines = System.IO.File.ReadAllLines(Environment.CurrentDirectory + "\\english-words.txt");
            foreach(string word in lines)
            {
                t.insert(t.root, word.ToCharArray(),0);
            }


            string input = Console.ReadLine();

            Queue<String> list = t.find_prefix(t.root,input.ToCharArray(), new Queue<string>());
            foreach(String s in list)
            {
                Console.WriteLine(s);
            }
            Console.ReadKey();

        }
    }
}
