using System;
using System.Collections;
using System.Data;
using System.Reflection;


namespace CSharpProfessional
{
     public class Program
    {
        public void Stringdemo()
        {
            char[] chars = new[] {'a', 'b', 'c'};
            String str = new string(chars);
        }
        public string Stringdemo2()
        {
            string str = "abc";
            return str;
        }
        static void MainT()
        {

            
            //
            // #region Reflectiondemo
            //
            // ReflectionDemo.Reflection();
            //
            // #endregion
            // #region Stringdemo
            //
            // /*Program program = new Program();
            // program.Stringdemo();
            // program.Stringdemo2();*/
            //
            // #endregion
            //
            //
            // #region Tuple demo
            //
            // /*
            // var result = TupleDemo.Divide(10, 3);
            // Console.WriteLine($"how to display value,value 1:{result.Item1},value 2:{result.Item2}");
            // */
            //
            // #endregion
            //
            // #region how yield return control
            //
            // /*
            // var game = new GameMoves();
            // IEnumerator enumerator = game.Cross();
            // while (enumerator.MoveNext())
            // {
            //     enumerator = enumerator.Current as IEnumerator;
            // }*/
            //
            // #endregion
            //
            // #region ref type and value type
            //
            // /*int a = 0;
            // DebugMain b = new DebugMain();
            // b.Str = "Hello World";
            // var result = $"a value is {a},and b value is {b?.Str}";
            // b.Str = null;
            // b = null;
            // var result2 = $"a value is {a},and b value is {b?.Str}";
            // Console.WriteLine(result);
            // Console.WriteLine(result2);*/
            //
            // #endregion

        }
    }
}