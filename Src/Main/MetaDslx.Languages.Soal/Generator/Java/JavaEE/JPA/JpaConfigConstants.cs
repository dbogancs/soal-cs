using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Generator.Config;

namespace MetaDslx.Languages.Soal.Generator.Java.JavaEE.JPA
{
    class JpaConfigConstants : ConfigConstants
    {
        public static readonly string PERSISTENCE_UNIT = "persistence_unit";
        public static readonly string PERSISTENCE_UNIT_PROVIDER = "persistence_unit_provider";

        public static readonly string URL_PROP_VALUE = "url_prop_value";
        public static readonly string USERNAME_PROP_VALUE = "username_prop_value";
        public static readonly string PASSWORD_PROP_VALUE = "password_prop_value";
        public static readonly string DRIVER_PROP_VALUE = "driver_prop_value";
        public static readonly string GENERATION_PROP_VALUE = "generation_prop_value";

        public static readonly string URL_PROP_NAME = "url_prop_name";
        public static readonly string USERNAME_PROP_NAME = "username_prop_name";
        public static readonly string PASSWORD_PROP_NAME = "password_prop_name";
        public static readonly string DRIVER_PROP_NAME = "driver_prop_name";
        public static readonly string GENERATION_PROP_NAME = "generation_prop_name";

        public override string CONFIG_PATH { get { return Path.Combine(System.IO.Directory.GetCurrentDirectory(), "jpa-config.txt"); } }
    }
}
