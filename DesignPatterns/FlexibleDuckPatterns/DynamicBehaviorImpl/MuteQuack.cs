using System;

namespace DesignPatterns.FlexibleDuckPatterns.DynamicBehaviorImpl
{
    public class MuteQuack : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("I can`t quack");
        }
    }
}