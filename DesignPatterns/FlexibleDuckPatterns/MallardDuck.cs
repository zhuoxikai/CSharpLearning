using System;
using DesignPatterns.FlexibleDuckPatterns.DynamicBehaviorImpl;

namespace DesignPatterns.FlexibleDuckPatterns
{
    //
    public class MallardDuck :BetterDuck
    {
        
        //这里还是显式的实现了具体对象
        protected MallardDuck()
        {
            QuackBehavior = new NormalQuack();
            FlyBehavior = new FlyWithWings();
        }

        public override void Display()
        {
            Console.WriteLine("I am a real MallardDuck!");
        }

        static void Main()
        {
            BetterDuck duck = new MallardDuck();
            duck.PerformFly();
            duck.PerformQuack();
        }

    }
}