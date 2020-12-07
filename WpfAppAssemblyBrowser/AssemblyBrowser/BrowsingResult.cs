using System.Collections.Generic;

namespace AssemblyBrowser
{
    public class BrowsingResult
    {
        public List<AsmNamespace> Namespaces { get; set;}
            public BrowsingResult()
            {
                Namespaces = new List<AsmNamespace>();
            }
    }
}