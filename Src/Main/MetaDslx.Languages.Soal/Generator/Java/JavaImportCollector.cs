using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaDslx.Languages.Soal.Generator.Java
{
    class JavaImportCollector
    {
        private List<KeyValuePair<string, string>> imports = new List<KeyValuePair<string, string>>();

        public bool AddImportFor(string projectName, string packageName, string fileName, string import)
        {
            KeyValuePair<string, string> kvp = new KeyValuePair<string, string>(projectName + "/" + packageName + "/" + fileName, import);

            List<KeyValuePair<string, string>> oldkvp = imports.FindAll(x => x.Key.Equals(projectName + "/" + packageName + "/" + fileName) && x.Value.Equals(import));
            if (oldkvp == null || oldkvp.Count == 0)
            {
                imports.Add(kvp);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> GetImportsFor(string projectName, string packageName, string fileName)
        {
            List<KeyValuePair<string, string>> pairlist = imports.FindAll(x => x.Key.Equals(projectName + "/" + packageName + "/" + fileName));
            HashSet<string> importlist = new HashSet<string>();
            foreach (var pair in pairlist)
            {
                importlist.Add(pair.Value);
            }
            return importlist.ToList();
        }
    }
}
