using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace HW3
{
    class FibonacciSeq : TextReader
    {

        string fibSeq;

        BigInteger bigA;
        BigInteger bigB;
        BigInteger bigC;

        
        public FibonacciSeq(int n)
        {
            fibSeq = bigFib(n);
        }

        public override string ReadToEnd()
        {
            
            return fibSeq;
        }
        
        public string bigFib(int n)
        {
            StringBuilder sb = new StringBuilder();
            bigA = 0;
            bigB = 1;
            bigC = 0;

            sb.Append("1: 0\r\n2: 1\r\n");

            for (int i = 2; i < n; i++)
            {
                bigC = bigA + bigB;
                sb.AppendFormat("{0}: {1}\r\n", (i + 1), bigC.ToString());
                bigA = bigB;
                bigB = bigC;
            }

            return sb.ToString();



        }

    }
}
