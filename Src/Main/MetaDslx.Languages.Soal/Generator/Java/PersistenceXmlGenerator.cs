using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections; //4:1
using MetaDslx.Languages.Soal.Generator.Java; //5:1

namespace MetaDslx.Languages.Soal.Generator.Java //1:1
{
    using __Hidden_PersistenceXmlGenerator_16043486;
    namespace __Hidden_PersistenceXmlGenerator_16043486
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


    public class PersistenceXmlGenerator //2:1
    {
        private object Instances; //2:1

        public PersistenceXmlGenerator() //2:1
        {
        }

        public PersistenceXmlGenerator(object instances) : this() //2:1
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

        public string Generate(String persistenceUnit, IEnumerable<String> classes, String url, String username, String password) //7:1
        {
            StringBuilder __out = new StringBuilder();
            __out.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"); //8:1
            __out.AppendLine(false); //8:39
            __out.Append("<persistence version=\"2.1\" xmlns=\"http://xmlns.jcp.org/xml/ns/persistence\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://xmlns.jcp.org/xml/ns/persistence http://xmlns.jcp.org/xml/ns/persistence/persistence_2_1.xsd\">"); //9:1
            __out.AppendLine(false); //9:251
            bool __tmp2_outputWritten = false;
            string __tmp3_line = "	<persistence-unit name=\""; //10:1
            if (!string.IsNullOrEmpty(__tmp3_line))
            {
                __out.Append(__tmp3_line);
                __tmp2_outputWritten = true;
            }
            StringBuilder __tmp4 = new StringBuilder();
            __tmp4.Append(persistenceUnit);
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
            string __tmp5_line = "\" transaction-type=\"RESOURCE_LOCAL\">"; //10:43
            if (!string.IsNullOrEmpty(__tmp5_line))
            {
                __out.Append(__tmp5_line);
                __tmp2_outputWritten = true;
            }
            if (__tmp2_outputWritten) __out.AppendLine(true);
            if (__tmp2_outputWritten)
            {
                __out.AppendLine(false); //10:79
            }
            __out.Append("		<provider>org.eclipse.persistence.jpa.PersistenceProvider</provider>"); //11:1
            __out.AppendLine(false); //11:71
            var __loop1_results = 
                (from ins in __Enumerate((classes).GetEnumerator()) //12:7
                select new { ins = ins}
                ).ToList(); //12:2
            for (int __loop1_iteration = 0; __loop1_iteration < __loop1_results.Count; ++__loop1_iteration)
            {
                var __tmp6 = __loop1_results[__loop1_iteration];
                var ins = __tmp6.ins;
                bool __tmp8_outputWritten = false;
                string __tmp9_line = "		<class>"; //13:1
                if (!string.IsNullOrEmpty(__tmp9_line))
                {
                    __out.Append(__tmp9_line);
                    __tmp8_outputWritten = true;
                }
                StringBuilder __tmp10 = new StringBuilder();
                __tmp10.Append(cC(ins));
                using(StreamReader __tmp10Reader = new StreamReader(this.__ToStream(__tmp10.ToString())))
                {
                    bool __tmp10_last = __tmp10Reader.EndOfStream;
                    while(!__tmp10_last)
                    {
                        string __tmp10_line = __tmp10Reader.ReadLine();
                        __tmp10_last = __tmp10Reader.EndOfStream;
                        if ((__tmp10_last && !string.IsNullOrEmpty(__tmp10_line)) || (!__tmp10_last && __tmp10_line != null))
                        {
                            __out.Append(__tmp10_line);
                            __tmp8_outputWritten = true;
                        }
                        if (!__tmp10_last) __out.AppendLine(true);
                    }
                }
                string __tmp11_line = "</class>"; //13:19
                if (!string.IsNullOrEmpty(__tmp11_line))
                {
                    __out.Append(__tmp11_line);
                    __tmp8_outputWritten = true;
                }
                if (__tmp8_outputWritten) __out.AppendLine(true);
                if (__tmp8_outputWritten)
                {
                    __out.AppendLine(false); //13:27
                }
            }
            __out.Append("		<properties>"); //15:1
            __out.AppendLine(false); //15:15
            bool __tmp13_outputWritten = false;
            string __tmp14_line = "			<property name=\"javax.persistence.jdbc.url\" value=\""; //16:1
            if (!string.IsNullOrEmpty(__tmp14_line))
            {
                __out.Append(__tmp14_line);
                __tmp13_outputWritten = true;
            }
            StringBuilder __tmp15 = new StringBuilder();
            __tmp15.Append(url);
            using(StreamReader __tmp15Reader = new StreamReader(this.__ToStream(__tmp15.ToString())))
            {
                bool __tmp15_last = __tmp15Reader.EndOfStream;
                while(!__tmp15_last)
                {
                    string __tmp15_line = __tmp15Reader.ReadLine();
                    __tmp15_last = __tmp15Reader.EndOfStream;
                    if ((__tmp15_last && !string.IsNullOrEmpty(__tmp15_line)) || (!__tmp15_last && __tmp15_line != null))
                    {
                        __out.Append(__tmp15_line);
                        __tmp13_outputWritten = true;
                    }
                    if (!__tmp15_last) __out.AppendLine(true);
                }
            }
            string __tmp16_line = "\"/>"; //16:60
            if (!string.IsNullOrEmpty(__tmp16_line))
            {
                __out.Append(__tmp16_line);
                __tmp13_outputWritten = true;
            }
            if (__tmp13_outputWritten) __out.AppendLine(true);
            if (__tmp13_outputWritten)
            {
                __out.AppendLine(false); //16:63
            }
            bool __tmp18_outputWritten = false;
            string __tmp19_line = "			<property name=\"javax.persistence.jdbc.user\" value=\""; //17:1
            if (!string.IsNullOrEmpty(__tmp19_line))
            {
                __out.Append(__tmp19_line);
                __tmp18_outputWritten = true;
            }
            StringBuilder __tmp20 = new StringBuilder();
            __tmp20.Append(username);
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
            string __tmp21_line = "\"/>"; //17:66
            if (!string.IsNullOrEmpty(__tmp21_line))
            {
                __out.Append(__tmp21_line);
                __tmp18_outputWritten = true;
            }
            if (__tmp18_outputWritten) __out.AppendLine(true);
            if (__tmp18_outputWritten)
            {
                __out.AppendLine(false); //17:69
            }
            __out.Append("			<property name=\"javax.persistence.jdbc.driver\" value=\"org.apache.derby.jdbc.ClientDriver\"/>"); //18:1
            __out.AppendLine(false); //18:95
            bool __tmp23_outputWritten = false;
            string __tmp24_line = "			<property name=\"javax.persistence.jdbc.password\" value=\""; //19:1
            if (!string.IsNullOrEmpty(__tmp24_line))
            {
                __out.Append(__tmp24_line);
                __tmp23_outputWritten = true;
            }
            StringBuilder __tmp25 = new StringBuilder();
            __tmp25.Append(password);
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
            string __tmp26_line = "\"/>"; //19:70
            if (!string.IsNullOrEmpty(__tmp26_line))
            {
                __out.Append(__tmp26_line);
                __tmp23_outputWritten = true;
            }
            if (__tmp23_outputWritten) __out.AppendLine(true);
            if (__tmp23_outputWritten)
            {
                __out.AppendLine(false); //19:73
            }
            __out.Append("			<property name=\"javax.persistence.schema-generation.database.action\" value=\"drop-and-create\"/>"); //20:1
            __out.AppendLine(false); //20:98
            __out.Append("		</properties>"); //21:1
            __out.AppendLine(false); //21:16
            __out.Append("	</persistence-unit>"); //22:1
            __out.AppendLine(false); //22:21
            __out.Append("</persistence>"); //23:1
            __out.AppendLine(false); //23:15
            return __out.ToString();
        }

        public string cC(String fullName) //26:1
        {
            return JavaConventionHelper.classFullNamePackageConvention(fullName); //27:2
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
