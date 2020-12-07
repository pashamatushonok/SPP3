using System;
using System.Reflection;
using AssemblyBrowser.Enums;

namespace AssemblyBrowser.TypeComponents
{
    public class Parameter : AsmTypeComponent
    {
        public new string Name { get; set;}
        public new Type ValueType { get; set;}
        public ParameterType PassingType { get; set;}
        public bool IsOptional { get; set;}

        public Parameter(ParameterInfo parameter)
        {
            Name = parameter.Name;
            ValueType = parameter.ParameterType;

            if (parameter.IsIn)
            {
                PassingType = ParameterType.In;
            }
            else if (parameter.IsOut)
            {
                PassingType = ParameterType.Out;
            }

            IsOptional = parameter.IsOptional;
        }
    }
}