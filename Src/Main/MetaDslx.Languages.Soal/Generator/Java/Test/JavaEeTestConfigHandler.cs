using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MetaDslx.Languages.Soal.Generator.Java
{
    static class JavaEeTestConfigHandler
    {
        static public bool testOn;
        static private Dictionary<string, string> config;
        static private bool firstRun = true;

        static public string getValue(string key)
        {
            if (firstRun)
            {
                config = read();
                firstRun = false;
            }

            if (!testOn) return "";
            if (config == null) config = read();

            string value = config[key];
            if (value == null) value = "MISSING_VALUE";
            return value;
        }

        static private Dictionary<string, string> read()
        {
            Dictionary<string, string> config = new Dictionary<string, string>();

            try
            {
                string[] lines = File.ReadAllLines(JavaEeTestConstants.CONFIG_PATH);

                foreach (string line in lines)
                {
                    if (!line.Equals("") && !line[0].Equals('#') && line.Contains(":"))
                    {
                        string[] splits = line.Replace(" ", "").Split(new char[] { ':' }, 2);
                        config.Add(splits[0], splits[1]);
                    }
                }

                if (config[JavaEeTestConstants.TEST_ON].Equals(JavaEeTestConstants.TRUE)) testOn = true;
                else testOn = false;

            } catch(Exception e)
            {
                Console.WriteLine("Exception while reading javaee-test-config.txt: " + e.Message);
                testOn = false;
            }

            return config;
        }


    }
}
