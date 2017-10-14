using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MetaDslx.Languages.Soal.Generator.Config;

namespace MetaDslx.Languages.Soal.Generator.Java.Config
{
    class JavaTypeConfigConstants : ConfigConstants
    {
        public static readonly string XXX = "xxx";

        public override string CONFIG_PATH { get { return Path.Combine(System.IO.Directory.GetCurrentDirectory(), "java-type-config.txt"); } }
    }
}
