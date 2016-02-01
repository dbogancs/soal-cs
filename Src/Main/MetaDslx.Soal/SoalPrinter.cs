﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaDslx.Soal //1:1
{
    using __Hidden_SoalPrinter_1843054982;
    namespace __Hidden_SoalPrinter_1843054982
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

    public class SoalPrinter //2:1
    {
        private IEnumerable<MetaDslx.Core.ModelObject> Instances; //2:1

        public SoalPrinter() //2:1
        {
        }

        public SoalPrinter(IEnumerable<MetaDslx.Core.ModelObject> instances) : this() //2:1
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

        public string Generate() //4:1
        {
            StringBuilder __out = new StringBuilder();
            var __loop1_results = 
                (from __loop1_var1 in __Enumerate((Instances).GetEnumerator()) //5:7
                from ns in __Enumerate((__loop1_var1).GetEnumerator()).OfType<Namespace>() //5:18
                select new { __loop1_var1 = __loop1_var1, ns = ns}
                ).ToList(); //5:2
            int __loop1_iteration = 0;
            foreach (var __tmp1 in __loop1_results)
            {
                ++__loop1_iteration;
                var __loop1_var1 = __tmp1.__loop1_var1;
                var ns = __tmp1.ns;
                string __tmp2Prefix = string.Empty;
                string __tmp3Suffix = string.Empty;
                StringBuilder __tmp4 = new StringBuilder();
                __tmp4.Append(GenerateNamespace(ns));
                using(StreamReader __tmp4Reader = new StreamReader(this.__ToStream(__tmp4.ToString())))
                {
                    bool __tmp4_first = true;
                    while(__tmp4_first || !__tmp4Reader.EndOfStream)
                    {
                        __tmp4_first = false;
                        string __tmp4Line = __tmp4Reader.ReadLine();
                        if (__tmp4Line == null)
                        {
                            __tmp4Line = "";
                        }
                        __out.Append(__tmp2Prefix);
                        __out.Append(__tmp4Line);
                        __out.Append(__tmp3Suffix);
                        __out.AppendLine(); //6:24
                    }
                }
            }
            return __out.ToString();
        }

        public string GenerateNamespace(Namespace ns) //10:1
        {
            StringBuilder __out = new StringBuilder();
            if (ns.Declarations.Count > 0) //11:2
            {
                if (ns.Prefix != null) //12:2
                {
                    string __tmp1Prefix = "namespace "; //13:1
                    string __tmp2Suffix = "\""; //13:48
                    StringBuilder __tmp3 = new StringBuilder();
                    __tmp3.Append(ns.FullName);
                    using(StreamReader __tmp3Reader = new StreamReader(this.__ToStream(__tmp3.ToString())))
                    {
                        bool __tmp3_first = true;
                        while(__tmp3_first || !__tmp3Reader.EndOfStream)
                        {
                            __tmp3_first = false;
                            string __tmp3Line = __tmp3Reader.ReadLine();
                            if (__tmp3Line == null)
                            {
                                __tmp3Line = "";
                            }
                            __out.Append(__tmp1Prefix);
                            __out.Append(__tmp3Line);
                        }
                    }
                    string __tmp4Line = " = "; //13:24
                    __out.Append(__tmp4Line);
                    StringBuilder __tmp5 = new StringBuilder();
                    __tmp5.Append(ns.Prefix);
                    using(StreamReader __tmp5Reader = new StreamReader(this.__ToStream(__tmp5.ToString())))
                    {
                        bool __tmp5_first = true;
                        while(__tmp5_first || !__tmp5Reader.EndOfStream)
                        {
                            __tmp5_first = false;
                            string __tmp5Line = __tmp5Reader.ReadLine();
                            if (__tmp5Line == null)
                            {
                                __tmp5Line = "";
                            }
                            __out.Append(__tmp5Line);
                        }
                    }
                    string __tmp6Line = ":\""; //13:38
                    __out.Append(__tmp6Line);
                    StringBuilder __tmp7 = new StringBuilder();
                    __tmp7.Append(ns.Uri);
                    using(StreamReader __tmp7Reader = new StreamReader(this.__ToStream(__tmp7.ToString())))
                    {
                        bool __tmp7_first = true;
                        while(__tmp7_first || !__tmp7Reader.EndOfStream)
                        {
                            __tmp7_first = false;
                            string __tmp7Line = __tmp7Reader.ReadLine();
                            if (__tmp7Line == null)
                            {
                                __tmp7Line = "";
                            }
                            __out.Append(__tmp7Line);
                            __out.Append(__tmp2Suffix);
                            __out.AppendLine(); //13:49
                        }
                    }
                }
                else //14:2
                {
                    string __tmp8Prefix = "namespace "; //15:1
                    string __tmp9Suffix = "\""; //15:36
                    StringBuilder __tmp10 = new StringBuilder();
                    __tmp10.Append(ns.FullName);
                    using(StreamReader __tmp10Reader = new StreamReader(this.__ToStream(__tmp10.ToString())))
                    {
                        bool __tmp10_first = true;
                        while(__tmp10_first || !__tmp10Reader.EndOfStream)
                        {
                            __tmp10_first = false;
                            string __tmp10Line = __tmp10Reader.ReadLine();
                            if (__tmp10Line == null)
                            {
                                __tmp10Line = "";
                            }
                            __out.Append(__tmp8Prefix);
                            __out.Append(__tmp10Line);
                        }
                    }
                    string __tmp11Line = " = \""; //15:24
                    __out.Append(__tmp11Line);
                    StringBuilder __tmp12 = new StringBuilder();
                    __tmp12.Append(ns.Uri);
                    using(StreamReader __tmp12Reader = new StreamReader(this.__ToStream(__tmp12.ToString())))
                    {
                        bool __tmp12_first = true;
                        while(__tmp12_first || !__tmp12Reader.EndOfStream)
                        {
                            __tmp12_first = false;
                            string __tmp12Line = __tmp12Reader.ReadLine();
                            if (__tmp12Line == null)
                            {
                                __tmp12Line = "";
                            }
                            __out.Append(__tmp12Line);
                            __out.Append(__tmp9Suffix);
                            __out.AppendLine(); //15:37
                        }
                    }
                }
                __out.Append("{"); //17:1
                __out.AppendLine(); //17:2
                var __loop2_results = 
                    (from __loop2_var1 in __Enumerate((ns).GetEnumerator()) //18:8
                    from Declarations in __Enumerate((__loop2_var1.Declarations).GetEnumerator()) //18:12
                    from enm in __Enumerate((Declarations).GetEnumerator()).OfType<Enum>() //18:26
                    select new { __loop2_var1 = __loop2_var1, Declarations = Declarations, enm = enm}
                    ).ToList(); //18:3
                int __loop2_iteration = 0;
                foreach (var __tmp13 in __loop2_results)
                {
                    ++__loop2_iteration;
                    var __loop2_var1 = __tmp13.__loop2_var1;
                    var Declarations = __tmp13.Declarations;
                    var enm = __tmp13.enm;
                    string __tmp14Prefix = "	"; //19:1
                    string __tmp15Suffix = string.Empty; 
                    StringBuilder __tmp16 = new StringBuilder();
                    __tmp16.Append(GenerateEnum(ns, enm));
                    using(StreamReader __tmp16Reader = new StreamReader(this.__ToStream(__tmp16.ToString())))
                    {
                        bool __tmp16_first = true;
                        while(__tmp16_first || !__tmp16Reader.EndOfStream)
                        {
                            __tmp16_first = false;
                            string __tmp16Line = __tmp16Reader.ReadLine();
                            if (__tmp16Line == null)
                            {
                                __tmp16Line = "";
                            }
                            __out.Append(__tmp14Prefix);
                            __out.Append(__tmp16Line);
                            __out.Append(__tmp15Suffix);
                            __out.AppendLine(); //19:25
                        }
                    }
                    __out.AppendLine(); //20:1
                }
                var __loop3_results = 
                    (from __loop3_var1 in __Enumerate((ns).GetEnumerator()) //22:8
                    from Declarations in __Enumerate((__loop3_var1.Declarations).GetEnumerator()) //22:12
                    from st in __Enumerate((Declarations).GetEnumerator()).OfType<StructuredType>() //22:26
                    select new { __loop3_var1 = __loop3_var1, Declarations = Declarations, st = st}
                    ).ToList(); //22:3
                int __loop3_iteration = 0;
                foreach (var __tmp17 in __loop3_results)
                {
                    ++__loop3_iteration;
                    var __loop3_var1 = __tmp17.__loop3_var1;
                    var Declarations = __tmp17.Declarations;
                    var st = __tmp17.st;
                    string __tmp18Prefix = "	"; //23:1
                    string __tmp19Suffix = string.Empty; 
                    StringBuilder __tmp20 = new StringBuilder();
                    __tmp20.Append(GenerateStructuredType(ns, st));
                    using(StreamReader __tmp20Reader = new StreamReader(this.__ToStream(__tmp20.ToString())))
                    {
                        bool __tmp20_first = true;
                        while(__tmp20_first || !__tmp20Reader.EndOfStream)
                        {
                            __tmp20_first = false;
                            string __tmp20Line = __tmp20Reader.ReadLine();
                            if (__tmp20Line == null)
                            {
                                __tmp20Line = "";
                            }
                            __out.Append(__tmp18Prefix);
                            __out.Append(__tmp20Line);
                            __out.Append(__tmp19Suffix);
                            __out.AppendLine(); //23:34
                        }
                    }
                    __out.AppendLine(); //24:1
                }
                var __loop4_results = 
                    (from __loop4_var1 in __Enumerate((ns).GetEnumerator()) //26:8
                    from Declarations in __Enumerate((__loop4_var1.Declarations).GetEnumerator()) //26:12
                    from intf in __Enumerate((Declarations).GetEnumerator()).OfType<Interface>() //26:26
                    select new { __loop4_var1 = __loop4_var1, Declarations = Declarations, intf = intf}
                    ).ToList(); //26:3
                int __loop4_iteration = 0;
                foreach (var __tmp21 in __loop4_results)
                {
                    ++__loop4_iteration;
                    var __loop4_var1 = __tmp21.__loop4_var1;
                    var Declarations = __tmp21.Declarations;
                    var intf = __tmp21.intf;
                    string __tmp22Prefix = "	"; //27:1
                    string __tmp23Suffix = string.Empty; 
                    StringBuilder __tmp24 = new StringBuilder();
                    __tmp24.Append(GenerateInterface(ns, intf));
                    using(StreamReader __tmp24Reader = new StreamReader(this.__ToStream(__tmp24.ToString())))
                    {
                        bool __tmp24_first = true;
                        while(__tmp24_first || !__tmp24Reader.EndOfStream)
                        {
                            __tmp24_first = false;
                            string __tmp24Line = __tmp24Reader.ReadLine();
                            if (__tmp24Line == null)
                            {
                                __tmp24Line = "";
                            }
                            __out.Append(__tmp22Prefix);
                            __out.Append(__tmp24Line);
                            __out.Append(__tmp23Suffix);
                            __out.AppendLine(); //27:31
                        }
                    }
                    __out.AppendLine(); //28:1
                }
                var __loop5_results = 
                    (from __loop5_var1 in __Enumerate((ns).GetEnumerator()) //30:8
                    from Declarations in __Enumerate((__loop5_var1.Declarations).GetEnumerator()) //30:12
                    from bnd in __Enumerate((Declarations).GetEnumerator()).OfType<Binding>() //30:26
                    select new { __loop5_var1 = __loop5_var1, Declarations = Declarations, bnd = bnd}
                    ).ToList(); //30:3
                int __loop5_iteration = 0;
                foreach (var __tmp25 in __loop5_results)
                {
                    ++__loop5_iteration;
                    var __loop5_var1 = __tmp25.__loop5_var1;
                    var Declarations = __tmp25.Declarations;
                    var bnd = __tmp25.bnd;
                    string __tmp26Prefix = "	"; //31:1
                    string __tmp27Suffix = string.Empty; 
                    StringBuilder __tmp28 = new StringBuilder();
                    __tmp28.Append(GenerateBinding(ns, bnd));
                    using(StreamReader __tmp28Reader = new StreamReader(this.__ToStream(__tmp28.ToString())))
                    {
                        bool __tmp28_first = true;
                        while(__tmp28_first || !__tmp28Reader.EndOfStream)
                        {
                            __tmp28_first = false;
                            string __tmp28Line = __tmp28Reader.ReadLine();
                            if (__tmp28Line == null)
                            {
                                __tmp28Line = "";
                            }
                            __out.Append(__tmp26Prefix);
                            __out.Append(__tmp28Line);
                            __out.Append(__tmp27Suffix);
                            __out.AppendLine(); //31:28
                        }
                    }
                    __out.AppendLine(); //32:1
                }
                var __loop6_results = 
                    (from __loop6_var1 in __Enumerate((ns).GetEnumerator()) //34:8
                    from Declarations in __Enumerate((__loop6_var1.Declarations).GetEnumerator()) //34:12
                    from endp in __Enumerate((Declarations).GetEnumerator()).OfType<Endpoint>() //34:26
                    select new { __loop6_var1 = __loop6_var1, Declarations = Declarations, endp = endp}
                    ).ToList(); //34:3
                int __loop6_iteration = 0;
                foreach (var __tmp29 in __loop6_results)
                {
                    ++__loop6_iteration;
                    var __loop6_var1 = __tmp29.__loop6_var1;
                    var Declarations = __tmp29.Declarations;
                    var endp = __tmp29.endp;
                    string __tmp30Prefix = "	"; //35:1
                    string __tmp31Suffix = string.Empty; 
                    StringBuilder __tmp32 = new StringBuilder();
                    __tmp32.Append(GenerateEndpoint(ns, endp));
                    using(StreamReader __tmp32Reader = new StreamReader(this.__ToStream(__tmp32.ToString())))
                    {
                        bool __tmp32_first = true;
                        while(__tmp32_first || !__tmp32Reader.EndOfStream)
                        {
                            __tmp32_first = false;
                            string __tmp32Line = __tmp32Reader.ReadLine();
                            if (__tmp32Line == null)
                            {
                                __tmp32Line = "";
                            }
                            __out.Append(__tmp30Prefix);
                            __out.Append(__tmp32Line);
                            __out.Append(__tmp31Suffix);
                            __out.AppendLine(); //35:30
                        }
                    }
                    __out.AppendLine(); //36:1
                }
                __out.Append("}"); //38:1
                __out.AppendLine(); //38:2
                __out.AppendLine(); //39:1
            }
            return __out.ToString();
        }

        public string GetAncestor(StructuredType st) //43:1
        {
            if (st is Struct) //44:2
            {
                if (((Struct)st).BaseType != null) //45:3
                {
                    return " : " + ((Struct)st).BaseType.FullName(); //46:4
                }
            }
            else if (st is Exception) //48:2
            {
                if (((Exception)st).BaseType != null) //49:3
                {
                    return " : " + ((Exception)st).BaseType.FullName(); //50:4
                }
            }
            else if (st is Entity) //52:2
            {
                if (((Entity)st).BaseType != null) //53:3
                {
                    return " : " + ((Entity)st).BaseType.FullName(); //54:4
                }
            }
            return ""; //57:2
        }

        public string GenerateAnnotations(AnnotatedElement ae) //60:1
        {
            StringBuilder __out = new StringBuilder();
            var __loop7_results = 
                (from __loop7_var1 in __Enumerate((ae).GetEnumerator()) //61:7
                from annot in __Enumerate((__loop7_var1.Annotations).GetEnumerator()) //61:11
                select new { __loop7_var1 = __loop7_var1, annot = annot}
                ).ToList(); //61:2
            int __loop7_iteration = 0;
            foreach (var __tmp1 in __loop7_results)
            {
                ++__loop7_iteration;
                var __loop7_var1 = __tmp1.__loop7_var1;
                var annot = __tmp1.annot;
                string __tmp2Prefix = string.Empty; 
                string __tmp3Suffix = string.Empty; 
                StringBuilder __tmp4 = new StringBuilder();
                __tmp4.Append("[");
                using(StreamReader __tmp4Reader = new StreamReader(this.__ToStream(__tmp4.ToString())))
                {
                    bool __tmp4_first = true;
                    while(__tmp4_first || !__tmp4Reader.EndOfStream)
                    {
                        __tmp4_first = false;
                        string __tmp4Line = __tmp4Reader.ReadLine();
                        if (__tmp4Line == null)
                        {
                            __tmp4Line = "";
                        }
                        __out.Append(__tmp2Prefix);
                        __out.Append(__tmp4Line);
                    }
                }
                StringBuilder __tmp5 = new StringBuilder();
                __tmp5.Append(annot.Name);
                using(StreamReader __tmp5Reader = new StreamReader(this.__ToStream(__tmp5.ToString())))
                {
                    bool __tmp5_first = true;
                    while(__tmp5_first || !__tmp5Reader.EndOfStream)
                    {
                        __tmp5_first = false;
                        string __tmp5Line = __tmp5Reader.ReadLine();
                        if (__tmp5Line == null)
                        {
                            __tmp5Line = "";
                        }
                        __out.Append(__tmp5Line);
                    }
                }
                StringBuilder __tmp6 = new StringBuilder();
                __tmp6.Append("]");
                using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
                {
                    bool __tmp6_first = true;
                    while(__tmp6_first || !__tmp6Reader.EndOfStream)
                    {
                        __tmp6_first = false;
                        string __tmp6Line = __tmp6Reader.ReadLine();
                        if (__tmp6Line == null)
                        {
                            __tmp6Line = "";
                        }
                        __out.Append(__tmp6Line);
                        __out.Append(__tmp3Suffix);
                        __out.AppendLine(); //62:23
                    }
                }
            }
            return __out.ToString();
        }

        public string GenerateAnnotationsSingleLine(AnnotatedElement ae) //66:1
        {
            StringBuilder __out = new StringBuilder();
            var __loop8_results = 
                (from __loop8_var1 in __Enumerate((ae).GetEnumerator()) //67:7
                from annot in __Enumerate((__loop8_var1.Annotations).GetEnumerator()) //67:11
                select new { __loop8_var1 = __loop8_var1, annot = annot}
                ).ToList(); //67:2
            int __loop8_iteration = 0;
            foreach (var __tmp1 in __loop8_results)
            {
                ++__loop8_iteration;
                var __loop8_var1 = __tmp1.__loop8_var1;
                var annot = __tmp1.annot;
                string __tmp2Prefix = string.Empty; 
                string __tmp3Suffix = string.Empty; 
                StringBuilder __tmp4 = new StringBuilder();
                __tmp4.Append("[");
                using(StreamReader __tmp4Reader = new StreamReader(this.__ToStream(__tmp4.ToString())))
                {
                    bool __tmp4_first = true;
                    while(__tmp4_first || !__tmp4Reader.EndOfStream)
                    {
                        __tmp4_first = false;
                        string __tmp4Line = __tmp4Reader.ReadLine();
                        if (__tmp4Line == null)
                        {
                            __tmp4Line = "";
                        }
                        __out.Append(__tmp2Prefix);
                        __out.Append(__tmp4Line);
                    }
                }
                StringBuilder __tmp5 = new StringBuilder();
                __tmp5.Append(annot.Name);
                using(StreamReader __tmp5Reader = new StreamReader(this.__ToStream(__tmp5.ToString())))
                {
                    bool __tmp5_first = true;
                    while(__tmp5_first || !__tmp5Reader.EndOfStream)
                    {
                        __tmp5_first = false;
                        string __tmp5Line = __tmp5Reader.ReadLine();
                        if (__tmp5Line == null)
                        {
                            __tmp5Line = "";
                        }
                        __out.Append(__tmp5Line);
                    }
                }
                StringBuilder __tmp6 = new StringBuilder();
                __tmp6.Append("]");
                using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
                {
                    bool __tmp6_first = true;
                    while(__tmp6_first || !__tmp6Reader.EndOfStream)
                    {
                        __tmp6_first = false;
                        string __tmp6Line = __tmp6Reader.ReadLine();
                        if (__tmp6Line == null)
                        {
                            __tmp6Line = "";
                        }
                        __out.Append(__tmp6Line);
                        __out.Append(__tmp3Suffix);
                    }
                }
            }
            return __out.ToString();
        }

        public string GenerateReturnAnnotations(IList<Annotation> al) //72:1
        {
            StringBuilder __out = new StringBuilder();
            var __loop9_results = 
                (from annot in __Enumerate((al).GetEnumerator()) //73:7
                select new { annot = annot}
                ).ToList(); //73:2
            int __loop9_iteration = 0;
            foreach (var __tmp1 in __loop9_results)
            {
                ++__loop9_iteration;
                var annot = __tmp1.annot;
                string __tmp2Prefix = string.Empty; 
                string __tmp3Suffix = string.Empty; 
                StringBuilder __tmp4 = new StringBuilder();
                __tmp4.Append("[return: ");
                using(StreamReader __tmp4Reader = new StreamReader(this.__ToStream(__tmp4.ToString())))
                {
                    bool __tmp4_first = true;
                    while(__tmp4_first || !__tmp4Reader.EndOfStream)
                    {
                        __tmp4_first = false;
                        string __tmp4Line = __tmp4Reader.ReadLine();
                        if (__tmp4Line == null)
                        {
                            __tmp4Line = "";
                        }
                        __out.Append(__tmp2Prefix);
                        __out.Append(__tmp4Line);
                    }
                }
                StringBuilder __tmp5 = new StringBuilder();
                __tmp5.Append(annot.Name);
                using(StreamReader __tmp5Reader = new StreamReader(this.__ToStream(__tmp5.ToString())))
                {
                    bool __tmp5_first = true;
                    while(__tmp5_first || !__tmp5Reader.EndOfStream)
                    {
                        __tmp5_first = false;
                        string __tmp5Line = __tmp5Reader.ReadLine();
                        if (__tmp5Line == null)
                        {
                            __tmp5Line = "";
                        }
                        __out.Append(__tmp5Line);
                    }
                }
                StringBuilder __tmp6 = new StringBuilder();
                __tmp6.Append("]");
                using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
                {
                    bool __tmp6_first = true;
                    while(__tmp6_first || !__tmp6Reader.EndOfStream)
                    {
                        __tmp6_first = false;
                        string __tmp6Line = __tmp6Reader.ReadLine();
                        if (__tmp6Line == null)
                        {
                            __tmp6Line = "";
                        }
                        __out.Append(__tmp6Line);
                        __out.Append(__tmp3Suffix);
                        __out.AppendLine(); //74:31
                    }
                }
            }
            return __out.ToString();
        }

        public string GenerateEnum(Namespace currentNs, Enum enm) //78:1
        {
            StringBuilder __out = new StringBuilder();
            string __tmp1Prefix = string.Empty;
            string __tmp2Suffix = string.Empty;
            StringBuilder __tmp3 = new StringBuilder();
            __tmp3.Append(GenerateAnnotations(enm));
            using(StreamReader __tmp3Reader = new StreamReader(this.__ToStream(__tmp3.ToString())))
            {
                bool __tmp3_first = true;
                while(__tmp3_first || !__tmp3Reader.EndOfStream)
                {
                    __tmp3_first = false;
                    string __tmp3Line = __tmp3Reader.ReadLine();
                    if (__tmp3Line == null)
                    {
                        __tmp3Line = "";
                    }
                    __out.Append(__tmp1Prefix);
                    __out.Append(__tmp3Line);
                    __out.Append(__tmp2Suffix);
                    __out.AppendLine(); //79:27
                }
            }
            string __tmp4Prefix = "enum "; //80:1
            string __tmp5Suffix = string.Empty; 
            StringBuilder __tmp6 = new StringBuilder();
            __tmp6.Append(enm.Name);
            using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
            {
                bool __tmp6_first = true;
                while(__tmp6_first || !__tmp6Reader.EndOfStream)
                {
                    __tmp6_first = false;
                    string __tmp6Line = __tmp6Reader.ReadLine();
                    if (__tmp6Line == null)
                    {
                        __tmp6Line = "";
                    }
                    __out.Append(__tmp4Prefix);
                    __out.Append(__tmp6Line);
                    __out.Append(__tmp5Suffix);
                    __out.AppendLine(); //80:16
                }
            }
            __out.Append("{"); //81:1
            string __tmp7Prefix = "	"; //82:1
            string __tmp8Suffix = string.Empty; 
            StringBuilder __tmp9 = new StringBuilder();
            __tmp9.Append(GenerateEnumLiterals(currentNs, enm));
            using(StreamReader __tmp9Reader = new StreamReader(this.__ToStream(__tmp9.ToString())))
            {
                bool __tmp9_first = true;
                while(__tmp9_first || !__tmp9Reader.EndOfStream)
                {
                    __tmp9_first = false;
                    string __tmp9Line = __tmp9Reader.ReadLine();
                    if (__tmp9Line == null)
                    {
                        __tmp9Line = "";
                    }
                    __out.Append(__tmp7Prefix);
                    __out.Append(__tmp9Line);
                    __out.Append(__tmp8Suffix);
                    __out.AppendLine(); //82:40
                }
            }
            __out.Append("}"); //83:1
            __out.AppendLine(); //83:2
            return __out.ToString();
        }

        public string GenerateEnumLiterals(Namespace currentNs, Enum enm) //86:1
        {
            StringBuilder __out = new StringBuilder();
            var __loop10_results = 
                (from lit in __Enumerate((enm.EnumLiterals).GetEnumerator()) //87:8
                select new { lit = lit}
                ).ToList(); //87:3
            int __loop10_iteration = 0;
            string delim = ""; //87:30
            foreach (var __tmp1 in __loop10_results)
            {
                ++__loop10_iteration;
                if (__loop10_iteration >= 2) //87:49
                {
                    delim = ", "; //87:49
                }
                var lit = __tmp1.lit;
                string __tmp2Prefix = string.Empty;
                string __tmp3Suffix = string.Empty;
                StringBuilder __tmp4 = new StringBuilder();
                __tmp4.Append(delim);
                using(StreamReader __tmp4Reader = new StreamReader(this.__ToStream(__tmp4.ToString())))
                {
                    bool __tmp4_first = true;
                    while(__tmp4_first || !__tmp4Reader.EndOfStream)
                    {
                        __tmp4_first = false;
                        string __tmp4Line = __tmp4Reader.ReadLine();
                        if (__tmp4Line == null)
                        {
                            __tmp4Line = "";
                        }
                        __out.Append(__tmp2Prefix);
                        __out.Append(__tmp4Line);
                        __out.Append(__tmp3Suffix);
                        __out.AppendLine(); //88:8
                    }
                }
                string __tmp5Prefix = string.Empty;
                string __tmp6Suffix = string.Empty;
                StringBuilder __tmp7 = new StringBuilder();
                __tmp7.Append(GenerateAnnotations(lit));
                using(StreamReader __tmp7Reader = new StreamReader(this.__ToStream(__tmp7.ToString())))
                {
                    bool __tmp7_first = true;
                    while(__tmp7_first || !__tmp7Reader.EndOfStream)
                    {
                        __tmp7_first = false;
                        string __tmp7Line = __tmp7Reader.ReadLine();
                        if (__tmp7Line == null)
                        {
                            __tmp7Line = "";
                        }
                        __out.Append(__tmp5Prefix);
                        __out.Append(__tmp7Line);
                        __out.Append(__tmp6Suffix);
                    }
                }
                string __tmp8Prefix = string.Empty;
                string __tmp9Suffix = string.Empty;
                StringBuilder __tmp10 = new StringBuilder();
                __tmp10.Append(lit.Name);
                using(StreamReader __tmp10Reader = new StreamReader(this.__ToStream(__tmp10.ToString())))
                {
                    bool __tmp10_first = true;
                    while(__tmp10_first || !__tmp10Reader.EndOfStream)
                    {
                        __tmp10_first = false;
                        string __tmp10Line = __tmp10Reader.ReadLine();
                        if (__tmp10Line == null)
                        {
                            __tmp10Line = "";
                        }
                        __out.Append(__tmp8Prefix);
                        __out.Append(__tmp10Line);
                        __out.Append(__tmp9Suffix);
                    }
                }
            }
            return __out.ToString();
        }

        public string GenerateStructuredType(Namespace currentNs, StructuredType st) //94:1
        {
            StringBuilder __out = new StringBuilder();
            string __tmp1Prefix = string.Empty;
            string __tmp2Suffix = string.Empty;
            StringBuilder __tmp3 = new StringBuilder();
            __tmp3.Append(GenerateAnnotations(st));
            using(StreamReader __tmp3Reader = new StreamReader(this.__ToStream(__tmp3.ToString())))
            {
                bool __tmp3_first = true;
                while(__tmp3_first || !__tmp3Reader.EndOfStream)
                {
                    __tmp3_first = false;
                    string __tmp3Line = __tmp3Reader.ReadLine();
                    if (__tmp3Line == null)
                    {
                        __tmp3Line = "";
                    }
                    __out.Append(__tmp1Prefix);
                    __out.Append(__tmp3Line);
                    __out.Append(__tmp2Suffix);
                    __out.AppendLine(); //95:26
                }
            }
            if (st is Struct) //96:2
            {
                __out.Append("struct "); //97:1
            }
            else if (st is Exception) //98:2
            {
                __out.Append("exception "); //99:1
            }
            else if (st is Entity) //100:2
            {
                __out.Append("entity "); //101:1
            }
            string __tmp4Prefix = string.Empty; 
            string __tmp5Suffix = string.Empty; 
            StringBuilder __tmp6 = new StringBuilder();
            __tmp6.Append(st.Name);
            using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
            {
                bool __tmp6_first = true;
                while(__tmp6_first || !__tmp6Reader.EndOfStream)
                {
                    __tmp6_first = false;
                    string __tmp6Line = __tmp6Reader.ReadLine();
                    if (__tmp6Line == null)
                    {
                        __tmp6Line = "";
                    }
                    __out.Append(__tmp4Prefix);
                    __out.Append(__tmp6Line);
                }
            }
            StringBuilder __tmp7 = new StringBuilder();
            __tmp7.Append(GetAncestor(st));
            using(StreamReader __tmp7Reader = new StreamReader(this.__ToStream(__tmp7.ToString())))
            {
                bool __tmp7_first = true;
                while(__tmp7_first || !__tmp7Reader.EndOfStream)
                {
                    __tmp7_first = false;
                    string __tmp7Line = __tmp7Reader.ReadLine();
                    if (__tmp7Line == null)
                    {
                        __tmp7Line = "";
                    }
                    __out.Append(__tmp7Line);
                    __out.Append(__tmp5Suffix);
                    __out.AppendLine(); //103:27
                }
            }
            __out.Append("{"); //104:1
            __out.AppendLine(); //104:2
            var __loop11_results = 
                (from __loop11_var1 in __Enumerate((st).GetEnumerator()) //105:8
                from prop in __Enumerate((__loop11_var1.Properties).GetEnumerator()) //105:12
                select new { __loop11_var1 = __loop11_var1, prop = prop}
                ).ToList(); //105:3
            int __loop11_iteration = 0;
            foreach (var __tmp8 in __loop11_results)
            {
                ++__loop11_iteration;
                var __loop11_var1 = __tmp8.__loop11_var1;
                var prop = __tmp8.prop;
                string __tmp9Prefix = "	"; //106:1
                string __tmp10Suffix = string.Empty; 
                StringBuilder __tmp11 = new StringBuilder();
                __tmp11.Append(GenerateProperty(currentNs, prop));
                using(StreamReader __tmp11Reader = new StreamReader(this.__ToStream(__tmp11.ToString())))
                {
                    bool __tmp11_first = true;
                    while(__tmp11_first || !__tmp11Reader.EndOfStream)
                    {
                        __tmp11_first = false;
                        string __tmp11Line = __tmp11Reader.ReadLine();
                        if (__tmp11Line == null)
                        {
                            __tmp11Line = "";
                        }
                        __out.Append(__tmp9Prefix);
                        __out.Append(__tmp11Line);
                        __out.Append(__tmp10Suffix);
                        __out.AppendLine(); //106:37
                    }
                }
            }
            __out.Append("}"); //108:1
            __out.AppendLine(); //108:2
            return __out.ToString();
        }

        public string GenerateProperty(Namespace currentNs, Property prop) //111:1
        {
            StringBuilder __out = new StringBuilder();
            string __tmp1Prefix = string.Empty;
            string __tmp2Suffix = string.Empty;
            StringBuilder __tmp3 = new StringBuilder();
            __tmp3.Append(GenerateAnnotations(prop));
            using(StreamReader __tmp3Reader = new StreamReader(this.__ToStream(__tmp3.ToString())))
            {
                bool __tmp3_first = true;
                while(__tmp3_first || !__tmp3Reader.EndOfStream)
                {
                    __tmp3_first = false;
                    string __tmp3Line = __tmp3Reader.ReadLine();
                    if (__tmp3Line == null)
                    {
                        __tmp3Line = "";
                    }
                    __out.Append(__tmp1Prefix);
                    __out.Append(__tmp3Line);
                    __out.Append(__tmp2Suffix);
                }
            }
            string __tmp4Prefix = string.Empty; 
            string __tmp5Suffix = ";"; //113:49
            StringBuilder __tmp6 = new StringBuilder();
            __tmp6.Append(GenerateType(currentNs, prop.Type));
            using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
            {
                bool __tmp6_first = true;
                while(__tmp6_first || !__tmp6Reader.EndOfStream)
                {
                    __tmp6_first = false;
                    string __tmp6Line = __tmp6Reader.ReadLine();
                    if (__tmp6Line == null)
                    {
                        __tmp6Line = "";
                    }
                    __out.Append(__tmp4Prefix);
                    __out.Append(__tmp6Line);
                }
            }
            string __tmp7Line = " "; //113:37
            __out.Append(__tmp7Line);
            StringBuilder __tmp8 = new StringBuilder();
            __tmp8.Append(prop.Name);
            using(StreamReader __tmp8Reader = new StreamReader(this.__ToStream(__tmp8.ToString())))
            {
                bool __tmp8_first = true;
                while(__tmp8_first || !__tmp8Reader.EndOfStream)
                {
                    __tmp8_first = false;
                    string __tmp8Line = __tmp8Reader.ReadLine();
                    if (__tmp8Line == null)
                    {
                        __tmp8Line = "";
                    }
                    __out.Append(__tmp8Line);
                    __out.Append(__tmp5Suffix);
                    __out.AppendLine(); //113:50
                }
            }
            return __out.ToString();
        }

        public string GenerateRef(Namespace currentNs, Declaration decl) //116:1
        {
            StringBuilder __out = new StringBuilder();
            if (decl.Namespace != currentNs) //117:2
            {
                string __tmp1Prefix = string.Empty;
                string __tmp2Suffix = string.Empty;
                StringBuilder __tmp3 = new StringBuilder();
                __tmp3.Append(decl.FullName());
                using(StreamReader __tmp3Reader = new StreamReader(this.__ToStream(__tmp3.ToString())))
                {
                    bool __tmp3_first = true;
                    while(__tmp3_first || !__tmp3Reader.EndOfStream)
                    {
                        __tmp3_first = false;
                        string __tmp3Line = __tmp3Reader.ReadLine();
                        if (__tmp3Line == null)
                        {
                            __tmp3Line = "";
                        }
                        __out.Append(__tmp1Prefix);
                        __out.Append(__tmp3Line);
                        __out.Append(__tmp2Suffix);
                    }
                }
            }
            else //119:2
            {
                string __tmp4Prefix = string.Empty;
                string __tmp5Suffix = string.Empty;
                StringBuilder __tmp6 = new StringBuilder();
                __tmp6.Append(decl.Name);
                using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
                {
                    bool __tmp6_first = true;
                    while(__tmp6_first || !__tmp6Reader.EndOfStream)
                    {
                        __tmp6_first = false;
                        string __tmp6Line = __tmp6Reader.ReadLine();
                        if (__tmp6Line == null)
                        {
                            __tmp6Line = "";
                        }
                        __out.Append(__tmp4Prefix);
                        __out.Append(__tmp6Line);
                        __out.Append(__tmp5Suffix);
                    }
                }
            }
            return __out.ToString();
        }

        public string GenerateType(Namespace currentNs, SoalType t) //124:1
        {
            StringBuilder __out = new StringBuilder();
            if (t is PrimitiveType) //125:2
            {
                string __tmp1Prefix = string.Empty;
                string __tmp2Suffix = string.Empty;
                StringBuilder __tmp3 = new StringBuilder();
                __tmp3.Append(((PrimitiveType)t).Name);
                using(StreamReader __tmp3Reader = new StreamReader(this.__ToStream(__tmp3.ToString())))
                {
                    bool __tmp3_first = true;
                    while(__tmp3_first || !__tmp3Reader.EndOfStream)
                    {
                        __tmp3_first = false;
                        string __tmp3Line = __tmp3Reader.ReadLine();
                        if (__tmp3Line == null)
                        {
                            __tmp3Line = "";
                        }
                        __out.Append(__tmp1Prefix);
                        __out.Append(__tmp3Line);
                        __out.Append(__tmp2Suffix);
                    }
                }
            }
            else if (t is NullableType) //127:2
            {
                string __tmp4Prefix = string.Empty; 
                string __tmp5Suffix = "?"; //128:55
                StringBuilder __tmp6 = new StringBuilder();
                __tmp6.Append(GenerateType(currentNs, ((NullableType)t).InnerType));
                using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
                {
                    bool __tmp6_first = true;
                    while(__tmp6_first || !__tmp6Reader.EndOfStream)
                    {
                        __tmp6_first = false;
                        string __tmp6Line = __tmp6Reader.ReadLine();
                        if (__tmp6Line == null)
                        {
                            __tmp6Line = "";
                        }
                        __out.Append(__tmp4Prefix);
                        __out.Append(__tmp6Line);
                        __out.Append(__tmp5Suffix);
                    }
                }
            }
            else if (t is NonNullableType) //129:2
            {
                string __tmp7Prefix = string.Empty; 
                string __tmp8Suffix = "!"; //130:58
                StringBuilder __tmp9 = new StringBuilder();
                __tmp9.Append(GenerateType(currentNs, ((NonNullableType)t).InnerType));
                using(StreamReader __tmp9Reader = new StreamReader(this.__ToStream(__tmp9.ToString())))
                {
                    bool __tmp9_first = true;
                    while(__tmp9_first || !__tmp9Reader.EndOfStream)
                    {
                        __tmp9_first = false;
                        string __tmp9Line = __tmp9Reader.ReadLine();
                        if (__tmp9Line == null)
                        {
                            __tmp9Line = "";
                        }
                        __out.Append(__tmp7Prefix);
                        __out.Append(__tmp9Line);
                        __out.Append(__tmp8Suffix);
                    }
                }
            }
            else if (t is ArrayType) //131:2
            {
                string __tmp10Prefix = string.Empty; 
                string __tmp11Suffix = string.Empty; 
                StringBuilder __tmp12 = new StringBuilder();
                __tmp12.Append(GenerateType(currentNs, ((ArrayType)t).InnerType));
                using(StreamReader __tmp12Reader = new StreamReader(this.__ToStream(__tmp12.ToString())))
                {
                    bool __tmp12_first = true;
                    while(__tmp12_first || !__tmp12Reader.EndOfStream)
                    {
                        __tmp12_first = false;
                        string __tmp12Line = __tmp12Reader.ReadLine();
                        if (__tmp12Line == null)
                        {
                            __tmp12Line = "";
                        }
                        __out.Append(__tmp10Prefix);
                        __out.Append(__tmp12Line);
                    }
                }
                StringBuilder __tmp13 = new StringBuilder();
                __tmp13.Append("[]");
                using(StreamReader __tmp13Reader = new StreamReader(this.__ToStream(__tmp13.ToString())))
                {
                    bool __tmp13_first = true;
                    while(__tmp13_first || !__tmp13Reader.EndOfStream)
                    {
                        __tmp13_first = false;
                        string __tmp13Line = __tmp13Reader.ReadLine();
                        if (__tmp13Line == null)
                        {
                            __tmp13Line = "";
                        }
                        __out.Append(__tmp13Line);
                        __out.Append(__tmp11Suffix);
                    }
                }
            }
            else if (t is Declaration) //133:2
            {
                string __tmp14Prefix = string.Empty;
                string __tmp15Suffix = string.Empty;
                StringBuilder __tmp16 = new StringBuilder();
                __tmp16.Append(GenerateRef(currentNs, (Declaration)t));
                using(StreamReader __tmp16Reader = new StreamReader(this.__ToStream(__tmp16.ToString())))
                {
                    bool __tmp16_first = true;
                    while(__tmp16_first || !__tmp16Reader.EndOfStream)
                    {
                        __tmp16_first = false;
                        string __tmp16Line = __tmp16Reader.ReadLine();
                        if (__tmp16Line == null)
                        {
                            __tmp16Line = "";
                        }
                        __out.Append(__tmp14Prefix);
                        __out.Append(__tmp16Line);
                        __out.Append(__tmp15Suffix);
                        __out.AppendLine(); //134:41
                    }
                }
            }
            else //135:2
            {
                string __tmp17Prefix = string.Empty;
                string __tmp18Suffix = string.Empty;
                StringBuilder __tmp19 = new StringBuilder();
                __tmp19.Append(t);
                using(StreamReader __tmp19Reader = new StreamReader(this.__ToStream(__tmp19.ToString())))
                {
                    bool __tmp19_first = true;
                    while(__tmp19_first || !__tmp19Reader.EndOfStream)
                    {
                        __tmp19_first = false;
                        string __tmp19Line = __tmp19Reader.ReadLine();
                        if (__tmp19Line == null)
                        {
                            __tmp19Line = "";
                        }
                        __out.Append(__tmp17Prefix);
                        __out.Append(__tmp19Line);
                        __out.Append(__tmp18Suffix);
                    }
                }
            }
            return __out.ToString();
        }

        public string GenerateReturnType(Namespace currentNs, Operation op) //140:1
        {
            StringBuilder __out = new StringBuilder();
            if (op.IsOneway) //141:2
            {
                __out.Append("oneway"); //142:1
            }
            else //143:2
            {
                string __tmp1Prefix = string.Empty;
                string __tmp2Suffix = string.Empty;
                StringBuilder __tmp3 = new StringBuilder();
                __tmp3.Append(GenerateType(currentNs, op.ReturnType));
                using(StreamReader __tmp3Reader = new StreamReader(this.__ToStream(__tmp3.ToString())))
                {
                    bool __tmp3_first = true;
                    while(__tmp3_first || !__tmp3Reader.EndOfStream)
                    {
                        __tmp3_first = false;
                        string __tmp3Line = __tmp3Reader.ReadLine();
                        if (__tmp3Line == null)
                        {
                            __tmp3Line = "";
                        }
                        __out.Append(__tmp1Prefix);
                        __out.Append(__tmp3Line);
                        __out.Append(__tmp2Suffix);
                    }
                }
            }
            return __out.ToString();
        }

        public string GenerateThrows(Namespace currentNs, Operation op) //148:1
        {
            StringBuilder __out = new StringBuilder();
            var __loop12_results = 
                (from __loop12_var1 in __Enumerate((op).GetEnumerator()) //149:7
                from ex in __Enumerate((__loop12_var1.Exceptions).GetEnumerator()) //149:11
                select new { __loop12_var1 = __loop12_var1, ex = ex}
                ).ToList(); //149:2
            int __loop12_iteration = 0;
            string delim = " throws "; //149:26
            foreach (var __tmp1 in __loop12_results)
            {
                ++__loop12_iteration;
                if (__loop12_iteration >= 2) //149:53
                {
                    delim = ", "; //149:53
                }
                var __loop12_var1 = __tmp1.__loop12_var1;
                var ex = __tmp1.ex;
                string __tmp2Prefix = string.Empty; 
                string __tmp3Suffix = string.Empty; 
                StringBuilder __tmp4 = new StringBuilder();
                __tmp4.Append(delim);
                using(StreamReader __tmp4Reader = new StreamReader(this.__ToStream(__tmp4.ToString())))
                {
                    bool __tmp4_first = true;
                    while(__tmp4_first || !__tmp4Reader.EndOfStream)
                    {
                        __tmp4_first = false;
                        string __tmp4Line = __tmp4Reader.ReadLine();
                        if (__tmp4Line == null)
                        {
                            __tmp4Line = "";
                        }
                        __out.Append(__tmp2Prefix);
                        __out.Append(__tmp4Line);
                    }
                }
                StringBuilder __tmp5 = new StringBuilder();
                __tmp5.Append(GenerateType(currentNs, ex));
                using(StreamReader __tmp5Reader = new StreamReader(this.__ToStream(__tmp5.ToString())))
                {
                    bool __tmp5_first = true;
                    while(__tmp5_first || !__tmp5Reader.EndOfStream)
                    {
                        __tmp5_first = false;
                        string __tmp5Line = __tmp5Reader.ReadLine();
                        if (__tmp5Line == null)
                        {
                            __tmp5Line = "";
                        }
                        __out.Append(__tmp5Line);
                        __out.Append(__tmp3Suffix);
                        __out.AppendLine(); //150:37
                    }
                }
            }
            return __out.ToString();
        }

        public string GenerateParameters(Namespace currentNs, Operation op) //154:1
        {
            StringBuilder __out = new StringBuilder();
            var __loop13_results = 
                (from __loop13_var1 in __Enumerate((op).GetEnumerator()) //155:7
                from p in __Enumerate((__loop13_var1.Parameters).GetEnumerator()) //155:11
                select new { __loop13_var1 = __loop13_var1, p = p}
                ).ToList(); //155:2
            int __loop13_iteration = 0;
            string delim = ""; //155:25
            foreach (var __tmp1 in __loop13_results)
            {
                ++__loop13_iteration;
                if (__loop13_iteration >= 2) //155:44
                {
                    delim = ", "; //155:44
                }
                var __loop13_var1 = __tmp1.__loop13_var1;
                var p = __tmp1.p;
                string __tmp2Prefix = string.Empty; 
                string __tmp3Suffix = string.Empty; 
                StringBuilder __tmp4 = new StringBuilder();
                __tmp4.Append(delim);
                using(StreamReader __tmp4Reader = new StreamReader(this.__ToStream(__tmp4.ToString())))
                {
                    bool __tmp4_first = true;
                    while(__tmp4_first || !__tmp4Reader.EndOfStream)
                    {
                        __tmp4_first = false;
                        string __tmp4Line = __tmp4Reader.ReadLine();
                        if (__tmp4Line == null)
                        {
                            __tmp4Line = "";
                        }
                        __out.Append(__tmp2Prefix);
                        __out.Append(__tmp4Line);
                    }
                }
                StringBuilder __tmp5 = new StringBuilder();
                __tmp5.Append(GenerateAnnotationsSingleLine(p));
                using(StreamReader __tmp5Reader = new StreamReader(this.__ToStream(__tmp5.ToString())))
                {
                    bool __tmp5_first = true;
                    while(__tmp5_first || !__tmp5Reader.EndOfStream)
                    {
                        __tmp5_first = false;
                        string __tmp5Line = __tmp5Reader.ReadLine();
                        if (__tmp5Line == null)
                        {
                            __tmp5Line = "";
                        }
                        __out.Append(__tmp5Line);
                    }
                }
                StringBuilder __tmp6 = new StringBuilder();
                __tmp6.Append(GenerateType(currentNs, p.Type));
                using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
                {
                    bool __tmp6_first = true;
                    while(__tmp6_first || !__tmp6Reader.EndOfStream)
                    {
                        __tmp6_first = false;
                        string __tmp6Line = __tmp6Reader.ReadLine();
                        if (__tmp6Line == null)
                        {
                            __tmp6Line = "";
                        }
                        __out.Append(__tmp6Line);
                    }
                }
                string __tmp7Line = " "; //156:75
                __out.Append(__tmp7Line);
                StringBuilder __tmp8 = new StringBuilder();
                __tmp8.Append(p.Name);
                using(StreamReader __tmp8Reader = new StreamReader(this.__ToStream(__tmp8.ToString())))
                {
                    bool __tmp8_first = true;
                    while(__tmp8_first || !__tmp8Reader.EndOfStream)
                    {
                        __tmp8_first = false;
                        string __tmp8Line = __tmp8Reader.ReadLine();
                        if (__tmp8Line == null)
                        {
                            __tmp8Line = "";
                        }
                        __out.Append(__tmp8Line);
                        __out.Append(__tmp3Suffix);
                        __out.AppendLine(); //156:84
                    }
                }
            }
            return __out.ToString();
        }

        public string GenerateOperation(Namespace currentNs, Operation op) //160:1
        {
            StringBuilder __out = new StringBuilder();
            string __tmp1Prefix = string.Empty;
            string __tmp2Suffix = string.Empty;
            StringBuilder __tmp3 = new StringBuilder();
            __tmp3.Append(GenerateAnnotations(op));
            using(StreamReader __tmp3Reader = new StreamReader(this.__ToStream(__tmp3.ToString())))
            {
                bool __tmp3_first = true;
                while(__tmp3_first || !__tmp3Reader.EndOfStream)
                {
                    __tmp3_first = false;
                    string __tmp3Line = __tmp3Reader.ReadLine();
                    if (__tmp3Line == null)
                    {
                        __tmp3Line = "";
                    }
                    __out.Append(__tmp1Prefix);
                    __out.Append(__tmp3Line);
                    __out.Append(__tmp2Suffix);
                }
            }
            string __tmp4Prefix = string.Empty;
            string __tmp5Suffix = string.Empty;
            StringBuilder __tmp6 = new StringBuilder();
            __tmp6.Append(GenerateReturnAnnotations(op.ReturnAnnotations));
            using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
            {
                bool __tmp6_first = true;
                while(__tmp6_first || !__tmp6Reader.EndOfStream)
                {
                    __tmp6_first = false;
                    string __tmp6Line = __tmp6Reader.ReadLine();
                    if (__tmp6Line == null)
                    {
                        __tmp6Line = "";
                    }
                    __out.Append(__tmp4Prefix);
                    __out.Append(__tmp6Line);
                    __out.Append(__tmp5Suffix);
                }
            }
            string __tmp7Prefix = string.Empty; 
            string __tmp8Suffix = ";"; //163:114
            StringBuilder __tmp9 = new StringBuilder();
            __tmp9.Append(GenerateReturnType(currentNs, op));
            using(StreamReader __tmp9Reader = new StreamReader(this.__ToStream(__tmp9.ToString())))
            {
                bool __tmp9_first = true;
                while(__tmp9_first || !__tmp9Reader.EndOfStream)
                {
                    __tmp9_first = false;
                    string __tmp9Line = __tmp9Reader.ReadLine();
                    if (__tmp9Line == null)
                    {
                        __tmp9Line = "";
                    }
                    __out.Append(__tmp7Prefix);
                    __out.Append(__tmp9Line);
                }
            }
            string __tmp10Line = " "; //163:36
            __out.Append(__tmp10Line);
            StringBuilder __tmp11 = new StringBuilder();
            __tmp11.Append(op.Name);
            using(StreamReader __tmp11Reader = new StreamReader(this.__ToStream(__tmp11.ToString())))
            {
                bool __tmp11_first = true;
                while(__tmp11_first || !__tmp11Reader.EndOfStream)
                {
                    __tmp11_first = false;
                    string __tmp11Line = __tmp11Reader.ReadLine();
                    if (__tmp11Line == null)
                    {
                        __tmp11Line = "";
                    }
                    __out.Append(__tmp11Line);
                }
            }
            string __tmp12Line = "("; //163:46
            __out.Append(__tmp12Line);
            StringBuilder __tmp13 = new StringBuilder();
            __tmp13.Append(GenerateParameters(currentNs, op));
            using(StreamReader __tmp13Reader = new StreamReader(this.__ToStream(__tmp13.ToString())))
            {
                bool __tmp13_first = true;
                while(__tmp13_first || !__tmp13Reader.EndOfStream)
                {
                    __tmp13_first = false;
                    string __tmp13Line = __tmp13Reader.ReadLine();
                    if (__tmp13Line == null)
                    {
                        __tmp13Line = "";
                    }
                    __out.Append(__tmp13Line);
                }
            }
            string __tmp14Line = ")"; //163:82
            __out.Append(__tmp14Line);
            StringBuilder __tmp15 = new StringBuilder();
            __tmp15.Append(GenerateThrows(currentNs, op));
            using(StreamReader __tmp15Reader = new StreamReader(this.__ToStream(__tmp15.ToString())))
            {
                bool __tmp15_first = true;
                while(__tmp15_first || !__tmp15Reader.EndOfStream)
                {
                    __tmp15_first = false;
                    string __tmp15Line = __tmp15Reader.ReadLine();
                    if (__tmp15Line == null)
                    {
                        __tmp15Line = "";
                    }
                    __out.Append(__tmp15Line);
                    __out.Append(__tmp8Suffix);
                    __out.AppendLine(); //163:115
                }
            }
            return __out.ToString();
        }

        public string GenerateInterface(Namespace currentNs, Interface intf) //166:1
        {
            StringBuilder __out = new StringBuilder();
            string __tmp1Prefix = string.Empty;
            string __tmp2Suffix = string.Empty;
            StringBuilder __tmp3 = new StringBuilder();
            __tmp3.Append(GenerateAnnotations(intf));
            using(StreamReader __tmp3Reader = new StreamReader(this.__ToStream(__tmp3.ToString())))
            {
                bool __tmp3_first = true;
                while(__tmp3_first || !__tmp3Reader.EndOfStream)
                {
                    __tmp3_first = false;
                    string __tmp3Line = __tmp3Reader.ReadLine();
                    if (__tmp3Line == null)
                    {
                        __tmp3Line = "";
                    }
                    __out.Append(__tmp1Prefix);
                    __out.Append(__tmp3Line);
                    __out.Append(__tmp2Suffix);
                    __out.AppendLine(); //167:28
                }
            }
            string __tmp4Prefix = "interface "; //168:1
            string __tmp5Suffix = string.Empty; 
            StringBuilder __tmp6 = new StringBuilder();
            __tmp6.Append(intf.Name);
            using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
            {
                bool __tmp6_first = true;
                while(__tmp6_first || !__tmp6Reader.EndOfStream)
                {
                    __tmp6_first = false;
                    string __tmp6Line = __tmp6Reader.ReadLine();
                    if (__tmp6Line == null)
                    {
                        __tmp6Line = "";
                    }
                    __out.Append(__tmp4Prefix);
                    __out.Append(__tmp6Line);
                    __out.Append(__tmp5Suffix);
                    __out.AppendLine(); //168:22
                }
            }
            __out.Append("{"); //169:1
            __out.AppendLine(); //169:2
            var __loop14_results = 
                (from __loop14_var1 in __Enumerate((intf).GetEnumerator()) //170:8
                from op in __Enumerate((__loop14_var1.Operations).GetEnumerator()) //170:14
                select new { __loop14_var1 = __loop14_var1, op = op}
                ).ToList(); //170:3
            int __loop14_iteration = 0;
            foreach (var __tmp7 in __loop14_results)
            {
                ++__loop14_iteration;
                var __loop14_var1 = __tmp7.__loop14_var1;
                var op = __tmp7.op;
                string __tmp8Prefix = "	"; //171:1
                string __tmp9Suffix = string.Empty; 
                StringBuilder __tmp10 = new StringBuilder();
                __tmp10.Append(GenerateOperation(currentNs, op));
                using(StreamReader __tmp10Reader = new StreamReader(this.__ToStream(__tmp10.ToString())))
                {
                    bool __tmp10_first = true;
                    while(__tmp10_first || !__tmp10Reader.EndOfStream)
                    {
                        __tmp10_first = false;
                        string __tmp10Line = __tmp10Reader.ReadLine();
                        if (__tmp10Line == null)
                        {
                            __tmp10Line = "";
                        }
                        __out.Append(__tmp8Prefix);
                        __out.Append(__tmp10Line);
                        __out.Append(__tmp9Suffix);
                        __out.AppendLine(); //171:36
                    }
                }
            }
            __out.Append("}"); //173:1
            __out.AppendLine(); //173:2
            return __out.ToString();
        }

        public string GenerateTransportBinding(Namespace currentNs, TransportBindingElement bnd) //176:1
        {
            StringBuilder __out = new StringBuilder();
            var __tmp1 = bnd; //177:10
            if (bnd is HttpTransportBindingElement) //178:2
            {
                if (((HttpTransportBindingElement)bnd).Ssl) //179:3
                {
                    __out.Append("transport HTTP"); //180:1
                    __out.AppendLine(); //180:15
                    __out.Append("{"); //181:1
                    __out.AppendLine(); //181:2
                    __out.Append("	Ssl = true;"); //182:1
                    __out.AppendLine(); //182:13
                    string __tmp2Prefix = "	ClientAuthentication = "; //183:1
                    string __tmp3Suffix = ";"; //183:51
                    StringBuilder __tmp4 = new StringBuilder();
                    __tmp4.Append(((HttpTransportBindingElement)bnd).ClientAuthentication);
                    using(StreamReader __tmp4Reader = new StreamReader(this.__ToStream(__tmp4.ToString())))
                    {
                        bool __tmp4_first = true;
                        while(__tmp4_first || !__tmp4Reader.EndOfStream)
                        {
                            __tmp4_first = false;
                            string __tmp4Line = __tmp4Reader.ReadLine();
                            if (__tmp4Line == null)
                            {
                                __tmp4Line = "";
                            }
                            __out.Append(__tmp2Prefix);
                            __out.Append(__tmp4Line);
                            __out.Append(__tmp3Suffix);
                            __out.AppendLine(); //183:52
                        }
                    }
                    __out.Append("}"); //184:1
                    __out.AppendLine(); //184:2
                }
                else //185:3
                {
                    __out.Append("transport HTTP;"); //186:1
                    __out.AppendLine(); //186:16
                }
            }
            else if (bnd is RestTransportBindingElement) //188:2
            {
                __out.Append("transport REST;"); //188:39
                __out.AppendLine(); //188:54
            }
            else if (bnd is WebSocketTransportBindingElement) //189:2
            {
                __out.Append("transport WebSocket;"); //189:44
                __out.AppendLine(); //189:64
            }//190:2
            return __out.ToString();
        }

        public string GenerateEncodingBinding(Namespace currentNs, EncodingBindingElement bnd) //193:1
        {
            StringBuilder __out = new StringBuilder();
            var __tmp1 = bnd; //194:10
            if (bnd is SoapEncodingBindingElement) //195:2
            {
                if (((SoapEncodingBindingElement)bnd).Style != SoapEncodingStyle.DocumentWrapped || ((SoapEncodingBindingElement)bnd).Version != SoapVersion.Soap11 || ((SoapEncodingBindingElement)bnd).Mtom) //196:2
                {
                    __out.Append("encoding SOAP"); //197:1
                    __out.AppendLine(); //197:14
                    __out.Append("{"); //198:1
                    __out.AppendLine(); //198:2
                    if (((SoapEncodingBindingElement)bnd).Style != SoapEncodingStyle.DocumentWrapped) //199:3
                    {
                        string __tmp2Prefix = "	Style = "; //200:1
                        string __tmp3Suffix = ";"; //200:21
                        StringBuilder __tmp4 = new StringBuilder();
                        __tmp4.Append(((SoapEncodingBindingElement)bnd).Style);
                        using(StreamReader __tmp4Reader = new StreamReader(this.__ToStream(__tmp4.ToString())))
                        {
                            bool __tmp4_first = true;
                            while(__tmp4_first || !__tmp4Reader.EndOfStream)
                            {
                                __tmp4_first = false;
                                string __tmp4Line = __tmp4Reader.ReadLine();
                                if (__tmp4Line == null)
                                {
                                    __tmp4Line = "";
                                }
                                __out.Append(__tmp2Prefix);
                                __out.Append(__tmp4Line);
                                __out.Append(__tmp3Suffix);
                                __out.AppendLine(); //200:22
                            }
                        }
                    }
                    if (((SoapEncodingBindingElement)bnd).Version != SoapVersion.Soap11) //202:3
                    {
                        string __tmp5Prefix = "	Version = "; //203:1
                        string __tmp6Suffix = ";"; //203:25
                        StringBuilder __tmp7 = new StringBuilder();
                        __tmp7.Append(((SoapEncodingBindingElement)bnd).Version);
                        using(StreamReader __tmp7Reader = new StreamReader(this.__ToStream(__tmp7.ToString())))
                        {
                            bool __tmp7_first = true;
                            while(__tmp7_first || !__tmp7Reader.EndOfStream)
                            {
                                __tmp7_first = false;
                                string __tmp7Line = __tmp7Reader.ReadLine();
                                if (__tmp7Line == null)
                                {
                                    __tmp7Line = "";
                                }
                                __out.Append(__tmp5Prefix);
                                __out.Append(__tmp7Line);
                                __out.Append(__tmp6Suffix);
                                __out.AppendLine(); //203:26
                            }
                        }
                    }
                    if (((SoapEncodingBindingElement)bnd).Mtom) //205:3
                    {
                        __out.Append("	Mtom = true;"); //206:1
                        __out.AppendLine(); //206:14
                    }
                    __out.Append("}"); //208:1
                    __out.AppendLine(); //208:2
                }
                else //209:2
                {
                    __out.Append("encoding SOAP;"); //210:1
                    __out.AppendLine(); //210:15
                }
            }
            else if (bnd is XmlEncodingBindingElement) //212:2
            {
                __out.Append("encoding XML;"); //212:37
                __out.AppendLine(); //212:50
            }
            else if (bnd is JsonEncodingBindingElement) //213:2
            {
                __out.Append("encoding JSON;"); //213:38
                __out.AppendLine(); //213:52
            }//214:2
            return __out.ToString();
        }

        public string GenerateBinding(Namespace currentNs, Binding bnd) //217:1
        {
            StringBuilder __out = new StringBuilder();
            string __tmp1Prefix = "binding "; //218:1
            string __tmp2Suffix = string.Empty; 
            StringBuilder __tmp3 = new StringBuilder();
            __tmp3.Append(bnd.Name);
            using(StreamReader __tmp3Reader = new StreamReader(this.__ToStream(__tmp3.ToString())))
            {
                bool __tmp3_first = true;
                while(__tmp3_first || !__tmp3Reader.EndOfStream)
                {
                    __tmp3_first = false;
                    string __tmp3Line = __tmp3Reader.ReadLine();
                    if (__tmp3Line == null)
                    {
                        __tmp3Line = "";
                    }
                    __out.Append(__tmp1Prefix);
                    __out.Append(__tmp3Line);
                    __out.Append(__tmp2Suffix);
                    __out.AppendLine(); //218:19
                }
            }
            __out.Append("{"); //219:1
            __out.AppendLine(); //219:2
            string __tmp4Prefix = "	"; //220:1
            string __tmp5Suffix = string.Empty; 
            StringBuilder __tmp6 = new StringBuilder();
            __tmp6.Append(GenerateTransportBinding(currentNs, bnd.Transport));
            using(StreamReader __tmp6Reader = new StreamReader(this.__ToStream(__tmp6.ToString())))
            {
                bool __tmp6_first = true;
                while(__tmp6_first || !__tmp6Reader.EndOfStream)
                {
                    __tmp6_first = false;
                    string __tmp6Line = __tmp6Reader.ReadLine();
                    if (__tmp6Line == null)
                    {
                        __tmp6Line = "";
                    }
                    __out.Append(__tmp4Prefix);
                    __out.Append(__tmp6Line);
                    __out.Append(__tmp5Suffix);
                    __out.AppendLine(); //220:54
                }
            }
            var __loop15_results = 
                (from __loop15_var1 in __Enumerate((bnd).GetEnumerator()) //221:8
                from enc in __Enumerate((__loop15_var1.Encodings).GetEnumerator()) //221:13
                select new { __loop15_var1 = __loop15_var1, enc = enc}
                ).ToList(); //221:3
            int __loop15_iteration = 0;
            foreach (var __tmp7 in __loop15_results)
            {
                ++__loop15_iteration;
                var __loop15_var1 = __tmp7.__loop15_var1;
                var enc = __tmp7.enc;
                string __tmp8Prefix = "	"; //222:1
                string __tmp9Suffix = string.Empty; 
                StringBuilder __tmp10 = new StringBuilder();
                __tmp10.Append(GenerateEncodingBinding(currentNs, enc));
                using(StreamReader __tmp10Reader = new StreamReader(this.__ToStream(__tmp10.ToString())))
                {
                    bool __tmp10_first = true;
                    while(__tmp10_first || !__tmp10Reader.EndOfStream)
                    {
                        __tmp10_first = false;
                        string __tmp10Line = __tmp10Reader.ReadLine();
                        if (__tmp10Line == null)
                        {
                            __tmp10Line = "";
                        }
                        __out.Append(__tmp8Prefix);
                        __out.Append(__tmp10Line);
                        __out.Append(__tmp9Suffix);
                        __out.AppendLine(); //222:43
                    }
                }
            }
            __out.Append("}"); //224:1
            __out.AppendLine(); //224:2
            return __out.ToString();
        }

        public string GenerateEndpoint(Namespace currentNs, Endpoint endp) //227:1
        {
            StringBuilder __out = new StringBuilder();
            string __tmp1Prefix = "endpoint "; //228:1
            string __tmp2Suffix = string.Empty; 
            StringBuilder __tmp3 = new StringBuilder();
            __tmp3.Append(endp.Name);
            using(StreamReader __tmp3Reader = new StreamReader(this.__ToStream(__tmp3.ToString())))
            {
                bool __tmp3_first = true;
                while(__tmp3_first || !__tmp3Reader.EndOfStream)
                {
                    __tmp3_first = false;
                    string __tmp3Line = __tmp3Reader.ReadLine();
                    if (__tmp3Line == null)
                    {
                        __tmp3Line = "";
                    }
                    __out.Append(__tmp1Prefix);
                    __out.Append(__tmp3Line);
                }
            }
            string __tmp4Line = " : "; //228:21
            __out.Append(__tmp4Line);
            StringBuilder __tmp5 = new StringBuilder();
            __tmp5.Append(GenerateRef(currentNs, endp.Interface));
            using(StreamReader __tmp5Reader = new StreamReader(this.__ToStream(__tmp5.ToString())))
            {
                bool __tmp5_first = true;
                while(__tmp5_first || !__tmp5Reader.EndOfStream)
                {
                    __tmp5_first = false;
                    string __tmp5Line = __tmp5Reader.ReadLine();
                    if (__tmp5Line == null)
                    {
                        __tmp5Line = "";
                    }
                    __out.Append(__tmp5Line);
                    __out.Append(__tmp2Suffix);
                    __out.AppendLine(); //228:64
                }
            }
            __out.Append("{"); //229:1
            __out.AppendLine(); //229:2
            string __tmp6Prefix = "	binding "; //230:1
            string __tmp7Suffix = ";"; //230:48
            StringBuilder __tmp8 = new StringBuilder();
            __tmp8.Append(GenerateRef(currentNs, endp.Binding));
            using(StreamReader __tmp8Reader = new StreamReader(this.__ToStream(__tmp8.ToString())))
            {
                bool __tmp8_first = true;
                while(__tmp8_first || !__tmp8Reader.EndOfStream)
                {
                    __tmp8_first = false;
                    string __tmp8Line = __tmp8Reader.ReadLine();
                    if (__tmp8Line == null)
                    {
                        __tmp8Line = "";
                    }
                    __out.Append(__tmp6Prefix);
                    __out.Append(__tmp8Line);
                    __out.Append(__tmp7Suffix);
                    __out.AppendLine(); //230:49
                }
            }
            string __tmp9Prefix = "	location "; //231:1
            string __tmp10Suffix = ";"; //231:25
            StringBuilder __tmp11 = new StringBuilder();
            __tmp11.Append(endp.Address);
            using(StreamReader __tmp11Reader = new StreamReader(this.__ToStream(__tmp11.ToString())))
            {
                bool __tmp11_first = true;
                while(__tmp11_first || !__tmp11Reader.EndOfStream)
                {
                    __tmp11_first = false;
                    string __tmp11Line = __tmp11Reader.ReadLine();
                    if (__tmp11Line == null)
                    {
                        __tmp11Line = "";
                    }
                    __out.Append(__tmp9Prefix);
                    __out.Append(__tmp11Line);
                    __out.Append(__tmp10Suffix);
                    __out.AppendLine(); //231:26
                }
            }
            __out.Append("}"); //232:1
            __out.AppendLine(); //232:2
            return __out.ToString();
        }

    }
}