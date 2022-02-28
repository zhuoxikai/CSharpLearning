using System;

namespace Delegate
{
    public class sample
    {
        public delegate void Del(string message);


        public static void DelegateMethod(string message)
        {
            Console.WriteLine(message);
        }

        private Del handler = DelegateMethod;
        
    }
}