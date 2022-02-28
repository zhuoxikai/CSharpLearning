using System.Xml;

namespace CSharpProfessional.Extension
{
    public static class MyDTOExtension
    {
        private const string Name = "Chare";
        public static string GetName(this MyDTO myDto)
        {
            if (string.IsNullOrEmpty(myDto.Name))
            {
                return Name;
            }
            return myDto.Name;
        } 
    }
}