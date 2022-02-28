using System;
using System.Collections.Generic;

namespace CSharpBasic
{
    public class generics
    {
        internal sealed class DictionaryStringKey<TValue> :Dictionary<String, String> 
        { 
        }

        //讨论泛型的实例化
        private static void RunGenerics()
        {
            //编译会通过，在CreateInstance的时候会校验Type种ContainsGenericsParameters
            Object o = null; 
            // Dictionary<,> 有2个类型参数的开放类型 
            Type t = typeof(Dictionary<,>); 
            // 创建实例会失败 
            o = CreateInstance(t); 
            Console.WriteLine(); 
            
            
            // DictionaryStringKey<>有一个类型参数的开发类型 
            t = typeof(DictionaryStringKey<>); 
            // 创建该类型的实例也会失败 
            o = CreateInstance(t); 
            Console.WriteLine(); 
            
            
            // DictionaryStringKey<Guid> 是闭合类型 
            t = typeof(DictionaryStringKey<Guid>); 
            // 创建成功 
            o = CreateInstance(t); 
            // 输出类型名字 
            Console.WriteLine("Object type=" + o.GetType()); 
        }
        private static Object CreateInstance(Type t) 
        { 
            Object o = null; 
            try 
            { 
                //使用默认的构造函数来创造该类型的实例 
                o = Activator.CreateInstance(t); 
                Console.Write("Created instance of {0}", t.ToString()); 
            } 
            catch (ArgumentException e) 
            { 
                Console.WriteLine(e.Message); 
            } 
            return o; 
        } 
        // private static void Main(string[] args) 
        // {
        //     RunGenerics();
        // }
    }
}