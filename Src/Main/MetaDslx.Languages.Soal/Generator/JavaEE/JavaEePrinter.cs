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
using MetaDslx.Languages.Soal.Generator.JavaEE.Pom;
using MetaDslx.Languages.Soal.Generator.JavaEE.Persistence;

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
            string nsDirectory = Path.Combine(root, entity.Namespace.Name.ToLower());
            Directory.CreateDirectory(nsDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(nsDirectory, entity.Name + ".java")))
            {
                JavaEeGenerator javaGen = new JavaEeGenerator();
                writer.WriteLine(javaGen.GenerateEntity(entity, entites));
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
            string nsDirectory = Path.Combine(root, en.Namespace.Name.ToLower());
            Directory.CreateDirectory(nsDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(nsDirectory, en.Name + ".java")))
            {
                JavaEeGenerator javaGen = new JavaEeGenerator();
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
