using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Compiler.Diagnostics;
using MetaDslx.Compiler.Symbols;
using MetaDslx.Compiler.Syntax;
using MetaDslx.Core;
using MetaDslx.Languages.Soal.Generator;
using MetaDslx.Languages.Soal.Symbols;
using System.Collections.Immutable;
using System.IO;
using MetaDslx.Languages.Soal.Generator.Java.Maven;
using MetaDslx.Languages.Soal.Generator.Java.JavaEE.JPA;
using MetaDslx.Languages.Soal.Generator.Java.JavaEE.EJB;

namespace MetaDslx.Languages.Soal.Generator.Java.JavaEE
{
    static class JavaEePrinter
    {
        public static void PrintPomXml(PomXmlIdentifier project, List<PomXmlIdentifier> dependencies, List<PomXmlIdentifier> plugins, string root)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(root, "pom.xml")))
            {
                PomXmlGenerator pomGen = new PomXmlGenerator();
                writer.WriteLine(pomGen.Generate(project, dependencies, plugins));
            }
        }

        public static void PrintJpaEntity(Struct entity, string root, List<Struct> entites, List<string> imports)
        {
            string packageDirectory = Path.Combine(root, JavaConventionHelper.packageConvention(entity.Namespace.Name));
            string entityDirectory = Path.Combine(packageDirectory, "entities");
            Directory.CreateDirectory(entityDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(entityDirectory, entity.Name + ".java")))
            {
                EntityGenerator javaGen = new EntityGenerator();
                writer.WriteLine(javaGen.Generate(entity, entites, imports));
            }
        }

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

        public static void PrintEjb(Component project, string root, List<string> imports)
        {
            string packageDirectory = Path.Combine(root, JavaConventionHelper.packageConvention(project.Namespace.Name));
            Directory.CreateDirectory(packageDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(packageDirectory, project.Name + ".java")))
            {
                EjbGenerator javaGen = new EjbGenerator();
                writer.WriteLine(javaGen.Generate(project, imports));
            }
        }

        public static void PrintPersistenceXml(string persistenceUnit, string persistenceUnitProvider, IEnumerable<string> classes, List<PersistenceXmlProperty> persistenceUnitPropertyList, string root)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(root, "persistence.xml")))
            {
                PersistenceXmlGenerator persGen = new PersistenceXmlGenerator();

                writer.WriteLine(persGen.Generate(persistenceUnit, persistenceUnitProvider, classes, persistenceUnitPropertyList));
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

        /*
        public static void PrintAllEnum(Namespace ns, string root)
        {
            foreach (var decl in ns.Declarations)
            {
                if (decl.MMetaClass.Name.Equals("Enum"))
                {
                    var en = decl as Symbols.Enum;
                    JavaEePrinter.PrintEnum(en, root);
                }
            }
        }
        */

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
