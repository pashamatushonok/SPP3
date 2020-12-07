using AssemblyBrowser;
using AssemblyBrowser.Enums;
using AssemblyBrowser.TypeComponents;

namespace WpfAppAssemblyBrowser.Model
{
    public class Converter
    {
        public static string ConvertToString(Field field)
        {
            var result = field.Modifier.ToString().ToLower() + " ";

            if (field.IsStatic)
            {
                result += "static ";
            }

            if (field.IsReadonly)
            {
                result += "readonly ";
            }

            result += field.ValueType.Name + " ";

            return result + field.Name;
        }

        public static string ConvertToString(Property property)
        {
            var result = property.Modifier.ToString().ToLower() + " ";

            if (property.IsVirtual && !property.IsAbstract)
            {
                result += "virtual ";
            }
            else if (property.IsVirtual && property.IsAbstract)
            {
                result += "abstract ";
            }

            if (property.IsStatic)
            {
                result += "static ";
            }

            result += property.ValueType.Name + " ";
            result += property.Name;

            result += "{ ";

            if (property.CanRead)
            {
                result += property.GetterModifier.ToString().ToLower() + " get;";
            }

            if (property.CanWrite)
            {
                result += property.SetterModifier.ToString().ToLower() + " set;";
            }

            result += " }";

            return result;
        }

        public static string ConvertToString(AsmType type)
        {
            var result = type.Modifier.ToString().ToLower() + " ";

            if (type.IsSealed && !type.IsStructure && !type.IsEnum)
            {
                result += "sealed ";
            }

            if (type.IsAbstract)
            {
                result += "abstract ";
            }

            if (type.IsEnum)
            {
                result += "enum ";
            }
            else if (type.IsStructure)
            {
                result += "structure ";
            }
            else if (type.IsInterface)
            {
                result = "interface " + type.Name;
                return result;
            }
            else if (type.IsClass)
            {
                result += "class ";
            }

            return result + type.Name;
        }

        public static string ConvertToString(Method method)
        {
            var result = method.Modifier.ToString().ToLower() + " ";

            if (method.IsVirtual && !method.IsAbstract)
            {
                result += "virtual ";
            }
            else if (method.IsVirtual && method.IsAbstract)
            {
                result += "abstract ";
            }

            result += method.ValueType.Name + " " + method.Name + " (";
            foreach (Parameter param in method.Parameters)
            {
                var parametersString = param.PassingType.ToString().ToLower();
                parametersString += param.ValueType.Name + ",";
                result += parametersString;
            }

            if (result[result.Length - 1] == ',')
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result + ")";
        }

        public static string ConvertToString(Constructor constructor)
        {
            var result = constructor.Modifier.ToString().ToLower() + " " + Constructor.Name + "(";

            foreach (Parameter param in constructor.Parameters)
            {
                var parametersString = param.PassingType.ToString().ToLower();
                parametersString += param.ValueType.Name + ",";
                result += parametersString;
            }

            if (result[result.Length - 1] == ',')
            {
                result = result.Substring(0, result.Length - 1);
            }

            return result + ")";
        }
    }
}