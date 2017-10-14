using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MetaDslx.Languages.Soal.Generator.Config;

namespace MetaDslx.Languages.Soal.Generator.Java.JavaEE.Config
{
    static class JavaEeConfigHandler
    {
        static private ConfigHandler handler = new ConfigHandler(new JavaEeConfigConstants(), "javaee-config");

        static public bool configOn { get { return handler.configOn; } }

        static public string getValue(string key)
        {
            return handler.GetValue(key);
        }
    }
}
