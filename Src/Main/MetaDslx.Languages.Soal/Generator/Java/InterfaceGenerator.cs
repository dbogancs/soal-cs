using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Symbols; //4:1

namespace MetaDslx.Languages.Soal.Generator.Java //1:1
{
    using __Hidden_InterfaceGenerator_1215819900;
    namespace __Hidden_InterfaceGenerator_1215819900
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

        public string Generate(Interface iface, List<string> imports) //6:1
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
            var __loop1_results = 
                (from import in __Enumerate((imports).GetEnumerator()) //10:7
                select new { import = import}
                ).ToList(); //10:2
            for (int __loop1_iteration = 0; __loop1_iteration < __loop1_results.Count; ++__loop1_iteration)
            {
                var __tmp6 = __loop1_results[__loop1_iteration];
                var import = __tmp6.import;
                bool __tmp8_outputWritten = false;
                string __tmp9_line = "import "; //11:1
                if (!string.IsNullOrEmpty(__tmp9_line))
                {
                    __out.Append(__tmp9_line);
                    __tmp8_outputWritten = true;
                }
                StringBuilder __tmp10 = new StringBuilder();
                __tmp10.Append(import);
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
                string __tmp11_line = ";"; //11:16
                if (!string.IsNullOrEmpty(__tmp11_line))
                {
                    __out.Append(__tmp11_line);
                    __tmp8_outputWritten = true;
                }
                if (__tmp8_outputWritten) __out.AppendLine(true);
                if (__tmp8_outputWritten)
                {
                    __out.AppendLine(false); //11:17
                }
            }
            __out.AppendLine(true); //13:1
            bool __tmp13_outputWritten = false;
            string __tmp14_line = "public interface "; //14:1
            if (!string.IsNullOrEmpty(__tmp14_line))
            {
                __out.Append(__tmp14_line);
                __tmp13_outputWritten = true;
            }
            StringBuilder __tmp15 = new StringBuilder();
            __tmp15.Append(iface.Name);
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
            string __tmp16_line = " {"; //14:30
            if (!string.IsNullOrEmpty(__tmp16_line))
            {
                __out.Append(__tmp16_line);
                __tmp13_outputWritten = true;
            }
            if (__tmp13_outputWritten) __out.AppendLine(true);
            if (__tmp13_outputWritten)
            {
                __out.AppendLine(false); //14:32
            }
            var __loop2_results = 
                (from func in __Enumerate((iface.Operations).GetEnumerator()) //15:7
                select new { func = func}
                ).ToList(); //15:2
            for (int __loop2_iteration = 0; __loop2_iteration < __loop2_results.Count; ++__loop2_iteration)
            {
                var __tmp17 = __loop2_results[__loop2_iteration];
                var func = __tmp17.func;
                string __tmp18Prefix = "	"; //16:1
                StringBuilder __tmp20 = new StringBuilder();
                __tmp20.Append(tC(func.Result.Type));
                using(StreamReader __tmp20Reader = new StreamReader(this.__ToStream(__tmp20.ToString())))
                {
                    bool __tmp20_last = __tmp20Reader.EndOfStream;
                    while(!__tmp20_last)
                    {
                        string __tmp20_line = __tmp20Reader.ReadLine();
                        __tmp20_last = __tmp20Reader.EndOfStream;
                        if (!string.IsNullOrEmpty(__tmp18Prefix))
                        {
                            __out.Append(__tmp18Prefix);
                        }
                        if ((__tmp20_last && !string.IsNullOrEmpty(__tmp20_line)) || (!__tmp20_last && __tmp20_line != null))
                        {
                            __out.Append(__tmp20_line);
                        }
                        if (!__tmp20_last) __out.AppendLine(true);
                    }
                }
                string __tmp21_line = " "; //16:24
                if (!string.IsNullOrEmpty(__tmp21_line))
                {
                    __out.Append(__tmp21_line);
                }
                StringBuilder __tmp22 = new StringBuilder();
                __tmp22.Append(func.Name);
                using(StreamReader __tmp22Reader = new StreamReader(this.__ToStream(__tmp22.ToString())))
                {
                    bool __tmp22_last = __tmp22Reader.EndOfStream;
                    while(!__tmp22_last)
                    {
                        string __tmp22_line = __tmp22Reader.ReadLine();
                        __tmp22_last = __tmp22Reader.EndOfStream;
                        if ((__tmp22_last && !string.IsNullOrEmpty(__tmp22_line)) || (!__tmp22_last && __tmp22_line != null))
                        {
                            __out.Append(__tmp22_line);
                        }
                        if (!__tmp22_last) __out.AppendLine(true);
                    }
                }
                string __tmp23_line = "("; //16:36
                if (!string.IsNullOrEmpty(__tmp23_line))
                {
                    __out.Append(__tmp23_line);
                }
                int i = 0; //17:2
                var __loop3_results = 
                    (from arg in __Enumerate((func.Parameters).GetEnumerator()) //18:7
                    select new { arg = arg}
                    ).ToList(); //18:2
                for (int __loop3_iteration = 0; __loop3_iteration < __loop3_results.Count; ++__loop3_iteration)
                {
                    var __tmp24 = __loop3_results[__loop3_iteration];
                    var arg = __tmp24.arg;
                    StringBuilder __tmp27 = new StringBuilder();
                    __tmp27.Append(tC(arg.Type));
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
                            if (!__tmp27_last) __out.AppendLine(true);
                        }
                    }
                    string __tmp28_line = " "; //19:15
                    if (!string.IsNullOrEmpty(__tmp28_line))
                    {
                        __out.Append(__tmp28_line);
                    }
                    StringBuilder __tmp29 = new StringBuilder();
                    __tmp29.Append(mC(arg.Name));
                    using(StreamReader __tmp29Reader = new StreamReader(this.__ToStream(__tmp29.ToString())))
                    {
                        bool __tmp29_last = __tmp29Reader.EndOfStream;
                        while(!__tmp29_last)
                        {
                            string __tmp29_line = __tmp29Reader.ReadLine();
                            __tmp29_last = __tmp29Reader.EndOfStream;
                            if ((__tmp29_last && !string.IsNullOrEmpty(__tmp29_line)) || (!__tmp29_last && __tmp29_line != null))
                            {
                                __out.Append(__tmp29_line);
                            }
                        }
                    }
                    if (i != func.Parameters.Count - 1) //20:2
                    {
                        __out.Append(", "); //20:33
                    }
                    i = i + 1;
                }
                __out.Append(")"); //22:1
                if (func.Exceptions != null && func.Exceptions.Count > 0) //23:2
                {
                    __out.Append(" throws "); //24:1
                    int j = 0; //25:2
                    var __loop4_results = 
                        (from ex in __Enumerate((func.Exceptions).GetEnumerator()) //26:7
                        select new { ex = ex}
                        ).ToList(); //26:2
                    for (int __loop4_iteration = 0; __loop4_iteration < __loop4_results.Count; ++__loop4_iteration)
                    {
                        var __tmp30 = __loop4_results[__loop4_iteration];
                        var ex = __tmp30.ex;
                        StringBuilder __tmp33 = new StringBuilder();
                        __tmp33.Append(ex.Name);
                        using(StreamReader __tmp33Reader = new StreamReader(this.__ToStream(__tmp33.ToString())))
                        {
                            bool __tmp33_last = __tmp33Reader.EndOfStream;
                            while(!__tmp33_last)
                            {
                                string __tmp33_line = __tmp33Reader.ReadLine();
                                __tmp33_last = __tmp33Reader.EndOfStream;
                                if ((__tmp33_last && !string.IsNullOrEmpty(__tmp33_line)) || (!__tmp33_last && __tmp33_line != null))
                                {
                                    __out.Append(__tmp33_line);
                                }
                            }
                        }
                        if (j != func.Exceptions.Count - 1) //28:2
                        {
                            __out.Append(", "); //28:33
                        }
                        j = j + 1;
                    }
                }
                __out.Append(";"); //30:9
                __out.AppendLine(false); //30:10
            }
            __out.Append("}"); //32:1
            __out.AppendLine(false); //32:2
            return __out.ToString();
        }

        public string mC(String methodName) //36:1
        {
            return JavaConventionHelper.methodNameConvention(methodName); //37:2
        }

        public string aC(String attributeName) //40:1
        {
            return JavaConventionHelper.attributeNameConvention(attributeName); //41:2
        }

        public string cC(String columnName) //44:1
        {
            return JavaConventionHelper.databaseColumnNameConvention(columnName); //45:2
        }

        public string tC(SoalType t) //48:1
        {
            return JavaConventionHelper.classNameConvention(t); //49:2
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
