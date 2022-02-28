using System;

namespace CSharpProfessional
{
    public class AppDomainTest
    {
        public void method()
        {
            AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}