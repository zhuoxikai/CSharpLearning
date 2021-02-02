using System;

namespace DesignPatterns.FlexibleDuckPatterns.DynamicBehaviorImpl
{
    public class FlyNoWing : IFlyBehavior
    {
        public void Fly()
        {
            Console.WriteLine("I can`t fly because I don`t have wings");
        }
    }
}