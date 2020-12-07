using System.Collections.Generic;
using System.Reflection;
using AssemblyBrowser.Enums;

namespace AssemblyBrowser.TypeComponents
{
    public class Constructor : AsmTypeComponent
    {
        public new const string Name = "constructor";
        public AccessModifier Modifier { get; set;}
        public List<Parameter> Parameters { get; set;}
        public bool IsStatic { get; set;}

        public Constructor(ConstructorInfo constructor)
        {
            if (constructor.IsPublic)
            {
                Modifier = AccessModifier.Public;
            }

            if (constructor.IsPrivate)
            {
                Modifier = AccessModifier.Private;
            }

            if (constructor.IsFamily)
            {
                Modifier = AccessModifier.Protected;
            }

            if (constructor.IsAssembly)
            {
                Modifier = AccessModifier.Internal;
            }

            ParameterInfo[] parameters = constructor.GetParameters();
            Parameters = new List<Parameter>();
            foreach (var parameter in parameters)
            {
                var newParameter = new Parameter(parameter);
                Parameters.Add(newParameter);
            }

            IsStatic = constructor.IsStatic;
        }
    }
}