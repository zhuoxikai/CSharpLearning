/*只在编译的时有用，不针对业务逻辑 、
此外还有 undef  
条件 if elif else endif
异常 warning error
例如：
    #if DEBUG && RELEASE
    #error "You've defined DEBUG and RELEASE simultaneously!"
    #endif
    #warning "Don't forget to remove this line before the boss tests the code!"
不常用 region endregion line pragma
 */
#define DEBUG

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSharpProfessional
{
    /// <summary>
    ///  自定义的特性需要继承 System.Attribute类
    ///  特性需要以Attribute结尾 在使用的时候可以省略
    ///  AttributeUsage是预定义特性 除此以外还有 Conditional Obsolete
    ///     AttributTargets 规定特性的作用范围
    ///     AllowMultiple(可选)指明 同类元素是否允许多次应用同一特性 默认false
    ///     Inherited(可选) 是否可以继承 默认false
    /// </summary>
    [AttributeUsage(AttributeTargets.All,AllowMultiple =true,Inherited =true)]
    class DeBugInfoAttribute : System.Attribute
    {
        private int _bugNo;
        private string _developer;
        private string _reportTime;//之前将该类型定义为DateTime，但由于Attribute不支持struct,所以换成string 
        private string _message;

        public int BugNo { get => _bugNo; set => _bugNo = value; }
        public string Developer { get => _developer; set => _developer = value; }
        
        public string ReportTime { get => _reportTime; set => _reportTime = value; }
        public string Message { get => _message; set => _message = value; }

        public DeBugInfoAttribute(int bugNo, string developer, string reportTime)
        {
            BugNo = bugNo;
            Developer = developer;
            ReportTime = reportTime;
        }
    }

    
    class AttributeDemo
    {
        private int _age;
        public int Age { get => _age; set => _age = value; }

        /// <summary>
        ///  Obsolete 预定义属性 标记已过时的方法
        ///  message 用于表明 原因与代替方法
        ///  iserror 告诉编辑使用这个方法是否算错误
        /// </summary>
        /// <returns></returns>
        [Obsolete("this is obsolete,Please use GetAgeTwo instead.",true)]
        public int GetAge()
        {
            return _age;
        }
        
        public int GetAgeTwo()
        {
            return Age;
        }

        /// <summary>
        ///  Conditional 只能用于 void返回
        ///  conditionString 通过#define 定义
        /// </summary>
        /// <param name="meassage"></param>
        [Conditional("DEBUG")]
        public void Printer(string meassage)
        {
            Console.WriteLine(meassage);
        }

        
    }
}
