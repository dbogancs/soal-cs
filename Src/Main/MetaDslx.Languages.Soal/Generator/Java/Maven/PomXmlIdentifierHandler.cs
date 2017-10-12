using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Generator.Java.JavaEE.Test;
using MetaDslx.Languages.Soal.Symbols;

namespace MetaDslx.Languages.Soal.Generator.Java.Maven
{
    static class PomXmlIdentifierHandler
    {
        public static List<PomXmlIdentifier> GetProjectDependences(Component project)
        {
            List<PomXmlIdentifier> dependencies = new List<PomXmlIdentifier>();

            if (JavaEeTestConfigHandler.testOn)
            {
                //server = new PomXmlIdentifier();
                //server.groupId = "org.wildfly.plugins";
                //server.artifactId = "wildfly-javaee7-with-tools";
                //server.version = "10.1.0.Final";

                PomXmlIdentifier junitDep = new PomXmlIdentifier();
                junitDep.groupId = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JUNIT_DEPENDENCY_GROUPID);
                junitDep.artifactId = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JUNIT_DEPENDENCY_ARTIFACTID);
                junitDep.version = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JUNIT_DEPENDENCY_VERSION);
                dependencies.Add(junitDep);

                if (project.Implementation.Name.Equals("JPA"))
                {
                    PomXmlIdentifier jpaDep = new PomXmlIdentifier();
                    jpaDep.groupId = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JPA_DEPENDENCY_GROUPID);
                    jpaDep.artifactId = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JPA_DEPENDENCY_ARTIFACTID);
                    jpaDep.version = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.JPA_DEPENDENCY_VERSION);
                    dependencies.Add(jpaDep);
                }

                if (project.Implementation.Name.Equals("EJB"))
                {
                    PomXmlIdentifier ejbDep = new PomXmlIdentifier();
                    ejbDep.groupId = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.EJB_DEPENDENCY_GROUPID);
                    ejbDep.artifactId = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.EJB_DEPENDENCY_ARTIFACTID);
                    ejbDep.version = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.EJB_DEPENDENCY_VERSION);
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
            if (JavaEeTestConfigHandler.testOn) projectId.version = JavaEeTestConfigHandler.getValue(JavaEeTestConstants.PROJECT_VERSION);

            return projectId;
        }

    }
}
