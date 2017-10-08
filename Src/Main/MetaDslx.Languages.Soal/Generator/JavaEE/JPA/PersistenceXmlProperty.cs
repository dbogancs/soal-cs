using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaDslx.Languages.Soal.Generator.JavaEE.JPA
{
    public class PersistenceXmlProperty
    {
        public string name;
        public string value;

        public PersistenceXmlProperty()
        {

        }

        public PersistenceXmlProperty(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
