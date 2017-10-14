using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Generator.Config;

namespace MetaDslx.Languages.Soal.Generator.Java.Import
{
    static class JavaImportConfigHandler
    {
        static private ConfigHandler handler = new ConfigHandler(new JavaImportConfigConstants(), "java-import-config");

        static public bool configOn { get { return handler.configOn; } }

        static public string getValue(string key)
        {
            string value = handler.GetValue(key);

            if (value.Equals(ConfigHandler.NO_VALUE) || value.Equals(ConfigHandler.MISSING_VALUE))
            {
                return null;
            }
            else { return value; }
        }
    }
}
