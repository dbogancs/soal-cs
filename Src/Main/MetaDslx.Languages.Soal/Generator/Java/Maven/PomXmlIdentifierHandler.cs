using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Generator.Java.JavaEE.Config;
using MetaDslx.Languages.Soal.Symbols;

namespace MetaDslx.Languages.Soal.Generator.Java.Maven
{
    static class PomXmlIdentifierHandler
    {
        public static List<PomXmlIdentifier> GetProjectDependences(Component project)
        {
            List<PomXmlIdentifier> dependencies = new List<PomXmlIdentifier>();

            if (MavenConfigHandler.configOn)
            {
                //server = new PomXmlIdentifier();
                //server.groupId = "org.wildfly.plugins";
                //server.artifactId = "wildfly-javaee7-with-tools";
                //server.version = "10.1.0.Final";

                PomXmlIdentifier junitDep = new PomXmlIdentifier();
                junitDep.groupId = MavenConfigHandler.getValue(MavenConfigConstants.JUNIT_DEPENDENCY_GROUPID);
                junitDep.artifactId = MavenConfigHandler.getValue(MavenConfigConstants.JUNIT_DEPENDENCY_ARTIFACTID);
                junitDep.version = MavenConfigHandler.getValue(MavenConfigConstants.JUNIT_DEPENDENCY_VERSION);
                dependencies.Add(junitDep);

                if (project.Implementation.Name.Equals("JPA"))
                {
                    PomXmlIdentifier jpaDep = new PomXmlIdentifier();
                    jpaDep.groupId = MavenConfigHandler.getValue(MavenConfigConstants.JPA_DEPENDENCY_GROUPID);
                    jpaDep.artifactId = MavenConfigHandler.getValue(MavenConfigConstants.JPA_DEPENDENCY_ARTIFACTID);
                    jpaDep.version = MavenConfigHandler.getValue(MavenConfigConstants.JPA_DEPENDENCY_VERSION);
                    dependencies.Add(jpaDep);
                }

                if (project.Implementation.Name.Equals("EJB"))
                {
                    PomXmlIdentifier ejbDep = new PomXmlIdentifier();
                    ejbDep.groupId = MavenConfigHandler.getValue(MavenConfigConstants.EJB_DEPENDENCY_GROUPID);
                    ejbDep.artifactId = MavenConfigHandler.getValue(MavenConfigConstants.EJB_DEPENDENCY_ARTIFACTID);
                    ejbDep.version = MavenConfigHandler.getValue(MavenConfigConstants.EJB_DEPENDENCY_VERSION);
                    dependencies.Add(ejbDep);
                }
            }

            return dependencies;
        }

        public static PomXmlIdentifier GetProjectIdentifier(String appName, Component project)
        {
            PomXmlIdentifier projectId = new PomXmlIdentifier();
            projectId.groupId = appName;
            projectId.artifactId = project.MName;
            if (MavenConfigHandler.configOn) projectId.version = MavenConfigHandler.getValue(MavenConfigConstants.PROJECT_VERSION);

            return projectId;
        }

    }
}
