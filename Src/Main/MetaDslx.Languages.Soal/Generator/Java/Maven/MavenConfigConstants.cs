using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MetaDslx.Languages.Soal.Generator.Config;

namespace MetaDslx.Languages.Soal.Generator.Java.JavaEE.Config
{
    class MavenConfigConstants : ConfigConstants
    {
        public static readonly string PROJECT_VERSION = "project_version";

        public static readonly string SERVER_GROUPID = "server_groupid";
        public static readonly string SERVER_ARTIFACTID = "server_artifactid";
        public static readonly string SERVER_VERSION = "server_version";

        public static readonly string JUNIT_DEPENDENCY_GROUPID = "junit_dependency_groupid";
        public static readonly string JUNIT_DEPENDENCY_ARTIFACTID = "junit_dependency_artifactid";
        public static readonly string JUNIT_DEPENDENCY_VERSION = "junit_dependency_version";

        public static readonly string JPA_DEPENDENCY_GROUPID = "jpa_dependency_groupid";
        public static readonly string JPA_DEPENDENCY_ARTIFACTID = "jpa_dependency_arifactid";
        public static readonly string JPA_DEPENDENCY_VERSION = "jpa_dependency_version";

        public static readonly string EJB_DEPENDENCY_GROUPID = "ejb_dependency_groupid";
        public static readonly string EJB_DEPENDENCY_ARTIFACTID = "ejb_dependency_arifactid";
        public static readonly string EJB_DEPENDENCY_VERSION = "ejb_dependency_version";
        
        public override string CONFIG_PATH { get { return Path.Combine(System.IO.Directory.GetCurrentDirectory(), "maven-config.txt"); } }
    }
}
