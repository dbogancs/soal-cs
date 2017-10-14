using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Symbols;
using MetaDslx.Core;
using MetaDslx.Languages.Soal.Generator.Java.Config;

namespace MetaDslx.Languages.Soal.Generator.Java
{
    static class JavaConventionHelper
    {
        public static string classFullNamePackageConvention(string fullName)
        {
            string className = fullName.Split('.').Last();
            int splitIndex = fullName.LastIndexOf(".");
            return fullName.Substring(0, splitIndex).ToLower() + "." + className;
        }

        public static string classNameConvention(SoalType c)
        {
            ArrayType at = c as ArrayType;

            if (c.MName != null)
            {
                return JavaTypeConfigHandler.SwitchTypeName(c.MName);
            }
            else if (at.InnerType != null)
            {
                return JavaTypeConfigHandler.SwitchTypeName(at.MMetaClass.Name) + "<" + JavaTypeConfigHandler.SwitchTypeName(at.InnerType.MName) + ">";
            }
            else
            {
                return "UNKNOWN_TYPE";
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

        public static string packageConvention(string packageName)
        {
            return packageName.ToLower();
        }
    }
}

