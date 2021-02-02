using System;

namespace DesignPatterns.OriginDuck
{
    #region 讨论这种设计模式的好坏

    public class RedheadDuck : Duck
    {
        //重写父类方法
        public override void Display()
        {
            Console.WriteLine("I am a red head duck!");
        }
        //这样的设计模式会导致
        //当不能复用父类的方法时
        //每一个子类都需要单独实现自己的功能
        //其实子类与子类可能还有很多代码复用的时候
        //并且父类的变化可能会给子类带来无法预料的影响
        //比如在父类中加入 Swim()
        //但橡皮鸭(RudderDuck)子类 并不需要
    }

    #endregion
}