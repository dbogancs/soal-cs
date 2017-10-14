using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaDslx.Languages.Soal.Generator.Config
{
    class ConfigHandler
    {
        public bool configOn { get; private set; }
        private Dictionary<string, string> config;
        private bool firstRun = true;
        private string configFileName;
        private ConfigConstants constants;

        public static string MISSING_VALUE = "MISSING_VALUE";
        public static string NO_VALUE = "";

        public ConfigHandler(ConfigConstants constants, string configFileName)
        {
            this.constants = constants;
            this.configFileName = configFileName;
        }

        public string GetValue(string key)
        {
            if (firstRun)
            {
                config = read();
                firstRun = false;
            }

            if (!configOn) return NO_VALUE;
            if (config == null) config = read();

            string value = null;

            try
            {
                value = config[key];
            }
            catch
            {
                if (value == null) value = MISSING_VALUE;
            }
            return value;
        }

        public Dictionary<string, string> read()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();

            try
            {
                string[] lines = File.ReadAllLines(constants.CONFIG_PATH);

                foreach (string line in lines)
                {
                    if (!line.Equals("") && !line[0].Equals('#') && line.Contains(":"))
                    {
                        string[] splits = line.Replace(" ", "").Split(new char[] { ':' }, 2);
                        config.Add(splits[0], splits[1]);
                    }
                }

                if (config[constants.CONFIG_ON].Equals(constants.TRUE)) configOn = true;
                else configOn = false;

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while reading "+ configFileName + ".txt: " + e.Message);
                configOn = false;
            }

            return config;
        }
    }
}
