using System;

namespace CSharpProfessional
{
    public class DebugMain
    {
        private static void ValidateFilterInstance(object instance)
        {
            switch (instance)
            {
                case null:
                    break;
                case int _:
                    break;
                case long _:
                    break;
            }
        }
    }
}