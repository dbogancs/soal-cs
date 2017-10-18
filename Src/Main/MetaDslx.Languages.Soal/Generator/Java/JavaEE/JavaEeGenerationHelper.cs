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
using MetaDslx.Languages.Soal.Generator.Java.Import;
using MetaDslx.Languages.Soal.Generator.Java.Maven;
using MetaDslx.Languages.Soal.Generator.Java.JavaEE.JPA;
using MetaDslx.Languages.Soal.Generator.Java.JavaEE.Config;

namespace MetaDslx.Languages.Soal.Generator.Java.JavaEE
{
    static class JavaEeGenerationHelper
    {
        
        public static void GenerateJpaProjectContent(Component project, MavenProjectStructure mstruc, JavaImportCollector imports)
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
                    List<string> importlist = imports.GetFullImportObjectNamesForObject(project.Name, entity.Namespace.Name, entity.Name);
                    classes.Add(entity.FullName);
                    JavaEePrinter.PrintJpaEntity(entity, mstruc.mainJavaPath, db.Entities.ToList(), importlist);
                }
                Directory.CreateDirectory(mstruc.METAINFPath);

                List<PersistenceXmlProperty> proplist = new List<PersistenceXmlProperty>();
                if (JpaConfigHandler.configOn)
                {
                    proplist.Add(new PersistenceXmlProperty(
                        JpaConfigHandler.getValue(JpaConfigConstants.URL_PROP_NAME),
                        JpaConfigHandler.getValue(JpaConfigConstants.URL_PROP_VALUE)));
                    proplist.Add(new PersistenceXmlProperty(
                        JpaConfigHandler.getValue(JpaConfigConstants.USERNAME_PROP_NAME),
                        JpaConfigHandler.getValue(JpaConfigConstants.USERNAME_PROP_VALUE)));
                    proplist.Add(new PersistenceXmlProperty(
                        JpaConfigHandler.getValue(JpaConfigConstants.PASSWORD_PROP_NAME),
                        JpaConfigHandler.getValue(JpaConfigConstants.PASSWORD_PROP_VALUE)));
                    proplist.Add(new PersistenceXmlProperty(
                        JpaConfigHandler.getValue(JpaConfigConstants.DRIVER_PROP_NAME),
                        JpaConfigHandler.getValue(JpaConfigConstants.DRIVER_PROP_VALUE)));
                    proplist.Add(new PersistenceXmlProperty(
                        JpaConfigHandler.getValue(JpaConfigConstants.GENERATION_PROP_NAME),
                        JpaConfigHandler.getValue(JpaConfigConstants.GENERATION_PROP_VALUE)));
                }

                //JavaEePrinter.PrintAllEnum(ns, mstruc.mainJavaPath);

                JavaEePrinter.PrintPersistenceXml
                    (
                    JpaConfigHandler.getValue(JpaConfigConstants.PERSISTENCE_UNIT),
                    JpaConfigHandler.getValue(JpaConfigConstants.PERSISTENCE_UNIT_PROVIDER),
                    classes,
                    proplist,
                    mstruc.METAINFPath
                );
            }
        }

        public static void GenerateEjbProjectContent(Component project, MavenProjectStructure paths, JavaImportCollector imports)
        {
            List<string> importlist = null;

            foreach (var service in project.Services)
            {
                importlist = imports.GetFullImportObjectNamesForObject(project.Name, service.Interface.Namespace.Name, service.Interface.Name);
                JavaEePrinter.PrintJavaInterface(service.Interface, paths.mainJavaPath, importlist);
            }
            importlist = imports.GetFullImportObjectNamesForObject(project.Name, project.Namespace.Name, project.Name);
            JavaEePrinter.PrintEjb(project, paths.mainJavaPath, importlist);
        }

        public static void GeneratePomXml(PomXmlIdentifier projectInfo, List<PomXmlIdentifier> dependencies, List<PomXmlIdentifier> plugins, MavenProjectStructure paths)
        {
            JavaEePrinter.PrintPomXml(projectInfo, dependencies, plugins, paths.projectPath);
        }

        public static void GenerateJavaInterfaces(Component project, MavenProjectStructure mstruc, JavaImportCollector imports)
        {
            foreach (var service in project.Services)
            {
                List<string> importlist = imports.GetFullImportObjectNamesForObject(project.Name, service.Interface.Namespace.Name, service.Interface.Name);
                JavaEePrinter.PrintJavaInterface(service.Interface, mstruc.mainJavaPath, importlist);
            }
        }

        public static void GenerateUsedObjects(Component project, MavenProjectStructure mstruc, JavaImportCollector imports)
        {
            var iobjList = imports.GetImportsForProject(project.Name);

            foreach (var iobj in iobjList)
            {
                foreach (var dec in project.Namespace.Declarations)
                {
                    if (dec.Name.Equals(iobj.importObjectName))
                    {
                        if (dec.Name.Contains("Exception"))
                        {
                            var ex = dec as Struct;
                            if (ex != null) { JavaEePrinter.PrintJavaException(ex, mstruc.mainJavaPath); }
                        }
                        else if (dec.MMetaClass.Name.Equals("Enum"))
                        {
                            var en = dec as Symbols.Enum;
                            if(en != null) { JavaEePrinter.PrintEnum(en, mstruc.mainJavaPath); }
                        }
                        else
                        {
                            var st = dec as Struct;
                            if (st != null) {
                                List<string> importlist = imports.GetFullImportObjectNamesForObject(project.Name, project.Namespace.Name, st.Name);
                                JavaEePrinter.PrintJpaEntity(st, mstruc.mainJavaPath, new List<Struct>(), importlist);
                            }
                        }
                    }
                }
            }
            
        }

        public static void GenerateMavenProject(Namespace ns, Composite application, Component project, string root)
        {
            MavenProjectStructure mstruc = new MavenProjectStructure(root, project.Name);
            mstruc.CreateMavenStructure();

            JavaImportCollector imports = new JavaImportCollector();
            imports.CollectImportsFor(project);

            GenerateJavaInterfaces(project, mstruc, imports);

            if (project.Implementation.Name.Equals("JPA"))
            { GenerateJpaProjectContent(project, mstruc, imports); }

            if (project.Implementation.Name.Equals("EJB"))
            { GenerateEjbProjectContent(project, mstruc, imports); }

            GenerateUsedObjects(project, mstruc, imports);

            GeneratePomXml(
                PomXmlIdentifierHandler.GetProjectIdentifier(application.MName, project),
                PomXmlIdentifierHandler.GetProjectDependences(project),
                new List<PomXmlIdentifier>(),
                mstruc
            );
        }

        public static void GenerateJavaEe(Namespace ns, string outputDirectory)
        {
            foreach (var dec in ns.Declarations)
            {
                Composite composite = dec as Composite;
                if (composite != null)
                {
                    string parentRoot = Path.Combine(outputDirectory, composite.MName);
                    Directory.CreateDirectory(parentRoot);
                    
                    foreach (var project in composite.Components)
                    {
                        GenerateMavenProject(ns, composite, project, parentRoot);
                    }
                }
            }
        }
    }
}
