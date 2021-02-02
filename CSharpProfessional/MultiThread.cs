using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProfessional
{
    class MultiThread
    {
        void ThreadIncreasing()
        {
            Increase demo = new Increase();
            //并行编程
        }

    }
    class Increase
    {
        volatile int inc = 0;
        internal void Increasing()
        {
            inc++;
        }
    }
}
