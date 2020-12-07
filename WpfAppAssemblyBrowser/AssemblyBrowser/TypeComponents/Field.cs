using System.Reflection;
using AssemblyBrowser.Enums;

namespace AssemblyBrowser.TypeComponents
{
    public class Field : AsmTypeComponent
    {
        public bool IsReadonly { get; set;}
        public AccessModifier Modifier { get; set;}
        public bool IsStatic { get; set;}

        public Field(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            ValueType = fieldInfo.FieldType;

            if (fieldInfo.IsPublic)
            {
                Modifier = AccessModifier.Public;
            }

            if (fieldInfo.IsPrivate)
            {
                Modifier = AccessModifier.Private;
            }

            if (fieldInfo.IsFamily)
            {
                Modifier = AccessModifier.Protected;
            }

            if (fieldInfo.IsAssembly)
            {
                Modifier = AccessModifier.Internal;
            }

            var attr = fieldInfo.Attributes;
            IsReadonly = (attr & FieldAttributes.InitOnly) != 0;
            IsStatic = (attr & FieldAttributes.Static) != 0;
        }
    }
}