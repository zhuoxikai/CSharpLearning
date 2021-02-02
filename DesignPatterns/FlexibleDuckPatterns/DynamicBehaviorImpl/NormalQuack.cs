using System;

namespace DesignPatterns.FlexibleDuckPatterns.DynamicBehaviorImpl
{
    public class NormalQuack : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("Quack Quack Quack!");
        }
    }
}