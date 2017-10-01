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

namespace MetaDslx.Languages.Soal.Generator.Java
{
    static class JavaEeGenerationHelper
    {

        public static readonly String JPA = "JPA";
        public static readonly String COMPOSITE = "Composite";


        public static void CreateMavenParentProject(String root, String parentName)
        {
            string parentRoot = Path.Combine(root, parentName);
            Directory.CreateDirectory(parentRoot);
        }

        public static void CreateMavenProject(String root, String projectName)
        {
            string projectRoot = Path.Combine(root, projectName);
            string srcRoot = Path.Combine(projectRoot, "src");
            string targetRoot = Path.Combine(projectRoot, "target");
            string mainRoot = Path.Combine(srcRoot, "main");
            string testRoot = Path.Combine(srcRoot, "test");
            string mainJava = Path.Combine(mainRoot, "java");
            string mainResources = Path.Combine(mainRoot, "resources");
            string testJava = Path.Combine(testRoot, "java");
            string testResources = Path.Combine(testRoot, "resources");
            Directory.CreateDirectory(projectRoot);
            Directory.CreateDirectory(srcRoot);
            Directory.CreateDirectory(targetRoot);
            Directory.CreateDirectory(mainRoot);
            Directory.CreateDirectory(mainJava);
            Directory.CreateDirectory(mainResources);
            Directory.CreateDirectory(testRoot);
            Directory.CreateDirectory(testJava);
            Directory.CreateDirectory(testResources);
        }

        public static void PrintPomXml(Composite composite, Component component, String root)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(root, "pom.xml")))
            {
                PomXmlIdentifier project = new PomXmlIdentifier();
                project.groupId = composite.MName;
                project.artifactId = component.MName;
                project.version = "1.0";
                PomXmlIdentifier server = null;
                //PomXmlIdentifier = new List<String>();
                //server.groupId = "org.wildfly.plugins";
                //server.artifactId = "wildfly-javaee7-with-tools";
                //server.version = "10.1.0.Final";
                List<PomXmlIdentifier> dependencies = new List<PomXmlIdentifier>();
                PomXmlIdentifier junitDep = new PomXmlIdentifier();
                junitDep.groupId = "junit";
                junitDep.artifactId = "junit";
                junitDep.version = "3.8.1";
                PomXmlIdentifier jpaDep = new PomXmlIdentifier();
                jpaDep.groupId = "org.hibernate.javax.persistence";
                jpaDep.artifactId = "hibernate-jpa-2.0-api";
                jpaDep.version = "1.0.1.Final";
                
                dependencies.Add(junitDep);
                dependencies.Add(jpaDep);
                PomXmlGenerator pomGen = new PomXmlGenerator();
                writer.WriteLine(pomGen.Generate(project, server,dependencies));
            }
        }

        public static void PrintJpaEntity(Struct entity, String root, ImmutableModelList<Struct> entites)
        {
            String nsDirectory = Path.Combine(root, entity.Namespace.Name.ToLower());
            Directory.CreateDirectory(nsDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(nsDirectory, entity.Name + ".java")))
            {
                JavaEeGenerator javaGen = new JavaEeGenerator();
                writer.WriteLine(javaGen.GenerateEntity(entity, entites));
            }
        }

        public static void PrintPersistenceXml(String persistenceUnit, IEnumerable<String> classes, String url, String username, String password, String root)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(root, "persistence.xml")))
            {
                PersistenceXmlGenerator persGen = new PersistenceXmlGenerator();
                writer.WriteLine(persGen.Generate(persistenceUnit, classes, url, username, password));
            }
        }

        public static void PrintEnum(MetaDslx.Languages.Soal.Symbols.Enum en, String root)
        {
            String nsDirectory = Path.Combine(root, en.Namespace.Name.ToLower());
            Directory.CreateDirectory(nsDirectory);
            using (StreamWriter writer = new StreamWriter(Path.Combine(nsDirectory,en.Name + ".java")))
            {
                JavaEeGenerator javaGen = new JavaEeGenerator();
                writer.WriteLine(javaGen.GenerateEnum(en));
            }
        }

        public static void GenerateJavaEe(Namespace ns, String outputDirectory)
        {
            foreach (var dec in ns.Declarations)
            {
                Composite composite = dec as Composite;
                if (composite != null)
                {
                    CreateMavenParentProject(outputDirectory, composite.MName);
                    String parentRoot = Path.Combine(outputDirectory, composite.MName);
                    foreach (var component in composite.Components)
                    {
                        CreateMavenProject(parentRoot, component.Name);
                        String currentProject = Path.Combine(parentRoot, component.Name);
                        String currentProjectSrc = Path.Combine(currentProject, "src");
                        String currentProjectMain = Path.Combine(currentProjectSrc, "main");
                        String currentProjectJava = Path.Combine(currentProjectMain, "java");
                        String currentProjectResources = Path.Combine(currentProjectMain, "resources");
                        String currentProjectMETAINF = Path.Combine(currentProjectResources, "META_INF");
                        foreach (var service in component.Services)
                        {
                            foreach (var decl in ns.Declarations)
                            {
                                if(decl.MMetaClass.Name.Equals("Enum"))
                                {
                                    var en = decl as MetaDslx.Languages.Soal.Symbols.Enum;
                                    PrintEnum(en,currentProjectJava);
                                }
                            }

                            if (component.Implementation.MName.Equals(JPA))
                            {
                                Database db = service.Interface as Database;
                                List<String> classes = new List<string>();
                                if (db != null)
                                {
                                    foreach (var entity in db.Entities)
                                    {
                                        classes.Add(entity.FullName);
                                        PrintJpaEntity(entity, currentProjectJava, db.Entities);
                                    }
                                    Directory.CreateDirectory(currentProjectMETAINF);
                                    PrintPersistenceXml("SampleDBPU", classes, "jdbc: derby://localhost:1527/cinema_database","username","password", currentProjectMETAINF);
                                }
                            }

                        }
                        PrintPomXml(composite, component, currentProject);
                    }
                }
            }
        }
    }
}
