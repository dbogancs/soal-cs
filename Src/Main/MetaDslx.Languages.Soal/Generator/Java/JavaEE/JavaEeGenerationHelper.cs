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
        
        public static void GenerateJpaProjectContent(Namespace ns, Component project, MavenProjectStructure mstruc)
        {
            Database db = null;
            foreach (var service in project.Services)
            {
                db = service.Interface as Database;
                if (db != null) break;
            }

            List<String> classes = new List<string>();
            if (db != null)
            {
                foreach (var entity in db.Entities)
                {
                    classes.Add(entity.FullName);
                    JavaEePrinter.PrintJpaEntity(entity, mstruc.mainJavaPath, db.Entities);
                }
                Directory.CreateDirectory(mstruc.METAINFPath);

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

                JavaEePrinter.PrintAllEnum(ns, mstruc.mainJavaPath);

                JavaEePrinter.PrintPersistenceXml
                    (
                    JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_PERSISTENCE_UNIT),
                    JavaEeTestConfigHandler.getValue(JavaEeTestConstants.DATABASE_PERSISTENCE_UNIT_PROVIDER),
                    classes,
                    proplist,
                    mstruc.METAINFPath
                    );
            }
        }

        public static void GeneratePomXml(string projectGroupId, string projectArtifactId, MavenProjectStructure paths)
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

                JavaEePrinter.PrintPomXml(project, server, dependencies, paths.projectPath);
            }
        }

        public static void GenerateMavenProject(Namespace ns, Composite application, Component project, string root)
        {
            MavenProjectStructure mstruc = new MavenProjectStructure(root, project.Name);

            if (project.Implementation.Name.Equals("JPA"))
                GenerateJpaProjectContent(ns, project, mstruc);

            GeneratePomXml(application.MName, project.MName, mstruc);
        }

        public static void GenerateJavaEe(Namespace ns, String outputDirectory)
        {
            foreach (var dec in ns.Declarations)
            {
                Composite composite = dec as Composite;
                if (composite != null)
                {
                    string parentRoot = Path.Combine(outputDirectory, composite.MName);
                    Directory.CreateDirectory(parentRoot);
                    
                    foreach (var component in composite.Components)
                    {
                        GenerateMavenProject(ns, composite, component, parentRoot);
                    }
                }
            }
        }
    }
}
