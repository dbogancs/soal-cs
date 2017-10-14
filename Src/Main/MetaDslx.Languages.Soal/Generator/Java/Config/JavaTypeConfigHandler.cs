using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Generator.Config;

namespace MetaDslx.Languages.Soal.Generator.Java.Config
{
    static class JavaTypeConfigHandler
    {
        static private ConfigHandler handler = new ConfigHandler(new JavaTypeConfigConstants(), "java-type-config");
        
        static public bool configOn {  get { return handler.configOn; } }

        static public string GetValue(string key)
        {
            return handler.GetValue(key);
        }
        
        static public string SwitchTypeName(string key)
        {
            string value = GetValue(key);

            if (value.Equals(ConfigHandler.NO_VALUE) || value.Equals(ConfigHandler.MISSING_VALUE))
            {
                return key;
            }
            else { return value; }
        }
    }
}
