using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpProfessional
{
    public class MultiThread
    {
        public void ThreadIncreasing()
        {
            Increase demo = new Increase();
            //并行编程
            Thread increasing = new Thread(demo.Increasing);
            increasing.Start();
            Console.WriteLine("Starting Main thead");
            while (!increasing.IsAlive) ;
            Thread.Sleep(50);
            demo.Break();
            increasing.Join();
            Console.WriteLine("thread terminated");
        }

    }
    class Increase
    {
         int inc = 0;
        private bool flag = true;
        public void Break()
        {
            flag = false;
        }
        internal void Increasing()
        {
            while (flag)
            {
                inc++;
            }
            Console.WriteLine($"num is {inc}");
        }
    }
}
