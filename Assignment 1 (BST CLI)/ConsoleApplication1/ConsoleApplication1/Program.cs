using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = UI();
            string[] data = new string[1];

            switch(input)
            {
                case 1: data = useTestData();
                    break;
                case 2: data = getInputData();
                    break;

                case 3: data = useTestData();
                    return;
                
            }

            GenericBST<int> BST = buildTree(data);

            Console.Clear();
            Console.WriteLine("Finished adding elements to tree");

            traverseTree(BST);

            return;
             
        }

        public static void traverseTree(GenericBST<int> BST)
        {
            StringBuilder output = new StringBuilder();
            output.Append("");
            BST.getList(output);

            Console.WriteLine("Inorder traversal:");
            
            Console.WriteLine(output);
            Console.Write("Number of nodes: ");
            Console.WriteLine(BST.getNumNodes().ToString());
            Console.Write("Height: ");
            Console.WriteLine(BST.calcHeight());

            int minHeight = BST.getNumNodes();
            minHeight = (int) Math.Ceiling(((Math.Log10(minHeight + 1)/Math.Log10(2)) - 1));
            
            Console.WriteLine("Min levels with {0} nodes is: {1}", BST.getNumNodes().ToString(), minHeight);
            Console.WriteLine("Press enter to continue . . .");

            Console.ReadLine();

            return;
        }

        public static int UI()
        {
            String userInput;
            Console.WriteLine("Welcome to Assignment 1");
            Console.WriteLine("Enter your choice to continue");
            Console.WriteLine("1: Use test data to build tree");
            Console.WriteLine("2: Enter numbers manually");
            Console.WriteLine("3: Quit");
            Console.Write("your choice: ");
            userInput = Console.ReadLine();

            int input;
            bool sucsess = int.TryParse(userInput, out input);
            if(!sucsess)
            {
                Console.WriteLine("Oops that's not a number... Please try again");
                Console.ReadLine();
                Console.Clear();
                return UI();
            }
            else
            {
                return input;
            }

        }

        public static string[] useTestData()
        {
            Console.Clear();
            Console.WriteLine("You have chosen to use test data");
            Console.WriteLine("this is the sequence to be used:");
            
            string testData = "5 17 36 99 3 4 1 50 22 87";
            Console.WriteLine(testData);

            return testData.Split(' ');

           
        }

        public static string[] getInputData()
        {
            string inputData = "";

            Console.Clear();
            Console.WriteLine("You have chosen to input numbers manually");
            Console.WriteLine("Please enter numbers seperated by a space");

            inputData = Console.ReadLine();

            return inputData.Split(' ');

        }

        public static GenericBST<int> buildTree(string[] input)
        {
            GenericBST<int> tree = new GenericBST<int>();

            for (int i = 0; i < input.Length; i++)
            {
                int x;
                bool result = int.TryParse(input[i], out x);

                if (result)
                {
                    tree.addNode(x);
                }
                else
                {
                    Console.WriteLine("ERROR DID NOT CONVERT NUMBER TO INT");
                    Console.WriteLine("Continuing...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            return tree;
        }
    }
}
