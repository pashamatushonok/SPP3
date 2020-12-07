using System;

namespace AssemblyBrowser.TypeComponents
{
    public abstract class AsmTypeComponent
    {
        public string Name { get; protected set; }
        public Type ValueType { get; protected set; }
    }
}