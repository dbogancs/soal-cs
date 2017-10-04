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

        public static void CreateMavenParentProject(String root, String parentName)
        {
            string parentRoot = Path.Combine(root, parentName);
            Directory.CreateDirectory(parentRoot);
        }

        public static void CreateMavenStructure(String root, String projectName)
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

        public static void PrintPomXml(PomXmlIdentifier project, PomXmlIdentifier server, List<PomXmlIdentifier> dependencies, String root)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(root, "pom.xml")))
            {
                PomXmlGenerator pomGen = new PomXmlGenerator();
                writer.WriteLine(pomGen.Generate(project, server, dependencies));
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

        public static void PrintPersistenceXml(String persistenceUnit, String persistenceUnitProvider, IEnumerable<String> classes, List<PersistenceXmlProperty> persistenceUnitPropertyList, String root)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(root, "persistence.xml")))
            {
                PersistenceXmlGenerator persGen = new PersistenceXmlGenerator();

                writer.WriteLine(persGen.Generate(persistenceUnit, persistenceUnitProvider, classes, persistenceUnitPropertyList));
            }
        }

        public static void PrintEnum(MetaDslx.Languages.Soal.Symbols.Enum en, String root)
        {
            String nsDirectory = Path.Combine(root, en.Namespace.Name.ToLower());
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
                    PrintEnum(en, root);
                }
            }
        }


        public static void GenerateJpaProject(Database db, string currentProjectJava, string currentProjectMETAINF)
        {
            List<String> classes = new List<string>();
            if (db != null)
            {
                foreach (var entity in db.Entities)
                {
                    classes.Add(entity.FullName);
                    PrintJpaEntity(entity, currentProjectJava, db.Entities);
                }
                Directory.CreateDirectory(currentProjectMETAINF);

                List<PersistenceXmlProperty> proplist = new List<PersistenceXmlProperty>();
                if (JavaEeTestConfigHandler.testOn)
                {
                    proplist.Add(new PersistenceXmlProperty(
                        JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_URL_PROP_NAME),
                        JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_URL_PROP_VALUE)));
                    proplist.Add(new PersistenceXmlProperty(
                        JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_USERNAME_PROP_NAME),
                        JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_USERNAME_PROP_VALUE)));
                    proplist.Add(new PersistenceXmlProperty(
                        JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_PASSWORD_PROP_NAME),
                        JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_PASSWORD_PROP_VALUE)));
                    proplist.Add(new PersistenceXmlProperty(
                        JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_DRIVER_PROP_NAME),
                        JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_DRIVER_PROP_VALUE)));
                    proplist.Add(new PersistenceXmlProperty(
                        JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_GENERATION_PROP_NAME),
                        JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_GENERATION_PROP_VALUE)));
                }

                PrintPersistenceXml
                    (
                    JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_PERSISTENCE_UNIT),
                    JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_PERSISTENCE_UNIT_PROVIDER),
                    classes,
                    proplist,
                    currentProjectMETAINF
                    );
            }
        }

        public static void GeneratePomXml(string projectGroupId, string projectArtifactId, string root)
        {
            PomXmlIdentifier project = null;
            PomXmlIdentifier server = null;
            List<PomXmlIdentifier> dependencies = new List<PomXmlIdentifier>();

            if (JavaEeTestConfigHandler.testOn)
            {
                project = new PomXmlIdentifier();
                project.groupId = projectGroupId;
                project.artifactId = projectArtifactId;
                project.version = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.PROJECT_VERSION);

                //server = new PomXmlIdentifier();
                //server.groupId = "org.wildfly.plugins";
                //server.artifactId = "wildfly-javaee7-with-tools";
                //server.version = "10.1.0.Final";

                PomXmlIdentifier junitDep = new PomXmlIdentifier();
                junitDep.groupId = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JUNIT_DEPENDENCY_GROUPID);
                junitDep.artifactId = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JUNIT_DEPENDENCY_ARTIFACTID);
                junitDep.version = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JUNIT_DEPENDENCY_VERSION);

                PomXmlIdentifier jpaDep = new PomXmlIdentifier();
                jpaDep.groupId = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JPA_DEPENDENCY_GROUPID);
                jpaDep.artifactId = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JPA_DEPENDENCY_ARTIFACTID);
                jpaDep.version = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JPA_DEPENDENCY_VERSION);

                dependencies.Add(junitDep);
                dependencies.Add(jpaDep);

                PrintPomXml(project, server, dependencies, root);
            }
        }

        public static void GenerateMavenProject(Namespace ns, Composite application, Component project, string root)
        {
            CreateMavenStructure(root, project.Name);
            String currentProject = Path.Combine(root, project.Name);
            String currentProjectSrc = Path.Combine(currentProject, "src");
            String currentProjectMain = Path.Combine(currentProjectSrc, "main");
            String currentProjectJava = Path.Combine(currentProjectMain, "java");
            String currentProjectResources = Path.Combine(currentProjectMain, "resources");
            String currentProjectMETAINF = Path.Combine(currentProjectResources, "META_INF");

            foreach (var service in project.Services)
            {
                PrintAllEnum(ns, currentProjectJava);
                
                if (project.Implementation.MName.Equals("JPA"))
                {
                    Database db = service.Interface as Database;
                    if(db != null) GenerateJpaProject(db, currentProjectJava, currentProjectMETAINF);
                }

            }

            GeneratePomXml(application.MName, project.MName, currentProject);
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
                        GenerateMavenProject(ns, composite, component, parentRoot);
                    }
                }
            }
        }
    }
}
