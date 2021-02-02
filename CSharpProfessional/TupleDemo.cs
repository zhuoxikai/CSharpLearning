using System;
using System.Runtime.Remoting.Messaging;

namespace CSharpProfessional
{
    public class TupleDemo
    {
        public static Tuple<int, int> Divide(int dividend, int divisor)
        {
            int result = dividend / divisor;
            int remaider = dividend % divisor;
            
            return Tuple.Create(result,remaider);
        }


    }
}