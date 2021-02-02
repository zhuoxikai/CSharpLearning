using System;

namespace DesignPatterns.FlexibleDuckPatterns.DynamicBehaviorImpl
{
    public class FlyWithWings : IFlyBehavior
    {
        
        /*private readonly FlyBehavior _flyBehaviorImplementation;

        public FlyWithWings(FlyBehavior flyBehaviorImplementation)
        {
            _flyBehaviorImplementation = flyBehaviorImplementation;
        }

        public void Fly()
        {
            _flyBehaviorImplementation.Fly();
        }*/
        //直接实现方法
        public void Fly()
        {
            Console.WriteLine("I am flying with my wings");
        }
    }
}