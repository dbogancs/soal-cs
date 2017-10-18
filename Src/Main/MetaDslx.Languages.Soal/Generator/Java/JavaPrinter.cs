using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Symbols;
using System.IO;

namespace MetaDslx.Languages.Soal.Generator.Java
{
    class JavaPrinter
    {

        public static void PrintJavaInterface(Interface iface, string root, List<string> imports)
        {
            string packageDirectory = Path.Combine(root, JavaConventionHelper.packageConvention(iface.Namespace.Name));
            Directory.CreateDirectory(packageDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(packageDirectory, iface.Name + ".java")))
            {
                InterfaceGenerator javaGen = new InterfaceGenerator();
                writer.WriteLine(javaGen.Generate(iface, imports));
            }
        }

        public static void PrintEnum(Symbols.Enum en, string root)
        {
            string packageDirectory = Path.Combine(root, JavaConventionHelper.packageConvention(en.Namespace.Name));
            string enumDirectory = Path.Combine(packageDirectory, "enums");
            Directory.CreateDirectory(enumDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(enumDirectory, en.Name + ".java")))
            {
                EnumGenerator javaGen = new EnumGenerator();
                writer.WriteLine(javaGen.Generate(en));
            }
        }

        public static void PrintJavaException(Struct ex, string root)
        {
            string packageDirectory = Path.Combine(root, JavaConventionHelper.packageConvention(ex.Namespace.Name));
            string exceptionDirectory = Path.Combine(packageDirectory, "exceptions");
            Directory.CreateDirectory(exceptionDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(exceptionDirectory, ex.Name + ".java")))
            {
                ExceptionGenerator javaGen = new ExceptionGenerator();
                writer.WriteLine(javaGen.Generate(ex));
            }
        }
    }
}
