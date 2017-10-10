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
using MetaDslx.Languages.Soal.Generator.JavaEE.Maven;
using MetaDslx.Languages.Soal.Generator.JavaEE.JPA;
using MetaDslx.Languages.Soal.Generator.JavaSE;

namespace MetaDslx.Languages.Soal.Generator.JavaEE
{
    static class JavaEePrinter
    {
        public static void PrintPomXml(PomXmlIdentifier project, PomXmlIdentifier server, List<PomXmlIdentifier> dependencies, string root)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(root, "pom.xml")))
            {
                PomXmlGenerator pomGen = new PomXmlGenerator();
                writer.WriteLine(pomGen.Generate(project, server, dependencies));
            }
        }

        public static void PrintJpaEntity(Struct entity, string root, ImmutableModelList<Struct> entites)
        {
            string packageDirectory = Path.Combine(root, entity.Namespace.Name.ToLower());
            Directory.CreateDirectory(packageDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(packageDirectory, entity.Name + ".java")))
            {
                EntityGenerator javaGen = new EntityGenerator();
                writer.WriteLine(javaGen.Generate(entity, entites));
            }
        }

        public static void PrintJavaInterface(Interface iface, string root)
        {
            string packageDirectory = Path.Combine(root, iface.Namespace.Name.ToLower());
            Directory.CreateDirectory(packageDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(packageDirectory, iface.Name + ".java")))
            {
                InterfaceGenerator javaGen = new InterfaceGenerator();
                writer.WriteLine(javaGen.Generate(iface));
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
            string packageDirectory = Path.Combine(root, en.Namespace.Name.ToLower());
            Directory.CreateDirectory(packageDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(packageDirectory, en.Name + ".java")))
            {
                EntityGenerator javaGen = new EntityGenerator();
                writer.WriteLine(javaGen.GenerateEnum(en));
            }
        }

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
    }
}
