using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaDslx.Languages.Soal.Generator.Config
{
    abstract class ConfigConstants
    {
        public string CONFIG_ON { get { return "config_on"; } }
        public string TRUE { get { return "true"; } }
        public abstract string CONFIG_PATH { get; }
    }
}
