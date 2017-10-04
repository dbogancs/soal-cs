using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MetaDslx.Languages.Soal.Generator.Java
{
    class MavenProjectStructure
    {
        public string projectRoot;
        public string projectName;

        public string projectPath { get { return Path.Combine(projectRoot, projectName); } }
        public string srcPath { get { return Path.Combine(projectPath, "src"); } }
        public string targetPath { get { return Path.Combine(projectPath, "target"); } }
        public string mainPath { get { return Path.Combine(srcPath, "main"); } }
        public string testPath { get { return Path.Combine(srcPath, "test"); } }
        public string mainJavaPath { get { return Path.Combine(mainPath, "java"); } }
        public string mainResourcesPath { get { return Path.Combine(mainPath, "resources"); } }
        public string METAINFPath { get { return Path.Combine(mainResourcesPath, "META_INF"); } }
        public string testJavaPath { get { return Path.Combine(testPath, "java"); } }
        public string testResourcesPath { get { return Path.Combine(testPath, "resources"); } }
        
        public MavenProjectStructure(string projectRoot, string projectName)
        {
            this.projectRoot = projectRoot;
            this.projectName = projectName;
        }

        public void CreateMavenStructure()
        {
            Directory.CreateDirectory(projectPath);
            Directory.CreateDirectory(srcPath);
            Directory.CreateDirectory(targetPath);
            Directory.CreateDirectory(mainPath);
            Directory.CreateDirectory(mainJavaPath);
            Directory.CreateDirectory(mainResourcesPath);
            Directory.CreateDirectory(testPath);
            Directory.CreateDirectory(testJavaPath);
            Directory.CreateDirectory(testResourcesPath);
        }
    }
}
