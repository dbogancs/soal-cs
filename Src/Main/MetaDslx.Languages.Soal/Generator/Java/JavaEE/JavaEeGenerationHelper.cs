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
using MetaDslx.Languages.Soal.Generator.Java;
using MetaDslx.Languages.Soal.Generator.Java.Maven;
using MetaDslx.Languages.Soal.Generator.Java.JavaEE.JPA;
using MetaDslx.Languages.Soal.Generator.Java.JavaEE.Test;

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
                    List<string> importlist = imports.GetImportsFor(project.Name, entity.Namespace.Name, entity.Name);
                    classes.Add(entity.FullName);
                    JavaEePrinter.PrintJpaEntity(entity, mstruc.mainJavaPath, db.Entities.ToList(), importlist);
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

                //JavaEePrinter.PrintAllEnum(ns, mstruc.mainJavaPath);

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

        public static void GenerateEjbProjectContent(Component project, MavenProjectStructure paths, JavaImportCollector imports)
        {
            List<string> importlist = null;

            foreach (var service in project.Services)
            {
                importlist = imports.GetImportsFor(project.Name, service.Interface.Namespace.Name, service.Interface.Name);
                JavaEePrinter.PrintJavaInterface(service.Interface, paths.mainJavaPath, importlist);
            }
            importlist = imports.GetImportsFor(project.Name, project.Namespace.Name, project.Name);
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
                List<string> importlist = imports.GetImportsFor(project.Name, service.Interface.Namespace.Name, service.Interface.Name);
                JavaEePrinter.PrintJavaInterface(service.Interface, mstruc.mainJavaPath, importlist);
            }
        }

        public static void GenerateUsedComponents(Component project, MavenProjectStructure mstruc, JavaImportCollector imports)
        {
            HashSet<Struct> usedEntities = new HashSet<Struct>();
            HashSet<Symbols.Enum> usedEnums = new HashSet<Symbols.Enum>();
            HashSet<Struct> usedExceptions = new HashSet<Struct>();

            foreach (var attr in project.Properties)
            {
                FindUsedTypes(project, project.Name, attr.Type, usedEntities, usedEnums, imports);
            }



            foreach (var service in project.Services)
            {
                Database db = service.Interface as Database;
                if (db != null)
                {
                    foreach (var entity in db.Entities)
                    {
                        FindUsedTypes(project, project.Name, entity, usedEntities, usedEnums, imports);
                    }
                }

                foreach (var func in service.Interface.Operations)
                {
                    FindUsedTypes(project, service.Interface.Name, func.Result.Type, usedEntities, usedEnums, imports);
                    FindUsedTypes(project, project.Name, func.Result.Type, usedEntities, usedEnums, imports);

                    foreach (var attr in func.Parameters)
                    {
                        FindUsedTypes(project, service.Interface.Name, attr.Type, usedEntities, usedEnums, imports);
                        FindUsedTypes(project, project.Name, attr.Type, usedEntities, usedEnums, imports);
                    }

                    foreach (var ex in func.Exceptions)
                    {
                        usedExceptions.Add(ex as Struct);
                        imports.AddImportFor(project.Name, project.Namespace.Name, service.Interface.Name,
                            JavaConventionHelper.packageConvention(project.Namespace.Name) + ".exceptions." + ex.Name);
                        imports.AddImportFor(project.Name, project.Namespace.Name, project.Name,
                            JavaConventionHelper.packageConvention(project.Namespace.Name) + ".exceptions." + ex.Name);
                    }
                }
            }

            foreach (var entity in usedEntities)
            {
                List<string> importlist = imports.GetImportsFor(project.Name, entity.Namespace.Name, entity.Name);
                JavaEePrinter.PrintJpaEntity(entity, mstruc.mainJavaPath, usedEntities.ToList(), importlist);
            }

            foreach (var enumtype in usedEnums)
            {
                JavaEePrinter.PrintEnum(enumtype, mstruc.mainJavaPath);
            }

            foreach (var ex in usedExceptions)
            {
                JavaEePrinter.PrintJavaException(ex, mstruc.mainJavaPath);
            }
        }

        private static void FindUsedTypes(Component project, string typeOwnerName, SoalType type, HashSet<Struct> structSet, HashSet<Symbols.Enum> enumSet, JavaImportCollector imports)
        {
            Struct entity = type as Struct;
            ArrayType list = type as ArrayType;
            Symbols.Enum enumtype = type as Symbols.Enum;

            if (list != null)
            {
                entity = list.InnerType as Struct;
                imports.AddImportFor(project.Name, project.Namespace.Name, typeOwnerName, "java.util.List");
            }

            if (entity != null)
            {
                structSet.Add(entity);
                bool success = imports.AddImportFor(project.Name, project.Namespace.Name, typeOwnerName,
                    JavaConventionHelper.packageConvention(project.Namespace.Name) + ".entities."+entity.Name);
                if (success)
                {
                    foreach (var attr in entity.Properties)
                    {
                        SoalType attrToType = attr.Type as SoalType;
                        Symbols.Enum attrToEnum = attr.Type as Symbols.Enum;

                        if (attrToEnum != null)
                        {
                            enumSet.Add(attrToEnum);
                            imports.AddImportFor(project.Name, project.Namespace.Name, entity.Name,
                                JavaConventionHelper.packageConvention(project.Namespace.Name) + ".enums." + attrToEnum.Name);
                        }
                        else if(attrToType != null)
                        {
                            FindUsedTypes(project, entity.Name, attrToType, structSet, enumSet, imports);
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
            GenerateUsedComponents(project, mstruc, imports);

            GenerateJavaInterfaces(project, mstruc, imports);

            if (project.Implementation.Name.Equals("JPA"))
            { GenerateJpaProjectContent(project, mstruc, imports); }

            if (project.Implementation.Name.Equals("EJB"))
            { GenerateEjbProjectContent(project, mstruc, imports); }


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
