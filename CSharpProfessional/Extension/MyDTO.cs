using System;

namespace CSharpProfessional.Extension
{
    public interface MyDTO
    {
        string Name { get; }

        Guid uid { get; }

        void Method();

    }
}