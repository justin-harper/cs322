using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree
{
    public abstract class Node
    {
       
    }

    public class constNode : Node
    {
        public double val;

        public constNode(double value)
        {
            val = value;
        }
    }

    public class varNode : Node
    {
        public string varName;

        public varNode (string name)
        {
            varName = name;
        }


    }
    public class opNode : Node 
    {
        public char Operator;

        public Node left;

        public Node right;


        public opNode (char op)
        {
            Operator = op;
        }

    }





}
