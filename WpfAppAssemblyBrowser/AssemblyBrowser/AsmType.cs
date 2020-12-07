using System;
using System.Collections.Generic;
using System.Reflection;
using AssemblyBrowser.Enums;
using AssemblyBrowser.TypeComponents;

namespace AssemblyBrowser
{
    public class AsmType
    {
        private const BindingFlags BindingFlags = System.Reflection.BindingFlags.Instance |
                                                  System.Reflection.BindingFlags.Public |
                                                  System.Reflection.BindingFlags.NonPublic |
                                                  System.Reflection.BindingFlags.Static;

        public string Name { get; set;}
        public bool IsNested { get; set;}
        public bool IsClass { get; set;}
        public bool IsStructure { get; set;}
        public bool IsSealed { get; set;}
        public bool IsInterface { get; set;}
        public bool IsAbstract { get; set;}
        public AccessModifier Modifier { get; set;}
        public List<Field> Fields { get; set;}
        public List<Property> Properties { get; set;}
        public List<Method> Methods { get; set;}
        public List<Constructor> Constructors { get; set;}
        public bool IsEnum { get; set;}

        public AsmType(Type type)
        {
            Name = type.Name;
            Fields = new List<Field>();
            Properties = new List<Property>();
            Methods = new List<Method>();
            Constructors = new List<Constructor>();
            var autoMethods = new List<MethodInfo>();

            IsNested = type.IsNested;
            IsSealed = type.IsSealed;

            TypeAttributes visibilityMask = type.Attributes & TypeAttributes.VisibilityMask;
//            switch (visibilityMask)
//            {
//                case TypeAttributes.Public:
//                    Modifier = AccessModifier.Public;
//                    break;
//                case TypeAttributes.NotPublic:
//                    Modifier = AccessModifier.Private;
//                    break;
//                case TypeAttributes.NestedAssembly:
//                    Modifier = AccessModifier.Internal;
//                    break;
//                case TypeAttributes.NestedFamily:
//                    Modifier = AccessModifier.Protected;
//                    break;
//            }
            if (visibilityMask == TypeAttributes.Public)
            {
                Modifier = AccessModifier.Public;
            }
            if (visibilityMask == TypeAttributes.NotPublic)
            {
                Modifier = AccessModifier.Private;
            }
            if (visibilityMask == TypeAttributes.NestedAssembly)
            {
                Modifier = AccessModifier.Internal;
            }
            if (visibilityMask == TypeAttributes.NestedFamily)
            {
                Modifier = AccessModifier.Protected;
            }

            TypeAttributes classSemantics = type.Attributes & TypeAttributes.ClassSemanticsMask;
            if (classSemantics == TypeAttributes.Class)
            {
                IsAbstract = (type.Attributes & TypeAttributes.Abstract) != 0;
                IsClass = type.IsClass;
                if (!IsClass)
                {
                    IsStructure = true;
                    IsEnum = type.IsEnum;
                }
            }

            if (classSemantics == TypeAttributes.Interface)
            {
                IsInterface = true;
                Modifier = AccessModifier.Public;
            }

            PropertyInfo[] properties = type.GetProperties(BindingFlags);
            foreach (var property in properties)
            {
                var assemblyProperty = new Property(property);
                autoMethods.AddRange(assemblyProperty.AutoMethods);
                Properties.Add(assemblyProperty);
            }

            FieldInfo[] fields = type.GetFields(BindingFlags);
            foreach (var field in fields)
            {
                if (field.Name.Contains("<")) continue;
                var assemblyField = new Field(field);
                Fields.Add(assemblyField);
            }

            MethodInfo[] methods = type.GetMethods(BindingFlags);
            foreach (var methodInfo in methods)
            {
                if (!autoMethods.Contains(methodInfo))
                {
                    var method = new Method(methodInfo);
                    Methods.Add(method);
                }
            }

            ConstructorInfo[] constructors = type.GetConstructors(BindingFlags);
            foreach (var constructorInfo in constructors)
            {
                var constructor = new Constructor(constructorInfo);
                Constructors.Add(constructor);
            }
        }
    }
}