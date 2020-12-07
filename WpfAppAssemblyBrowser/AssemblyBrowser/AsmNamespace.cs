using System.Collections.Generic;

namespace AssemblyBrowser
{
    public class AsmNamespace
    {
        public string Name { get; set; }
        public List<AsmType> AssemblyTypes { get; set; }

        public AsmNamespace(string name)
        {
            Name = name;
            AssemblyTypes = new List<AsmType>();
        }
    }
}