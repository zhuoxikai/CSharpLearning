using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProfessional
{


    [DeBugInfo(608, "Mike", "2077/1/12 17:37:02",Message = "this is a serious bug")]
    public class Student
    {
        private int _age;
        private int _class;
        private DateTime _birthday;

        [DeBugInfo(609,"Lily", "2077/1/12 17:37:02",Message = "age range isn't limited")]
        public int Age { get => _age; set => _age = value; }
        public int Class { get => _class; set => _class = value; }
        
        public DateTime Birthday { get => _birthday; set => _birthday = value; }

        //遇到关键字冲突时 可以用@转义
        public Student(int age, int @class,DateTime birthday)
        {
            Age = age;
            Class = @class;
            Birthday = birthday;
        }

        [DeBugInfo(605,"Yui", "2077/1/12 17:37:02",Message = "Useless method")]
        public void GrowUp()
        {
            Age++;
        }
        public override string ToString()
        {
            return "Age:" + Age + " Class:" + Class;
        }
    }
    public class Person
    {
        private int _age;
        private DateTime _birthday;

        [DeBugInfo(609,"Lily", "2077/1/12 17:37:02",Message = "age range isn't limited")]
        public int Age { get => _age; set => _age = value; }
        public DateTime Birthday { get => _birthday; set => _birthday = value; }
        
        public Person(int age,DateTime birthday)
        {
            Age = age;
            Birthday = birthday;
        }
        public Person() { }
    }
    public class ReflectionDemo
    {
        public static void Reflection()
        {
            Student student = new Student(20, 02,DateTime.MinValue);
            student.GrowUp();
            Person person = new Person();
            Type type = typeof(Student);
            //获得class的每个Attribute 
            foreach (Attribute attribute in type.GetCustomAttributes(false))
            {
                //类型转换 因为会有多个特性 所以再这里需要合理的校验
                Console.WriteLine("attribute is " + attribute.ToString());
                DeBugInfoAttribute deBugInfo = (DeBugInfoAttribute)attribute;
                if (null != deBugInfo)
                {
                    ReflectionDemo.PrinterDebugInfo(deBugInfo);
                }
            }

            
            foreach (var property in type.GetProperties())
            {
                //这个判断不能去掉 会直接导致强转出错 抛Exception
                if (property.PropertyType == typeof(DateTime))
                {
                    if ((DateTime)property.GetValue(student) == DateTime.MinValue)
                    {
                        //这里的第一个值 必须是这个对象的类或衍射类 值必须能存入对象 不支持强转
                        property.SetValue(student,"");
                        //property.SetValue(person,""); 会通过编译 但是错误 
                        //property.SetValue(person.birthday,DateTime.MinValue); 同上
                    }
                }
                Console.WriteLine(property);
            }
            
            foreach (MethodInfo method in type.GetMethods())
            {
                Console.WriteLine("Method name is :" + method.Name);
                foreach (Attribute attribute in method.GetCustomAttributes(false))
                {
                    //会执行子类的ToString
                    Console.WriteLine("attribute is " + attribute.ToString());

                    if (method.IsDefined(typeof(DeBugInfoAttribute), false))
                    {
                        DeBugInfoAttribute deBugInfo = (DeBugInfoAttribute)attribute;
                        ReflectionDemo.PrinterDebugInfo(deBugInfo);
                    }
                }
            }
            
            Console.WriteLine("Program end!");
            Console.ReadKey();
        }
        internal static void PrinterDebugInfo(DeBugInfoAttribute deBugInfo)
        {
            Console.WriteLine("attribute.BugNo is " + deBugInfo.BugNo);
            Console.WriteLine("attribute.developer is " + deBugInfo.Developer);
            Console.WriteLine("attribute.ReportTime is " + deBugInfo.ReportTime);
            Console.WriteLine("attribute.Message is " + deBugInfo.Message);
            Console.WriteLine("==================================");
        }
    }
}
