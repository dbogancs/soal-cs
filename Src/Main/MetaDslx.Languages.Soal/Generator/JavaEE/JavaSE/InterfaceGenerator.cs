using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Symbols; //4:1

namespace MetaDslx.Languages.Soal.Generator.JavaSE //1:1
{
    using __Hidden_InterfaceGenerator_1261867386;
    namespace __Hidden_InterfaceGenerator_1261867386
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


    public class InterfaceGenerator //2:1
    {
        private object Instances; //2:1

        public InterfaceGenerator() //2:1
        {
        }

        public InterfaceGenerator(object instances) : this() //2:1
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

        public string Generate(Interface iface) //6:1
        {
            StringBuilder __out = new StringBuilder();
            bool __tmp2_outputWritten = false;
            string __tmp3_line = "package "; //8:1
            if (!string.IsNullOrEmpty(__tmp3_line))
            {
                __out.Append(__tmp3_line);
                __tmp2_outputWritten = true;
            }
            StringBuilder __tmp4 = new StringBuilder();
            __tmp4.Append(iface.Namespace.Name.ToLower());
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
            string __tmp5_line = ";"; //8:41
            if (!string.IsNullOrEmpty(__tmp5_line))
            {
                __out.Append(__tmp5_line);
                __tmp2_outputWritten = true;
            }
            if (__tmp2_outputWritten) __out.AppendLine(true);
            if (__tmp2_outputWritten)
            {
                __out.AppendLine(false); //8:42
            }
            __out.AppendLine(true); //9:1
            bool __tmp7_outputWritten = false;
            string __tmp8_line = "public interface "; //10:1
            if (!string.IsNullOrEmpty(__tmp8_line))
            {
                __out.Append(__tmp8_line);
                __tmp7_outputWritten = true;
            }
            StringBuilder __tmp9 = new StringBuilder();
            __tmp9.Append(iface.Name);
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
            string __tmp10_line = " {"; //10:30
            if (!string.IsNullOrEmpty(__tmp10_line))
            {
                __out.Append(__tmp10_line);
                __tmp7_outputWritten = true;
            }
            if (__tmp7_outputWritten) __out.AppendLine(true);
            if (__tmp7_outputWritten)
            {
                __out.AppendLine(false); //10:32
            }
            var __loop1_results = 
                (from func in __Enumerate((iface.Operations).GetEnumerator()) //11:7
                select new { func = func}
                ).ToList(); //11:2
            for (int __loop1_iteration = 0; __loop1_iteration < __loop1_results.Count; ++__loop1_iteration)
            {
                var __tmp11 = __loop1_results[__loop1_iteration];
                var func = __tmp11.func;
                string __tmp12Prefix = "	"; //12:1
                StringBuilder __tmp14 = new StringBuilder();
                __tmp14.Append(tC(func.Result.Type));
                using(StreamReader __tmp14Reader = new StreamReader(this.__ToStream(__tmp14.ToString())))
                {
                    bool __tmp14_last = __tmp14Reader.EndOfStream;
                    while(!__tmp14_last)
                    {
                        string __tmp14_line = __tmp14Reader.ReadLine();
                        __tmp14_last = __tmp14Reader.EndOfStream;
                        if (!string.IsNullOrEmpty(__tmp12Prefix))
                        {
                            __out.Append(__tmp12Prefix);
                        }
                        if ((__tmp14_last && !string.IsNullOrEmpty(__tmp14_line)) || (!__tmp14_last && __tmp14_line != null))
                        {
                            __out.Append(__tmp14_line);
                        }
                        if (!__tmp14_last) __out.AppendLine(true);
                    }
                }
                string __tmp15_line = " "; //12:24
                if (!string.IsNullOrEmpty(__tmp15_line))
                {
                    __out.Append(__tmp15_line);
                }
                StringBuilder __tmp16 = new StringBuilder();
                __tmp16.Append(func.Name);
                using(StreamReader __tmp16Reader = new StreamReader(this.__ToStream(__tmp16.ToString())))
                {
                    bool __tmp16_last = __tmp16Reader.EndOfStream;
                    while(!__tmp16_last)
                    {
                        string __tmp16_line = __tmp16Reader.ReadLine();
                        __tmp16_last = __tmp16Reader.EndOfStream;
                        if ((__tmp16_last && !string.IsNullOrEmpty(__tmp16_line)) || (!__tmp16_last && __tmp16_line != null))
                        {
                            __out.Append(__tmp16_line);
                        }
                        if (!__tmp16_last) __out.AppendLine(true);
                    }
                }
                string __tmp17_line = "("; //12:36
                if (!string.IsNullOrEmpty(__tmp17_line))
                {
                    __out.Append(__tmp17_line);
                }
                int i = 0; //13:2
                var __loop2_results = 
                    (from arg in __Enumerate((func.Parameters).GetEnumerator()) //14:7
                    select new { arg = arg}
                    ).ToList(); //14:2
                for (int __loop2_iteration = 0; __loop2_iteration < __loop2_results.Count; ++__loop2_iteration)
                {
                    var __tmp18 = __loop2_results[__loop2_iteration];
                    var arg = __tmp18.arg;
                    StringBuilder __tmp21 = new StringBuilder();
                    __tmp21.Append(tC(arg.Type));
                    using(StreamReader __tmp21Reader = new StreamReader(this.__ToStream(__tmp21.ToString())))
                    {
                        bool __tmp21_last = __tmp21Reader.EndOfStream;
                        while(!__tmp21_last)
                        {
                            string __tmp21_line = __tmp21Reader.ReadLine();
                            __tmp21_last = __tmp21Reader.EndOfStream;
                            if ((__tmp21_last && !string.IsNullOrEmpty(__tmp21_line)) || (!__tmp21_last && __tmp21_line != null))
                            {
                                __out.Append(__tmp21_line);
                            }
                            if (!__tmp21_last) __out.AppendLine(true);
                        }
                    }
                    string __tmp22_line = " "; //15:15
                    if (!string.IsNullOrEmpty(__tmp22_line))
                    {
                        __out.Append(__tmp22_line);
                    }
                    StringBuilder __tmp23 = new StringBuilder();
                    __tmp23.Append(mC(arg.Name));
                    using(StreamReader __tmp23Reader = new StreamReader(this.__ToStream(__tmp23.ToString())))
                    {
                        bool __tmp23_last = __tmp23Reader.EndOfStream;
                        while(!__tmp23_last)
                        {
                            string __tmp23_line = __tmp23Reader.ReadLine();
                            __tmp23_last = __tmp23Reader.EndOfStream;
                            if ((__tmp23_last && !string.IsNullOrEmpty(__tmp23_line)) || (!__tmp23_last && __tmp23_line != null))
                            {
                                __out.Append(__tmp23_line);
                            }
                        }
                    }
                    if (i != func.Parameters.Count - 1) //16:2
                    {
                        __out.Append(", "); //16:33
                    }
                    i = i + 1;
                }
                __out.Append(")"); //18:1
                if (func.Exceptions != null && func.Exceptions.Count > 0) //19:2
                {
                    __out.Append(" throws "); //20:1
                    int j = 0; //21:2
                    var __loop3_results = 
                        (from ex in __Enumerate((func.Exceptions).GetEnumerator()) //22:7
                        select new { ex = ex}
                        ).ToList(); //22:2
                    for (int __loop3_iteration = 0; __loop3_iteration < __loop3_results.Count; ++__loop3_iteration)
                    {
                        var __tmp24 = __loop3_results[__loop3_iteration];
                        var ex = __tmp24.ex;
                        StringBuilder __tmp27 = new StringBuilder();
                        __tmp27.Append(ex.Name);
                        using(StreamReader __tmp27Reader = new StreamReader(this.__ToStream(__tmp27.ToString())))
                        {
                            bool __tmp27_last = __tmp27Reader.EndOfStream;
                            while(!__tmp27_last)
                            {
                                string __tmp27_line = __tmp27Reader.ReadLine();
                                __tmp27_last = __tmp27Reader.EndOfStream;
                                if ((__tmp27_last && !string.IsNullOrEmpty(__tmp27_line)) || (!__tmp27_last && __tmp27_line != null))
                                {
                                    __out.Append(__tmp27_line);
                                }
                            }
                        }
                        if (j != func.Exceptions.Count - 1) //24:2
                        {
                            __out.Append(", "); //24:33
                        }
                        j = j + 1;
                    }
                }
                __out.Append(";"); //26:9
                __out.AppendLine(false); //26:10
            }
            __out.Append("}"); //28:1
            __out.AppendLine(false); //28:2
            return __out.ToString();
        }

        public string mC(String methodName) //32:1
        {
            return JavaConventionHelper.methodNameConvention(methodName); //33:2
        }

        public string aC(String attributeName) //36:1
        {
            return JavaConventionHelper.attributeNameConvention(attributeName); //37:2
        }

        public string cC(String columnName) //40:1
        {
            return JavaConventionHelper.databaseColumnNameConvention(columnName); //41:2
        }

        public string tC(SoalType t) //44:1
        {
            return JavaConventionHelper.classNameConvention(t); //45:2
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
