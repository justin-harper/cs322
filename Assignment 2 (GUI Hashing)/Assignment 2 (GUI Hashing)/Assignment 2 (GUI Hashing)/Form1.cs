//Justin Harper
//WSUID: 10696738
//Assignment 2

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Assignment_2__GUI_Hashing_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<int> list = new List<int>(10000);
            Random rand = new Random();

            for(int i = 0; i < 10000; i++)
            {
                list.Add(rand.Next(20000));
            }                      
            textBox1.AppendText("List contains " + list.Count().ToString() + " random items in the range [0, 20000]\n");

            //method 1
            textBox1.AppendText("1.) Hash count: " + HashCount(list).ToString()+ " unique entries\n");
            textBox1.AppendText("     The hash method uses an O(N) add method to create the hashset\n");
            textBox1.AppendText("     Then the count function requires O(1) time to execute\n");
            
            //method 2
            textBox1.AppendText("2.) Using an O(N^2) solution to calculate unique values... However this doesn't require any addional space\n");
            textBox1.AppendText("     Number of unique values: " + BruteForce(list).ToString() + "\n");

            //method 3
            textBox1.AppendText("3.) After sorting the list I went through the list 1 time to calculate unique values\n");
            textBox1.AppendText("     Number of unique values is: " + SortedList(list).ToString() + "\n");
        }

        private int HashCount (List<int> list)
        {
            //method 1
            HashSet<int> hash = new HashSet<int>();

            for (int i = 0; i < list.Count(); i++)
            {
                hash.Add(list.ElementAt(i));
            }
            //hash.Count() seems to be reporting 1 too many unique elements
            return hash.Count() - 1;
        }

        private int BruteForce(List<int> list)
        {
            //method 2
            int uniqueValues = 0;

            for (int i = 0; i < list.Count(); i++)
            {
                int checking = list.ElementAt(i);
                bool unique = false;
                for (int j = 0; j < i; j++)
                {
                    if (checking == list.ElementAt(j))
                    {
                        unique = false;
                        break;
                    }
                    else
                    {
                        unique = true;
                    }
                }
                if (unique)
                {
                    uniqueValues++;
                }
            }
            return uniqueValues;
        }

        private int SortedList (List<int> list)
        {
            //method 3
            list.Sort();

            //note starting at 1 so that we can compare to previous value
            int listUnique = 0;
            for(int i = 1; i < list.Count(); i++)
            {
                if(list.ElementAt(i) == list.ElementAt(i-1))
                {
                    continue;
                }
                else
                {
                    listUnique++;
                }
            }
            return listUnique;
        }
    }
}
