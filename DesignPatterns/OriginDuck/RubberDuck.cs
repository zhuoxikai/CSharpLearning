using System;

namespace DesignPatterns.OriginDuck
{
    #region 讨论virtual 与 override
    public class RubberDuck : Duck
    {
        //父类没有virtual关键字
        //这里编译器会报一个Warning
        //我们可以通过 new 关键字来告诉编译器我们知道这里的风险
        public void Quack()
            // public new void Quack()
        {
            Console.WriteLine("ji ji ji!");
        }
        //virtual 和 override 都有
        public override void Fly()
        {
            Console.WriteLine("I can`t fly!");   
        }
        //父类有virtual 子类不加override
        public void Display()
        {
            Console.WriteLine("I am a Rudder Duck!");
        }
        
        // static void Main(string[] args)
        static void MainMethod(string[] args)
        {
            Duck duck = new RubberDuck();
            duck.Quack();
            duck.Fly();
            duck.Display();
            
            RubberDuck rudderDuck = new RubberDuck();
            rudderDuck.Quack();
            rudderDuck.Fly();
            rudderDuck.Display();
            
            /*
            Here is result:
            quack quack quack!
            nothing to do!
            I am look like a duck!
            ji ji ji!
            nothing to do!
            I am a Rudder Duck!
            通过以上结果可以看出
            | virtual | override | result   |
            |    √    |    √     | 一定覆盖  |  
            |    √    |    ×     | 取决于调用类型
            |    ×    |    ×     | 取决于调用类型 
            
            当我们想实现通过 声明父类型来动态设置衍生类时 
            必须加上virtual 和 override 关键字
            */
            
            var vDuck = new RubberDuck();
            vDuck.Quack();
            vDuck.Fly();
            vDuck.Display();
            //这里 编译器将 vDuck判断为了RudderDuck类型而不是Duck类型
        }
    }
    #endregion
}