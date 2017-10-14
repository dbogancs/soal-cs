using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Symbols;
using MetaDslx.Languages.Soal.Generator.Java.Import;

namespace MetaDslx.Languages.Soal.Generator.Java
{
    class JavaImportCollector
    {
        private List<JavaImportObject> imports = new List<JavaImportObject>();
        
        public void CollectImportsFor(Component project)
        {
            imports.Clear();
            AnalyseProject(project);
        }

        private void AnalyseProject(Component project)
        {
            foreach (var attr in project.Properties)
            {
                AnalyseSoalType(project, project.Name, attr.Type);
            }

            foreach (var service in project.Services)
            {
                Database db = service.Interface as Database;
                if (db != null)
                {
                    foreach (var entity in db.Entities)
                    {
                        AnalyseSoalType(project, project.Name, entity);
                    }
                }

                foreach (var func in service.Interface.Operations)
                {
                    AnalyseSoalType(project, service.Interface.Name, func.Result.Type);
                    AnalyseSoalType(project, project.Name, func.Result.Type);

                    foreach (var attr in func.Parameters)
                    {
                        AnalyseSoalType(project, service.Interface.Name, attr.Type);
                        AnalyseSoalType(project, project.Name, attr.Type);
                    }

                    foreach (var ex in func.Exceptions)
                    {
                        AddImport(project.Name, project.Namespace.Name, service.Interface.Name,
                            JavaConventionHelper.packageConvention(project.Namespace.Name) + ".exceptions", ex.Name, isFileGenerationNeeded(project, ex));
                        AddImport(project.Name, project.Namespace.Name, project.Name,
                            JavaConventionHelper.packageConvention(project.Namespace.Name) + ".exceptions", ex.Name, isFileGenerationNeeded(project, ex));
                    }
                }
            }
        }

        private void AnalyseSoalType(Component project, string typeOwnerName, SoalType type)
        {
            Struct entity = type as Struct;
            ArrayType list = type as ArrayType;
            Symbols.Enum enumtype = type as Symbols.Enum;

            if (list != null)
            {
                entity = list.InnerType as Struct;
                AddImport(project.Name, project.Namespace.Name, typeOwnerName, "java.util", "List", false);
            }

            if (entity != null)
            {
                bool success = AddImport(project.Name, project.Namespace.Name, typeOwnerName,
                    JavaConventionHelper.packageConvention(project.Namespace.Name) + ".entities", entity.Name, isFileGenerationNeeded(project, entity));
                if (success)
                {
                    foreach (var attr in entity.Properties)
                    {
                        SoalType attrToType = attr.Type as SoalType;
                        Symbols.Enum attrToEnum = attr.Type as Symbols.Enum;

                        if (attrToEnum != null)
                        {
                            AddImport(project.Name, project.Namespace.Name, entity.Name,
                                JavaConventionHelper.packageConvention(project.Namespace.Name) + ".enums", attrToEnum.Name, isFileGenerationNeeded(project, attrToEnum));
                        }
                        else if (attrToType != null)
                        {
                            AnalyseSoalType(project, entity.Name, attrToType);
                        }
                    }
                }
            }
        }

        bool isFileGenerationNeeded(Component project, SoalType type)
        {
            foreach (var dec in project.Namespace.Declarations)
            {
                Struct obj = dec as Struct;
                if (obj != null)
                {
                    if (obj.Name.Equals(type.MName))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AddImport(string userProjectName, string userPackageName, string userObjectName, string importPackageName, string importObjectName, bool isFileGenerationNeeded)
        {
            JavaImportObject obj = new JavaImportObject(userProjectName, userPackageName, userObjectName, importPackageName, importObjectName, isFileGenerationNeeded);
            return AddImport(obj);
        }

        public bool AddImport(JavaImportObject iobj)
        {
            JavaImportObject objinlist = imports.Find(x => x.Equals(iobj));
            if (objinlist == null)
            {
                imports.Add(iobj);
                return true;
            }
            else { return false; }
        }

        public List<JavaImportObject> GetImportsForObject(string projectName, string packageName, string objectName)
        {
            return imports.ToList().FindAll(x =>
                x.userProjectName.Equals(projectName)
                && x.userPackageName.Equals(packageName)
                && x.userObjectName.Equals(objectName));
        }

        public List<string> GetImportPackagesForObject(string projectName, string packageName, string objectName)
        {
            List<JavaImportObject> list = imports.ToList().FindAll(x =>
                x.userProjectName.Equals(projectName)
                && x.userPackageName.Equals(packageName)
                && x.userObjectName.Equals(objectName));

            HashSet<string> packages = new HashSet<string>();
            foreach (var iobj in list) { packages.Add(iobj.importPackageName); }
            return packages.ToList();
        }

        public List<string> GetImportObjectsForObject(string projectName, string packageName, string objectName)
        {
            List<JavaImportObject> list = imports.ToList().FindAll(x =>
                x.userProjectName.Equals(projectName)
                && x.userPackageName.Equals(packageName)
                && x.userObjectName.Equals(objectName));

            HashSet<string> objects = new HashSet<string>();
            foreach (var iobj in list) { objects.Add(iobj.importObjectName); }
            return objects.ToList();
        }

        public List<string> GetFullImportObjectNamesForObject(string projectName, string packageName, string objectName)
        {
            List<JavaImportObject> list = imports.ToList().FindAll(x =>
                x.userProjectName.Equals(projectName)
                && x.userPackageName.Equals(packageName)
                && x.userObjectName.Equals(objectName));

            HashSet<string> fullnames = new HashSet<string>();
            foreach (var iobj in list) { fullnames.Add(iobj.importPackageName + "." + iobj.importObjectName); }
            return fullnames.ToList();
        }

        public List<JavaImportObject> GetImportsForProject(string projectName)
        {
            return imports.ToList().FindAll(x =>
                x.userProjectName.Equals(projectName));
        }

        public List<string> GetImportPackagesForProject(string projectName)
        {
            List<JavaImportObject> list = imports.ToList().FindAll(x =>
                x.userProjectName.Equals(projectName));

            HashSet<string> packages = new HashSet<string>();
            foreach (var iobj in list) { packages.Add(iobj.importPackageName); }
            return packages.ToList();
        }

        public List<string> GetImportObjectsForProject(string projectName)
        {
            List<JavaImportObject> list = imports.ToList().FindAll(x =>
                x.userProjectName.Equals(projectName));

            HashSet<string> objects = new HashSet<string>();
            foreach (var iobj in list) { objects.Add(iobj.importObjectName); }
            return objects.ToList();
        }

        public List<string> GetFullImportObjectNamesForProject(string projectName)
        {
            List<JavaImportObject> list = imports.ToList().FindAll(x =>
                x.userProjectName.Equals(projectName));

            HashSet<string> fullnames = new HashSet<string>();
            foreach (var iobj in list) { fullnames.Add(iobj.importPackageName + "." + iobj.importObjectName); }
            return fullnames.ToList();
        }
    }
}
