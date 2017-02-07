using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree
{
   public class Tree
    {

        public Dictionary<string, double> _variables = new Dictionary<string,double>();

        private Node _root;
        //_expression will be used to store the input expression
        public string _expression;


        public Tree()
        {
            _expression = "";
            _root = null;
        }

        public Tree(string expression)
        {
            _expression = expression;

            _root = compile(expression);

        }

        public void buildTree(string expression)
        {
            _expression = expression;
            _root = compile(expression);
        }


        private Node compile(string expression)
        {
            if(string.IsNullOrEmpty(expression))
            {
                return null;
            }

            char[] opperators = { '+', '-', '*', '/' };

            foreach(char op in opperators)
            {
                Node newNode = compile(expression, op);
                if(newNode != null)
                {
                    return newNode;
                }
            }

            //if we get here than expression does not contain opperator
            //return appriate node

            double number;
            if(double.TryParse(expression, out number))
            {
                return new constNode(number);
            }
            else
            {
                //not a double...must be variable
                return new varNode(expression);
            }

        }


        private Node compile(string expression, char op)
        {
            int count = -1;

            for(int i = expression.Length - 1; i != -1; i += count)
            {
                if(op == expression[i])
                {
                    opNode newOpNode = new opNode(expression[i]);
                    newOpNode.left = compile(expression.Substring(0, i));
                    newOpNode.right = compile(expression.Substring(i + 1));

                    return newOpNode;
                }
            }
            return null;
        }


        private double EvaluateTree(Node x)
        {
            
            //check to see if x is const
            constNode checkConst = x as constNode;

            if(checkConst != null)
            {
                //x is a constNode
                //return its value
                return checkConst.val;
            }

            //x is not const
            //check to see if x is var

            varNode checkVar = x as varNode;
            if(checkVar != null)
            {
                //x is varNode
                return _variables[checkVar.varName];

            }





            opNode checkOp = x as opNode;

            if(checkOp != null)
            {
                //x is an opperator

                switch (checkOp.Operator)
                {
                    case '+':
                        return EvaluateTree(checkOp.left) + EvaluateTree(checkOp.right);
                    case '-':
                        return EvaluateTree(checkOp.left) - EvaluateTree(checkOp.right);
                    case '*':
                        return EvaluateTree(checkOp.left) * EvaluateTree(checkOp.right);
                    case '/':
                        return EvaluateTree(checkOp.left) / EvaluateTree(checkOp.right);
                }
                
            }

            //if we get here than we have a problem


            Console.WriteLine("x is not a valid node?");
            return 0;

        }


        //public funtion to evaluate the entire tree
        public double Evaluate()
        {
            return EvaluateTree(_root);
        }

    }
}
