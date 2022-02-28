using System;
using System.Collections.Generic;
using System.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBasic
{
    class Program //类默认是internal 
    {
        static void Main(string[] args) //方法默认修饰符为private
        {
            List<string> list = new List<string>() {null, null, null};
            Console.WriteLine(list.Count);
            List<string> list1 = new List<string>() ;
            Console.WriteLine(list1.Count);
            var we1 =list1.First();
        }
    }
}
