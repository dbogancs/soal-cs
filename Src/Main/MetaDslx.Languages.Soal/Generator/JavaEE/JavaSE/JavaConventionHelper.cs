using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Symbols;
using MetaDslx.Core;

namespace MetaDslx.Languages.Soal.Generator.JavaSE
{
    static class JavaConventionHelper
    {
        public static String classFullNamePackageConvention(String fullName)
        {
            String className = fullName.Split('.').Last();
            int splitIndex = fullName.LastIndexOf(".");
            return fullName.Substring(0, splitIndex).ToLower() + "." + className;
        }

        public static String classNameConvention(SoalType c)
        {
            if (c.MName != null)
            {
                if (c.MName.Equals("TimeSpan"))
                {
                    return "Date";
                }
                else if (c.MName.Equals("string"))
                {
                    return "String";
                }
                else if (c.MName.Equals("DateTime"))
                {
                    return "Date";
                }
                else
                {
                    return c.MName;
                }
            }
            else
            {
                ArrayType at = (ArrayType)c;
                if (at.MMetaClass.Name.Equals("ArrayType") && at.InnerType != null && !at.InnerType.MName.Equals(null))
                {
                    return "List<" + at.InnerType.MName + ">";
                }
                else
                {
                    return "";
                }
            }


        }

        public static string attributeNameConvention(String attributeName)
        {
            return Char.ToLowerInvariant(attributeName[0]) + attributeName.Substring(1);
        }

        public static string databaseColumnNameConvention(String columnName)
        {
            return columnName.ToUpper();
        }

        public static string methodNameConvention(string methodName)
        {
            return Char.ToLowerInvariant(methodName[0]) + methodName.Substring(1);
        }
    }
}

