using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Generator.Config;

namespace MetaDslx.Languages.Soal.Generator.Java.JavaEE.JPA
{
    class JpaConfigHandler
    {
        static private ConfigHandler handler = new ConfigHandler(new JpaConfigConstants(), "jpa-config");

        static public bool configOn { get { return handler.configOn; } }

        static public string getValue(string key)
        {
            return handler.GetValue(key);
        }
    }
}
