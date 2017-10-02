using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace MetaDslx.Languages.Soal.Generator.Java
{
    static class JavaEeTestConstants
    {
        public static readonly string TEST_ON = "test_on";


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

        public static readonly string DATABASE_PERSISTENCE_UNIT = "database_persistence_unit";
        public static readonly string DATABASE_URL = "database_url";
        public static readonly string DATABASE_USERNAME = "database_username";
        public static readonly string DATABASE_PASSWORD = "database_password";


        public static readonly string TRUE = "true";
        public static readonly string CONFIG_PATH = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "javaee-test-config.txt");
    }
}
