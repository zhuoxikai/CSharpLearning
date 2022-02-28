using System;

namespace CSharpBasic.StaticAndExtends
{
    public class Father
    {
        public static string StaticStr = "I am static string";
        public string NoStaticStr = "I am not static string";

        public static void StaticMethod()
        {
            Console.WriteLine("I am static method");
        }

        public void NoStaticMethod()
        {
            Console.WriteLine("I am not static method");
        }
    }
}