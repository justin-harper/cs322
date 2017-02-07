using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    public class GenericBST<T> where T : IComparable<T>
    {
        public Node root;
        private int numNodes;


        public class Node
        {
            public Node leftChild;
            public Node rightChild;

            private T data;

            public Node(T t)
            {
                leftChild = null;
                rightChild = null;

                data = t;
            }

            public T getData()
            {
                return data;
            }

        }

        public GenericBST()
        {
            root = null;
            numNodes = 0;
        }

        public void addNode(T value)
        {
            if (root == null)
            {
                root = new Node(value);
                numNodes++;
            }
            else
            {
                addNode(value, root);
            }

            return;
        }

        public int getNumNodes()
        {
            return numNodes;
        }

        private void addNode(T value, Node current)
        {
            //will only add "value" to tree if it does not already exist
            //if value already exists this function will return normally

            if (current.getData().Equals(value))
            {
                return;
            }
            else
            {

                T currentData = current.getData();
                int compareValue = currentData.CompareTo(value);

                //if compareValue == 0 ==> values are equal
                //if compareValue == -1 ==> value < currentData
                //if compareValue == 1 ==> value > currentData

                if (compareValue == 0)
                {
                    //do not add duplicate values
                    return;
                }
                else
                {
                    Node next;

                    if (compareValue > 0)
                    {
                        //go left
                        next = current.leftChild;
                        if (next == null)
                        {
                            current.leftChild = new Node(value);
                            numNodes++;
                        }
                        else
                        {
                            addNode(value, current.leftChild);
                        }
                    }
                    else
                    {
                        //go right
                        next = current.rightChild;
                        if (next == null)
                        {
                            current.rightChild = new Node(value);
                            numNodes++;
                        }
                        else
                        {
                            addNode(value, current.rightChild);
                        }
                    }
                }

            }
            return;
        }

        public void getList(StringBuilder output)
        {
            //note this function will clear output

            //output = "";
            getList(output, root);

            return;
        }

        private void getList(StringBuilder output, Node current)
        {
            if (current == null)
            {
                return;
            }
            else
            {
                //first go left
                getList(output, current.leftChild);

                //then get "us"
                //string temp = current.getData().ToString() + " ";
                //output += temp;
                //output += current.getData().ToString() + " "; 

                output.Append(current.getData().ToString() + " ");

                //then go right

                getList(output, current.rightChild);

            }
            return;
        }

        public void printTree()
        {
            printTree(root);
            return;
        }
        public void printTree(Node current)
        {

            if (null == current)
            {
                return;
            }
            else
            {
                printTree(current.leftChild);
                Console.Write(current.getData().ToString() + " ");
                printTree(current.rightChild);
            }

            return;
        }

        public int calcHeight()
        {
            if (root == null)
            {
                return -1;
            }
            else
            {
                return calcHeight(root);
            }
        }

        public int calcHeight(Node node)
        {
            int count = 0;
            if (node.leftChild != null)
            {
                count += calcHeight(node.leftChild);
            }
            else if (node.rightChild != null)
            {
                count += calcHeight(node.rightChild);
            }
            return count + 1;

        }

        public int max(int x, int y)
        {
            if (x > y)
            {
                return x;
            }
            else
            {
                return y;
            }
        }
    }

}
