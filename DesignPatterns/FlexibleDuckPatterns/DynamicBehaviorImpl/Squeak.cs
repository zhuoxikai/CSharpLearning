using System;

namespace DesignPatterns.FlexibleDuckPatterns.DynamicBehaviorImpl
{
    public class Squeak : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine(" Ji Ji Ji !");
        }
    }
}