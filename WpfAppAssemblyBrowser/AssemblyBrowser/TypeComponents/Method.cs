using System.Collections.Generic;
using System.Reflection;
using AssemblyBrowser.Enums;

namespace AssemblyBrowser.TypeComponents
{
    public class Method : AsmTypeComponent
    {
        public List<Parameter> Parameters { get; set;}
        public AccessModifier Modifier { get; set;}
        public bool IsAbstract { get; set;}
        public bool IsVirtual { get; set;}
        public bool IsStatic { get; set;}

        public Method(MethodInfo methodInfo)
        {
            Name = methodInfo.Name;
            ValueType = methodInfo.ReturnType;

            if (methodInfo.IsPublic)
            {
                Modifier = AccessModifier.Public;
            }

            if (methodInfo.IsPrivate)
            {
                Modifier = AccessModifier.Private;
            }

            if (methodInfo.IsFamily)
            {
                Modifier = AccessModifier.Protected;
            }

            if (methodInfo.IsAssembly)
            {
                Modifier = AccessModifier.Internal;
            }

            MethodAttributes attr = methodInfo.Attributes;
            IsVirtual = (attr & MethodAttributes.Virtual) != 0;
            IsAbstract = (attr & MethodAttributes.Abstract) != 0;
            IsStatic = (attr & MethodAttributes.Static) != 0;


            Parameters = new List<Parameter>();
            ParameterInfo[] parameters = methodInfo.GetParameters();
            foreach (var parameter in parameters)
            {
                var newParameter = new Parameter(parameter);
                Parameters.Add(newParameter);
            }
        }
    }
}