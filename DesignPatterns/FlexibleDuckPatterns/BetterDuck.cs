using System;

namespace DesignPatterns.FlexibleDuckPatterns
{
    //将稳定的代码放入超类
    //将变化的代码封装成接口
    public class BetterDuck
    {
        private IFlyBehavior _flyBehavior;
        private IQuackBehavior _quackBehavior;

        public IFlyBehavior FlyBehavior
        {
            get => _flyBehavior;
            set => _flyBehavior = value;
        }

        public IQuackBehavior QuackBehavior
        {
            get => _quackBehavior;
            set => _quackBehavior = value;
        }

        protected BetterDuck()
        {
        }

        
        #region here is method

        public void PerformQuack()
        {
            _quackBehavior.Quack();
        }

        public void SetPerformQuack(IQuackBehavior quackBehavior)
        {
            _quackBehavior = quackBehavior;
        }
        public void PerformFly()
        {
            _flyBehavior.Fly();
        }

        public  void SetPerformFly(IFlyBehavior flyBehavior)
        {
            _flyBehavior = flyBehavior;
        }

        public virtual void Display()
        {
            Console.WriteLine("this is method to display instance duck");
        }
        
        #endregion
    }
}