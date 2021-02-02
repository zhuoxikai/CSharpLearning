using System;

namespace DesignPatterns.OriginDuck
{
    //this is a superClass
    public class Duck
    {
        #region here is field

        private int _age;

        private string _name;
        
        #endregion
        
        #region here is properties
        
        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        #endregion

        #region here is constructor

        public Duck(int age, string name)
        {
            _age = age;
            _name = name;
        }

        //here is default constructor
        // 类在创建的时候都会有隐式的默认方法
        // 在构造函数被显式的声明后
        // 隐式的默认方法会被覆盖
        // 同理当继承父类后，子类的默认方法被覆盖
        // 所以要显式声明
        //SubClass need a 
        protected Duck()
        {
            
        }

        #endregion
        
        #region here is method
        public void Quack()
        {
            Console.WriteLine("quack quack quack!");
        }

        public virtual void Fly()
        {
            Console.WriteLine("I am fly!");
        }

        //虚方法才可以被override 
        public virtual void Display()
        {
            Console.WriteLine("I am look like a duck!");
        }
        
        #endregion 
        
    }
}