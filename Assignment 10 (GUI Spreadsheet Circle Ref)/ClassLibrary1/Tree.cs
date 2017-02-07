using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree
{
   public class Tree
    {
        /*************************************************************************
          *                           Class Members
          ************************************************************************/
        public Dictionary<string, double> _variables = new Dictionary<string, double>();

        private Node _root;
        //_expression will be used to store the input expression
        public string _expression;
        /*************************************************************************
          *                                 Tree()
          *                          default constructor
          ************************************************************************/
        public Tree()
        {
            _expression = "";
            _root = null;
        }
        /*************************************************************************
         *                          Tree(string expression)
         *                          builds tree with given expression
          ************************************************************************/
        public Tree(string expression)
        {
            _expression = expression;
            _root = compile(expression);
        }

        /*************************************************************************
         *                           buildTree(string expression)
         *                               basicly the same as above
         *                               but does is used when
         *                               a default tree is in use
         *                             
          ************************************************************************/
        public void buildTree(string expression)
        {
            _expression = expression;
            _root = compile(expression);
        }

        /*************************************************************************
         *                      private compile(string expression)
         *                              compiles(ie actualy creates
         *                              the tree) the expression
         *                              NOTE: handles "()"
         *                              
         *                              TODO: use member stirng
         *                              instead of input string?
          ************************************************************************/
        private Node compile(string expression)
        {
            if(string.IsNullOrEmpty(expression))
            {
                return null;
            }

            //removing enclosing parenthesis
            if ('(' == expression[0])
            {
                int pcounter = 1;
               
                for (int i = 1; i < expression.Length; i++)
                {
                    if ('(' == expression[i])
                    {
                        pcounter++;
                    }
                
                    else if (')' == expression[i])
                    {
                        pcounter--;
                       
                        if (0 == pcounter)
                        {
                            if (i != expression.Length - 1)
                            {
                                break;
                            }
                            else
                            {
                                return compile(expression.Substring(1, expression.Length - 2));
                            }
                        }
                    }
                }
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

        /*************************************************************************
         *                           compile(string expression, char op)
         *                              helper function for compile
          ************************************************************************/
        private Node compile(string expression, char op)
        {
            //need to start count at 1 so that i += count is at least 1 more
            int count = 1;
            //count open "(" so we can check for matching ")"
            int parenthesisCounter = 0;

            for(int i = 0; i != expression.Length; i += count)
            {
                //expression cannot start with "("
                if (')' == expression[i] && 0 == parenthesisCounter)
                {
                    //throw execption to produce "high" visablity error
                    throw new Exception();                   
                }
                if ('(' == expression[i])
                {
                    parenthesisCounter++;
                }
                else if (')' == expression[i])
                {
                    parenthesisCounter--;
                }
                //if the parenthesis isnt closed, ignore the expression inside of it
                if (0 != parenthesisCounter)
                {
                    continue;
                }                
                if(op == expression[i])
                {
                    opNode newOpNode = new opNode(expression[i]);
                    newOpNode.left = compile(expression.Substring(0, i));
                    newOpNode.right = compile(expression.Substring(i + 1));

                    return newOpNode;
                }
            }

            if(0 != parenthesisCounter)
            {
                //expression does not have balanced parenthesis
                //show high visabilty error
                throw new Exception();
            }
            return null;
        }

        /*************************************************************************
         *                           EvaluateTree(Node x)
         *                              calculates and returns
         *                              value based on tree
         *                              
         *                          NOTE: throws execption if dividing by 0
         ************************************************************************/
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
                try
                {
                    return _variables[checkVar.varName];
                }
                catch(KeyNotFoundException)
                {
                    return 0;
                }
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
                            if(EvaluateTree(checkOp.right) == 0)
                            {
                                throw new DivideByZeroException("HAHA VERY FUNNY! Trying to divide by 0");
                            }
                            return EvaluateTree(checkOp.left) / EvaluateTree(checkOp.right);
                }                
            }
            //if we get here than we have a problem
            //throw exeption to show high visabilty error
            throw new Exception();
        }
        /************************************************************************
         *                           EvaluateTree()
         *                              public function to evalutate tree
         *                              
         *                          NOTE: throws execption if dividing by 0
         ************************************************************************/
        public double Evaluate()
        {
            return EvaluateTree(_root);
        }
        /************************************************************************
         *                           setVariable(string key, double value)
         *                           sets variable in internal
         *                           dictionary for use by the
         *                           exprssion tree
         ************************************************************************/
        public void setVariable(string key, double value)
        {
            _variables[key] = value;
        }
   }
}
