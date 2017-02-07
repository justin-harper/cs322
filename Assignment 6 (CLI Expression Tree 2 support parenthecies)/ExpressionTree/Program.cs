//Justin Harper
//Homework 5


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree
{
    class Program
    {
        static void Main(string[] args)
        {

            Tree expTree = new Tree();


            string expression = "4+5+8";
            expTree.buildTree(expression);



            string input;


            while (true)
            {

                printUI(expression);
                input = Console.ReadLine();

                int choice = -4;
                bool success = int.TryParse(input, out choice);

                if(success == false || choice < 1 || choice > 4)
                {
                    
                    Console.WriteLine("Invalid please try again");
                    Pause();
                }
                else
                {
                    //input is valid

                    switch(choice)
                    {
                        case 1: expression = EnterNewExpression(); expTree.buildTree(expression);
                            break;
                        case 2: string varName = getVarName(); double varVal = getVarVal();
                            expTree._variables[varName] = varVal;
                            break;
                        case 3: Console.WriteLine("result = " + expTree.Evaluate());
                            break;

                        case 4: return;
                    }

                }




            }

        }

        public static string getVarName()
        {
            Console.WriteLine("Please enter variable name:");
            return Console.ReadLine();
        }

        public static double getVarVal()
        {
            Console.WriteLine("Please enter variable value:");
            string input = Console.ReadLine();
            double x;
            if (double.TryParse(input, out x))
            {
                return x;
            }
            else
            {
                Console.WriteLine("Please try again");
                return getVarVal();
            }
        }


        public static string EnterNewExpression()
        {
            Console.WriteLine("Enter a new expression:");
            return Console.ReadLine();

        }

        public static void Pause()
        {
            Console.WriteLine("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        public static void printUI(string expression)
        {
            Console.WriteLine("Menu (current expression = \""+ expression +"\")");

            Console.WriteLine("  1 = Enter a new expression");
            Console.WriteLine("  2 = Set a variable value");
            Console.WriteLine("  3 = Evaluate tree");
            Console.WriteLine("  4 = Quit");

        }


    }
}
