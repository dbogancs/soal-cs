using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Symbols; //4:1
using MetaDslx.Core; //5:1
using MetaDslx.Languages.Soal.Generator.Java; //6:1

namespace MetaDslx.Languages.Soal.Generator.Java.JavaEE.EJB //1:1
{
    using __Hidden_EjbGenerator_677176255;
    namespace __Hidden_EjbGenerator_677176255
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


    public class EjbGenerator //2:1
    {
        private object Instances; //2:1

        public EjbGenerator() //2:1
        {
        }

        public EjbGenerator(object instances) : this() //2:1
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

        public string Generate(Component c, List<string> imports) //8:1
        {
            StringBuilder __out = new StringBuilder();
            bool __tmp2_outputWritten = false;
            string __tmp3_line = "package "; //9:1
            if (!string.IsNullOrEmpty(__tmp3_line))
            {
                __out.Append(__tmp3_line);
                __tmp2_outputWritten = true;
            }
            StringBuilder __tmp4 = new StringBuilder();
            __tmp4.Append(pC(c.Namespace.Name));
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
            string __tmp5_line = ";"; //9:31
            if (!string.IsNullOrEmpty(__tmp5_line))
            {
                __out.Append(__tmp5_line);
                __tmp2_outputWritten = true;
            }
            if (__tmp2_outputWritten) __out.AppendLine(true);
            if (__tmp2_outputWritten)
            {
                __out.AppendLine(false); //9:32
            }
            __out.AppendLine(true); //10:1
            __out.Append("import javax.ejb;"); //11:1
            __out.AppendLine(false); //11:18
            __out.Append("import java.lang.UnsupportedOperationException"); //12:1
            __out.AppendLine(false); //12:47
            __out.Append("import java.util.Date;"); //13:1
            __out.AppendLine(false); //13:23
            var __loop1_results = 
                (from import in __Enumerate((imports).GetEnumerator()) //14:7
                select new { import = import}
                ).ToList(); //14:2
            for (int __loop1_iteration = 0; __loop1_iteration < __loop1_results.Count; ++__loop1_iteration)
            {
                var __tmp6 = __loop1_results[__loop1_iteration];
                var import = __tmp6.import;
                bool __tmp8_outputWritten = false;
                string __tmp9_line = "import "; //15:1
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
                string __tmp11_line = ";"; //15:16
                if (!string.IsNullOrEmpty(__tmp11_line))
                {
                    __out.Append(__tmp11_line);
                    __tmp8_outputWritten = true;
                }
                if (__tmp8_outputWritten) __out.AppendLine(true);
                if (__tmp8_outputWritten)
                {
                    __out.AppendLine(false); //15:17
                }
            }
            __out.AppendLine(true); //17:1
            __out.Append("@Stateless"); //18:1
            __out.AppendLine(false); //18:11
            string __tmp14_line = "public class "; //19:1
            if (!string.IsNullOrEmpty(__tmp14_line))
            {
                __out.Append(__tmp14_line);
            }
            StringBuilder __tmp15 = new StringBuilder();
            __tmp15.Append(c.Name);
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
                    }
                    if (!__tmp15_last) __out.AppendLine(true);
                }
            }
            string __tmp16_line = " "; //19:22
            if (!string.IsNullOrEmpty(__tmp16_line))
            {
                __out.Append(__tmp16_line);
            }
            if (c.Services != null && c.Services.Count > 0) //20:2
            {
                __out.Append("implements "); //20:49
                int k = 0; //21:2
                var __loop2_results = 
                    (from service in __Enumerate((c.Services).GetEnumerator()) //22:7
                    select new { service = service}
                    ).ToList(); //22:2
                for (int __loop2_iteration = 0; __loop2_iteration < __loop2_results.Count; ++__loop2_iteration)
                {
                    var __tmp17 = __loop2_results[__loop2_iteration];
                    var service = __tmp17.service;
                    StringBuilder __tmp20 = new StringBuilder();
                    __tmp20.Append(service.Interface.Name);
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
                            }
                        }
                    }
                    if (k != c.Services.Count - 1) //24:2
                    {
                        __out.Append(", "); //24:28
                    }
                    k = k + 1;
                }
            }
            __out.Append(" {"); //26:9
            __out.AppendLine(false); //26:11
            __out.AppendLine(true); //27:1
            var __loop3_results = 
                (from service in __Enumerate((c.Services).GetEnumerator()) //28:7
                select new { service = service}
                ).ToList(); //28:2
            for (int __loop3_iteration = 0; __loop3_iteration < __loop3_results.Count; ++__loop3_iteration)
            {
                var __tmp21 = __loop3_results[__loop3_iteration];
                var service = __tmp21.service;
                var __loop4_results = 
                    (from func in __Enumerate((service.Interface.Operations).GetEnumerator()) //29:7
                    select new { func = func}
                    ).ToList(); //29:2
                for (int __loop4_iteration = 0; __loop4_iteration < __loop4_results.Count; ++__loop4_iteration)
                {
                    var __tmp22 = __loop4_results[__loop4_iteration];
                    var func = __tmp22.func;
                    string __tmp23Prefix = "	"; //30:1
                    StringBuilder __tmp25 = new StringBuilder();
                    __tmp25.Append(tC(func.Result.Type));
                    using(StreamReader __tmp25Reader = new StreamReader(this.__ToStream(__tmp25.ToString())))
                    {
                        bool __tmp25_last = __tmp25Reader.EndOfStream;
                        while(!__tmp25_last)
                        {
                            string __tmp25_line = __tmp25Reader.ReadLine();
                            __tmp25_last = __tmp25Reader.EndOfStream;
                            if (!string.IsNullOrEmpty(__tmp23Prefix))
                            {
                                __out.Append(__tmp23Prefix);
                            }
                            if ((__tmp25_last && !string.IsNullOrEmpty(__tmp25_line)) || (!__tmp25_last && __tmp25_line != null))
                            {
                                __out.Append(__tmp25_line);
                            }
                            if (!__tmp25_last) __out.AppendLine(true);
                        }
                    }
                    string __tmp26_line = " "; //30:24
                    if (!string.IsNullOrEmpty(__tmp26_line))
                    {
                        __out.Append(__tmp26_line);
                    }
                    StringBuilder __tmp27 = new StringBuilder();
                    __tmp27.Append(func.Name);
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
                    string __tmp28_line = "("; //30:36
                    if (!string.IsNullOrEmpty(__tmp28_line))
                    {
                        __out.Append(__tmp28_line);
                    }
                    int i = 0; //31:2
                    var __loop5_results = 
                        (from arg in __Enumerate((func.Parameters).GetEnumerator()) //32:7
                        select new { arg = arg}
                        ).ToList(); //32:2
                    for (int __loop5_iteration = 0; __loop5_iteration < __loop5_results.Count; ++__loop5_iteration)
                    {
                        var __tmp29 = __loop5_results[__loop5_iteration];
                        var arg = __tmp29.arg;
                        StringBuilder __tmp32 = new StringBuilder();
                        __tmp32.Append(tC(arg.Type));
                        using(StreamReader __tmp32Reader = new StreamReader(this.__ToStream(__tmp32.ToString())))
                        {
                            bool __tmp32_last = __tmp32Reader.EndOfStream;
                            while(!__tmp32_last)
                            {
                                string __tmp32_line = __tmp32Reader.ReadLine();
                                __tmp32_last = __tmp32Reader.EndOfStream;
                                if ((__tmp32_last && !string.IsNullOrEmpty(__tmp32_line)) || (!__tmp32_last && __tmp32_line != null))
                                {
                                    __out.Append(__tmp32_line);
                                }
                                if (!__tmp32_last) __out.AppendLine(true);
                            }
                        }
                        string __tmp33_line = " "; //33:15
                        if (!string.IsNullOrEmpty(__tmp33_line))
                        {
                            __out.Append(__tmp33_line);
                        }
                        StringBuilder __tmp34 = new StringBuilder();
                        __tmp34.Append(mC(arg.Name));
                        using(StreamReader __tmp34Reader = new StreamReader(this.__ToStream(__tmp34.ToString())))
                        {
                            bool __tmp34_last = __tmp34Reader.EndOfStream;
                            while(!__tmp34_last)
                            {
                                string __tmp34_line = __tmp34Reader.ReadLine();
                                __tmp34_last = __tmp34Reader.EndOfStream;
                                if ((__tmp34_last && !string.IsNullOrEmpty(__tmp34_line)) || (!__tmp34_last && __tmp34_line != null))
                                {
                                    __out.Append(__tmp34_line);
                                }
                            }
                        }
                        if (i != func.Parameters.Count - 1) //34:2
                        {
                            __out.Append(", "); //34:33
                        }
                        i = i + 1;
                    }
                    __out.Append(")"); //36:1
                    if (func.Exceptions != null && func.Exceptions.Count > 0) //37:2
                    {
                        __out.Append(" throws "); //38:1
                        int j = 0; //39:2
                        var __loop6_results = 
                            (from ex in __Enumerate((func.Exceptions).GetEnumerator()) //40:7
                            select new { ex = ex}
                            ).ToList(); //40:2
                        for (int __loop6_iteration = 0; __loop6_iteration < __loop6_results.Count; ++__loop6_iteration)
                        {
                            var __tmp35 = __loop6_results[__loop6_iteration];
                            var ex = __tmp35.ex;
                            StringBuilder __tmp38 = new StringBuilder();
                            __tmp38.Append(ex.Name);
                            using(StreamReader __tmp38Reader = new StreamReader(this.__ToStream(__tmp38.ToString())))
                            {
                                bool __tmp38_last = __tmp38Reader.EndOfStream;
                                while(!__tmp38_last)
                                {
                                    string __tmp38_line = __tmp38Reader.ReadLine();
                                    __tmp38_last = __tmp38Reader.EndOfStream;
                                    if ((__tmp38_last && !string.IsNullOrEmpty(__tmp38_line)) || (!__tmp38_last && __tmp38_line != null))
                                    {
                                        __out.Append(__tmp38_line);
                                    }
                                }
                            }
                            if (j != func.Exceptions.Count - 1) //42:2
                            {
                                __out.Append(", "); //42:33
                            }
                            j = j + 1;
                        }
                    }
                    __out.Append(" {"); //44:9
                    __out.AppendLine(false); //44:11
                    __out.Append("		// TODO"); //45:1
                    __out.AppendLine(false); //45:10
                    __out.Append("        throw new UnsupportedOperationException();"); //46:1
                    __out.AppendLine(false); //46:51
                    __out.Append("	}"); //47:1
                    __out.AppendLine(false); //47:3
                    __out.AppendLine(true); //48:1
                }
            }
            __out.Append("}"); //52:1
            __out.AppendLine(false); //52:2
            return __out.ToString();
        }

        public string tC(SoalType t) //56:1
        {
            return JavaConventionHelper.classNameConvention(t); //57:2
        }

        public string mC(String methodName) //60:1
        {
            return JavaConventionHelper.methodNameConvention(methodName); //61:2
        }

        public string pC(String packageName) //64:1
        {
            return JavaConventionHelper.packageConvention(packageName); //65:2
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
