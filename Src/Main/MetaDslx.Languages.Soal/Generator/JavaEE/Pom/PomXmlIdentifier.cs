using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaDslx.Languages.Soal.Generator.JavaEE.Pom
{
    public class PomXmlIdentifier
    {
        public String groupId;
        public String artifactId;
        public String version;

        public PomXmlIdentifier()
        {

        }

        public PomXmlIdentifier(String groupId, String artifactId, String version)
        {
            this.groupId = groupId;
            this.artifactId = artifactId;
            this.version = version;
        }
    }
}
