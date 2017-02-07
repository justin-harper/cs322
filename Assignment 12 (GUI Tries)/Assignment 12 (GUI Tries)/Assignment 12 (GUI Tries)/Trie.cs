using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment_12__GUI_Tries_
{
    public class Trie
    {
        internal class Node
        {
            internal char letter;
            internal List<Node> Children = new List<Node>();

            internal Node()
            {
                //only to be used for root case;
            }

            internal Node(char c)
            {
                letter = c;
            }

        }

        internal Node _Root;

        public Trie()
        {

        }
        public void AddString(string s)
        {
            if(s == "")
            {
                return;
            }

            int element = 0;
            

            if(_Root == null)
            {
                _Root = new Node();
                _Root.Children.Add(new Node(s.ElementAt(element)));
            }

            Node current = _Root;


            while(element < s.Length)
            {
                foreach(Node x in current.Children)
                {
                    if(x.letter == s.ElementAt(element) && element < s.Length)
                    {
                        current = x;
                        element++;
                        break;
                    }
                    if(!(element < s.Length))
                    {
                        break;
                    }

                }
                if(!(element < s.Length))
                {
                    break;
                }
                //element < s.Length && s.ElementAt(element) is not in current.children
                //if(ReferenceEquals(current, _Root))
                //{
                //    throw new Exception("current == root");
                //}
                current.Children.Add(new Node(s.ElementAt(element)));

            }
            //element == s.length.... need to add '\0' to current.children
            //used to represent the end of a word
            //that word may be a prefix for another substring later
            //so we need to remember that this is also a word
            current.Children.Add(new Node('\0'));


        }

        public string childrenOfRoot()
        {
            StringBuilder chars = new StringBuilder();
            if(_Root == null)
            {
                return null;
            }
            foreach(Node x in _Root.Children)
            {
                chars.Append(string.Format(x.letter.ToString() + ", "));
            }

            return chars.ToString();
        }

        public List<string> getSuffixes(string prefix)
        {
            List<string> suffixs = new List<string>();

            if(_Root == null)
            {
                suffixs.Add("Trie Hasn't been bult yet!");
                return suffixs;
            }
            if(prefix == "" || prefix == null)
            {
                suffixs.Add("");
                return suffixs;
            }
            Node current = _Root;

            //need to consume the entire prefix string before adding anything to suffixs
            for (int i = 0; i < prefix.Length; i++)
            {
                Node check = current;
                foreach(Node x in current.Children)
                {
                    if(prefix.ElementAt(i) == x.letter)
                    {
                        current = x;
                        break;
                    }
                }
                if(check == current)
                {
                    //prefix string is not in the dictionary
                    suffixs.Add("Not in dictionary");
                    return suffixs;
                }
            }

            //we have consumed the entire prefix string ...now need to add suffixs to suffixs

            foreach(Node n in current.Children)
            {
               // string building = prefix;              
                
                Task.Run(() =>
                    {
                        suffixs.AddRange(getSuffixes(n, prefix));
                    });
            }

            return suffixs;
        }


        private List<string> getSuffixes(Node child, string prefix)
        {
            List<string> s = new List<string>();
            if(child.letter == '\0')
            {
                //prefix is a string in the dictionary
                s.Add(prefix);
            }
            else
            {
                prefix += child.letter.ToString();
                foreach(Node n in child.Children)
                {
                    s.AddRange(getSuffixes(n, prefix));
                }
            }

            return s;
        }


    }
}
