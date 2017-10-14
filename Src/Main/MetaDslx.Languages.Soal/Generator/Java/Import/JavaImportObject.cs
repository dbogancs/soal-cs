using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaDslx.Languages.Soal.Generator.Java.Import
{
    class JavaImportObject
    {
        public string userProjectName { get; set; }
        public string userPackageName { get; set; }
        public string userObjectName { get; set; }
        public string importPackageName { get; set; }
        public string importObjectName { get; set; }
        public bool isFileGenerationNeeded { get; set; }

        public JavaImportObject(string userProjectName, string userPackageName, string userObjectName, string importPackageName, string importObjectName, bool isFileGenerationNeeded)
        {
            this.userProjectName = userProjectName;
            this.userPackageName = userPackageName;
            this.userObjectName = userObjectName;
            this.importPackageName = importPackageName;
            this.importObjectName = importObjectName;
            this.isFileGenerationNeeded = isFileGenerationNeeded;
        }

        public bool Equals(JavaImportObject iobj)
        {
            return (userProjectName.Equals(iobj.userProjectName)
            && userPackageName.Equals(iobj.userPackageName)
            && userObjectName.Equals(iobj.userObjectName)
            && importPackageName.Equals(iobj.importPackageName)
            && importObjectName.Equals(iobj.importObjectName));
        }
    }
}
