using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Symbols; //4:1
using MetaDslx.Languages.Soal.Generator.Java; //5:1

namespace MetaDslx.Languages.Soal.Generator.Java //1:1
{
    using __Hidden_PomXmlGenerator_192260266;
    namespace __Hidden_PomXmlGenerator_192260266
    {
        internal static class __Extensions
        {
            internal static IEnumerator<T> GetEnumerator<T>(this T item)
            {
                if (item == null) return new List<T>().GetEnumerator();
                else return new List<T> { item }.GetEnumerator();
            }
        }
    }


    public class PomXmlGenerator //2:1
    {
        private object Instances; //2:1

        public PomXmlGenerator() //2:1
        {
        }

        public PomXmlGenerator(object instances) : this() //2:1
        {
            this.Instances = instances;
        }

        private Stream __ToStream(string text)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private static IEnumerable<T> __Enumerate<T>(IEnumerator<T> items)
        {
            while (items.MoveNext())
            {
                yield return items.Current;
            }
        }

        private int counter = 0;
        private int NextCounter()
        {
            return ++counter;
        }

        public string Generate(PomXmlIdentifier project, PomXmlIdentifier server, List<PomXmlIdentifier> dependencies) //7:1
        {
            StringBuilder __out = new StringBuilder();
            __out.Append("<project xmlns=\"http://maven.apache.org/POM/4.0.0\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\""); //8:1
            __out.AppendLine(false); //8:105
            __out.Append("  xsi:schemaLocation=\"http://maven.apache.org/POM/4.0.0 http://maven.apache.org/maven-v4_0_0.xsd\">"); //9:1
            __out.AppendLine(false); //9:99
            __out.Append("	<modelVersion>4.0.0</modelVersion>"); //10:1
            __out.AppendLine(false); //10:36
            bool __tmp2_outputWritten = false;
            string __tmp3_line = "	<groupId>"; //11:1
            if (!string.IsNullOrEmpty(__tmp3_line))
            {
                __out.Append(__tmp3_line);
                __tmp2_outputWritten = true;
            }
            StringBuilder __tmp4 = new StringBuilder();
            __tmp4.Append(project.groupId);
            using(StreamReader __tmp4Reader = new StreamReader(this.__ToStream(__tmp4.ToString())))
            {
                bool __tmp4_last = __tmp4Reader.EndOfStream;
                while(!__tmp4_last)
                {
                    string __tmp4_line = __tmp4Reader.ReadLine();
                    __tmp4_last = __tmp4Reader.EndOfStream;
                    if ((__tmp4_last && !string.IsNullOrEmpty(__tmp4_line)) || (!__tmp4_last && __tmp4_line != null))
                    {
                        __out.Append(__tmp4_line);
                        __tmp2_outputWritten = true;
                    }
                    if (!__tmp4_last) __out.AppendLine(true);
                }
            }
            string __tmp5_line = "</groupId>"; //11:28
            if (!string.IsNullOrEmpty(__tmp5_line))
            {
                __out.Append(__tmp5_line);
                __tmp2_outputWritten = true;
            }
            if (__tmp2_outputWritten) __out.AppendLine(true);
            if (__tmp2_outputWritten)
            {
                __out.AppendLine(false); //11:38
            }
            bool __tmp7_outputWritten = false;
            string __tmp8_line = "	<artifactId>"; //12:1
            if (!string.IsNullOrEmpty(__tmp8_line))
            {
                __out.Append(__tmp8_line);
                __tmp7_outputWritten = true;
            }
            StringBuilder __tmp9 = new StringBuilder();
            __tmp9.Append(project.artifactId);
            using(StreamReader __tmp9Reader = new StreamReader(this.__ToStream(__tmp9.ToString())))
            {
                bool __tmp9_last = __tmp9Reader.EndOfStream;
                while(!__tmp9_last)
                {
                    string __tmp9_line = __tmp9Reader.ReadLine();
                    __tmp9_last = __tmp9Reader.EndOfStream;
                    if ((__tmp9_last && !string.IsNullOrEmpty(__tmp9_line)) || (!__tmp9_last && __tmp9_line != null))
                    {
                        __out.Append(__tmp9_line);
                        __tmp7_outputWritten = true;
                    }
                    if (!__tmp9_last) __out.AppendLine(true);
                }
            }
            string __tmp10_line = "</artifactId>"; //12:34
            if (!string.IsNullOrEmpty(__tmp10_line))
            {
                __out.Append(__tmp10_line);
                __tmp7_outputWritten = true;
            }
            if (__tmp7_outputWritten) __out.AppendLine(true);
            if (__tmp7_outputWritten)
            {
                __out.AppendLine(false); //12:47
            }
            bool __tmp12_outputWritten = false;
            string __tmp13_line = "	<version>"; //13:1
            if (!string.IsNullOrEmpty(__tmp13_line))
            {
                __out.Append(__tmp13_line);
                __tmp12_outputWritten = true;
            }
            StringBuilder __tmp14 = new StringBuilder();
            __tmp14.Append(project.version);
            using(StreamReader __tmp14Reader = new StreamReader(this.__ToStream(__tmp14.ToString())))
            {
                bool __tmp14_last = __tmp14Reader.EndOfStream;
                while(!__tmp14_last)
                {
                    string __tmp14_line = __tmp14Reader.ReadLine();
                    __tmp14_last = __tmp14Reader.EndOfStream;
                    if ((__tmp14_last && !string.IsNullOrEmpty(__tmp14_line)) || (!__tmp14_last && __tmp14_line != null))
                    {
                        __out.Append(__tmp14_line);
                        __tmp12_outputWritten = true;
                    }
                    if (!__tmp14_last) __out.AppendLine(true);
                }
            }
            string __tmp15_line = "</version>"; //13:28
            if (!string.IsNullOrEmpty(__tmp15_line))
            {
                __out.Append(__tmp15_line);
                __tmp12_outputWritten = true;
            }
            if (__tmp12_outputWritten) __out.AppendLine(true);
            if (__tmp12_outputWritten)
            {
                __out.AppendLine(false); //13:38
            }
            __out.AppendLine(true); //14:1
            __out.AppendLine(true); //15:1
            __out.Append("	<dependencies>"); //16:1
            __out.AppendLine(false); //16:16
            __out.AppendLine(true); //17:1
            var __loop1_results = 
                (from dependency in __Enumerate((dependencies).GetEnumerator()) //18:8
                select new { dependency = dependency}
                ).ToList(); //18:2
            for (int __loop1_iteration = 0; __loop1_iteration < __loop1_results.Count; ++__loop1_iteration)
            {
                var __tmp16 = __loop1_results[__loop1_iteration];
                var dependency = __tmp16.dependency;
                __out.Append("		<dependency>"); //19:1
                __out.AppendLine(false); //19:15
                bool __tmp18_outputWritten = false;
                string __tmp19_line = "		  <groupId>"; //20:1
                if (!string.IsNullOrEmpty(__tmp19_line))
                {
                    __out.Append(__tmp19_line);
                    __tmp18_outputWritten = true;
                }
                StringBuilder __tmp20 = new StringBuilder();
                __tmp20.Append(dependency.groupId);
                using(StreamReader __tmp20Reader = new StreamReader(this.__ToStream(__tmp20.ToString())))
                {
                    bool __tmp20_last = __tmp20Reader.EndOfStream;
                    while(!__tmp20_last)
                    {
                        string __tmp20_line = __tmp20Reader.ReadLine();
                        __tmp20_last = __tmp20Reader.EndOfStream;
                        if ((__tmp20_last && !string.IsNullOrEmpty(__tmp20_line)) || (!__tmp20_last && __tmp20_line != null))
                        {
                            __out.Append(__tmp20_line);
                            __tmp18_outputWritten = true;
                        }
                        if (!__tmp20_last) __out.AppendLine(true);
                    }
                }
                string __tmp21_line = "</groupId>"; //20:34
                if (!string.IsNullOrEmpty(__tmp21_line))
                {
                    __out.Append(__tmp21_line);
                    __tmp18_outputWritten = true;
                }
                if (__tmp18_outputWritten) __out.AppendLine(true);
                if (__tmp18_outputWritten)
                {
                    __out.AppendLine(false); //20:44
                }
                bool __tmp23_outputWritten = false;
                string __tmp24_line = "		  <artifactId>"; //21:1
                if (!string.IsNullOrEmpty(__tmp24_line))
                {
                    __out.Append(__tmp24_line);
                    __tmp23_outputWritten = true;
                }
                StringBuilder __tmp25 = new StringBuilder();
                __tmp25.Append(dependency.artifactId);
                using(StreamReader __tmp25Reader = new StreamReader(this.__ToStream(__tmp25.ToString())))
                {
                    bool __tmp25_last = __tmp25Reader.EndOfStream;
                    while(!__tmp25_last)
                    {
                        string __tmp25_line = __tmp25Reader.ReadLine();
                        __tmp25_last = __tmp25Reader.EndOfStream;
                        if ((__tmp25_last && !string.IsNullOrEmpty(__tmp25_line)) || (!__tmp25_last && __tmp25_line != null))
                        {
                            __out.Append(__tmp25_line);
                            __tmp23_outputWritten = true;
                        }
                        if (!__tmp25_last) __out.AppendLine(true);
                    }
                }
                string __tmp26_line = "</artifactId>"; //21:40
                if (!string.IsNullOrEmpty(__tmp26_line))
                {
                    __out.Append(__tmp26_line);
                    __tmp23_outputWritten = true;
                }
                if (__tmp23_outputWritten) __out.AppendLine(true);
                if (__tmp23_outputWritten)
                {
                    __out.AppendLine(false); //21:53
                }
                bool __tmp28_outputWritten = false;
                string __tmp29_line = "		  <version>"; //22:1
                if (!string.IsNullOrEmpty(__tmp29_line))
                {
                    __out.Append(__tmp29_line);
                    __tmp28_outputWritten = true;
                }
                StringBuilder __tmp30 = new StringBuilder();
                __tmp30.Append(dependency.version);
                using(StreamReader __tmp30Reader = new StreamReader(this.__ToStream(__tmp30.ToString())))
                {
                    bool __tmp30_last = __tmp30Reader.EndOfStream;
                    while(!__tmp30_last)
                    {
                        string __tmp30_line = __tmp30Reader.ReadLine();
                        __tmp30_last = __tmp30Reader.EndOfStream;
                        if ((__tmp30_last && !string.IsNullOrEmpty(__tmp30_line)) || (!__tmp30_last && __tmp30_line != null))
                        {
                            __out.Append(__tmp30_line);
                            __tmp28_outputWritten = true;
                        }
                        if (!__tmp30_last) __out.AppendLine(true);
                    }
                }
                string __tmp31_line = "</version>"; //22:34
                if (!string.IsNullOrEmpty(__tmp31_line))
                {
                    __out.Append(__tmp31_line);
                    __tmp28_outputWritten = true;
                }
                if (__tmp28_outputWritten) __out.AppendLine(true);
                if (__tmp28_outputWritten)
                {
                    __out.AppendLine(false); //22:44
                }
                __out.Append("		</dependency>"); //23:1
                __out.AppendLine(false); //23:16
                __out.AppendLine(true); //24:1
            }
            __out.Append("	</dependencies>"); //26:1
            __out.AppendLine(false); //26:17
            __out.AppendLine(true); //27:1
            __out.AppendLine(true); //28:1
            __out.Append("	<build>"); //29:1
            __out.AppendLine(false); //29:9
            __out.Append("		<plugins>"); //30:1
            __out.AppendLine(false); //30:12
            __out.AppendLine(true); //31:1
            if (server != null) //32:2
            {
                __out.Append("			<plugin>"); //33:1
                __out.AppendLine(false); //33:12
                bool __tmp33_outputWritten = false;
                string __tmp34_line = "				<groupId>"; //34:1
                if (!string.IsNullOrEmpty(__tmp34_line))
                {
                    __out.Append(__tmp34_line);
                    __tmp33_outputWritten = true;
                }
                StringBuilder __tmp35 = new StringBuilder();
                __tmp35.Append(server.groupId);
                using(StreamReader __tmp35Reader = new StreamReader(this.__ToStream(__tmp35.ToString())))
                {
                    bool __tmp35_last = __tmp35Reader.EndOfStream;
                    while(!__tmp35_last)
                    {
                        string __tmp35_line = __tmp35Reader.ReadLine();
                        __tmp35_last = __tmp35Reader.EndOfStream;
                        if ((__tmp35_last && !string.IsNullOrEmpty(__tmp35_line)) || (!__tmp35_last && __tmp35_line != null))
                        {
                            __out.Append(__tmp35_line);
                            __tmp33_outputWritten = true;
                        }
                        if (!__tmp35_last) __out.AppendLine(true);
                    }
                }
                string __tmp36_line = "</groupId>"; //34:30
                if (!string.IsNullOrEmpty(__tmp36_line))
                {
                    __out.Append(__tmp36_line);
                    __tmp33_outputWritten = true;
                }
                if (__tmp33_outputWritten) __out.AppendLine(true);
                if (__tmp33_outputWritten)
                {
                    __out.AppendLine(false); //34:40
                }
                bool __tmp38_outputWritten = false;
                string __tmp39_line = "				<artifactId>"; //35:1
                if (!string.IsNullOrEmpty(__tmp39_line))
                {
                    __out.Append(__tmp39_line);
                    __tmp38_outputWritten = true;
                }
                StringBuilder __tmp40 = new StringBuilder();
                __tmp40.Append(server.artifactId);
                using(StreamReader __tmp40Reader = new StreamReader(this.__ToStream(__tmp40.ToString())))
                {
                    bool __tmp40_last = __tmp40Reader.EndOfStream;
                    while(!__tmp40_last)
                    {
                        string __tmp40_line = __tmp40Reader.ReadLine();
                        __tmp40_last = __tmp40Reader.EndOfStream;
                        if ((__tmp40_last && !string.IsNullOrEmpty(__tmp40_line)) || (!__tmp40_last && __tmp40_line != null))
                        {
                            __out.Append(__tmp40_line);
                            __tmp38_outputWritten = true;
                        }
                        if (!__tmp40_last) __out.AppendLine(true);
                    }
                }
                string __tmp41_line = "</artifactId>"; //35:36
                if (!string.IsNullOrEmpty(__tmp41_line))
                {
                    __out.Append(__tmp41_line);
                    __tmp38_outputWritten = true;
                }
                if (__tmp38_outputWritten) __out.AppendLine(true);
                if (__tmp38_outputWritten)
                {
                    __out.AppendLine(false); //35:49
                }
                bool __tmp43_outputWritten = false;
                string __tmp44_line = "				<version>"; //36:1
                if (!string.IsNullOrEmpty(__tmp44_line))
                {
                    __out.Append(__tmp44_line);
                    __tmp43_outputWritten = true;
                }
                StringBuilder __tmp45 = new StringBuilder();
                __tmp45.Append(server.version);
                using(StreamReader __tmp45Reader = new StreamReader(this.__ToStream(__tmp45.ToString())))
                {
                    bool __tmp45_last = __tmp45Reader.EndOfStream;
                    while(!__tmp45_last)
                    {
                        string __tmp45_line = __tmp45Reader.ReadLine();
                        __tmp45_last = __tmp45Reader.EndOfStream;
                        if ((__tmp45_last && !string.IsNullOrEmpty(__tmp45_line)) || (!__tmp45_last && __tmp45_line != null))
                        {
                            __out.Append(__tmp45_line);
                            __tmp43_outputWritten = true;
                        }
                        if (!__tmp45_last) __out.AppendLine(true);
                    }
                }
                string __tmp46_line = "</version>"; //36:30
                if (!string.IsNullOrEmpty(__tmp46_line))
                {
                    __out.Append(__tmp46_line);
                    __tmp43_outputWritten = true;
                }
                if (__tmp43_outputWritten) __out.AppendLine(true);
                if (__tmp43_outputWritten)
                {
                    __out.AppendLine(false); //36:40
                }
                __out.Append("			</plugin>"); //37:1
                __out.AppendLine(false); //37:13
                __out.AppendLine(true); //38:1
            }
            __out.Append("		</plugins>"); //40:1
            __out.AppendLine(false); //40:13
            __out.Append("	</build>"); //41:1
            __out.AppendLine(false); //41:10
            __out.AppendLine(true); //42:1
            __out.AppendLine(true); //43:1
            __out.Append("</project>"); //44:1
            __out.AppendLine(false); //44:11
            return __out.ToString();
        }

        private class StringBuilder
        {
            private bool newLine;
            private System.Text.StringBuilder builder = new System.Text.StringBuilder();
            
            public StringBuilder()
            {
                this.newLine = true;
            }
            
            public void Append(string str)
            {
                if (str == null) return;
                if (!string.IsNullOrEmpty(str))
                {
                    this.newLine = false;
                }
                builder.Append(str);
            }
            
            public void Append(object obj)
            {
                if (obj == null) return;
                string text = obj.ToString();
                this.Append(text);
            }
            
            public void AppendLine(bool force = false)
            {
                if (force || !this.newLine)
                {
                    builder.AppendLine();
                    this.newLine = true;
                }
            }
            
            public override string ToString()
            {
                return builder.ToString();
            }
        }
    }
}
