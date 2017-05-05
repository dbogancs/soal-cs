﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaDslx.Languages.Soal.Symbols; //4:1
using MetaDslx.Core; //5:1

namespace MetaDslx.Languages.Soal.Generator.Java //1:1
{
    using __Hidden_JavaEeGenerator_1426566488;
    namespace __Hidden_JavaEeGenerator_1426566488
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


    public class JavaEeGenerator //2:1
    {
        private object Instances; //2:1

        public JavaEeGenerator() //2:1
        {
        }

        public JavaEeGenerator(object instances) : this() //2:1
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

        public string Generate() //8:1
        {
            StringBuilder __out = new StringBuilder();
            return __out.ToString();
        }

        public string GenerateEntity(Struct e, ImmutableModelList<Struct> entities) //12:1
        {
            StringBuilder __out = new StringBuilder();
            bool __tmp2_outputWritten = false;
            string __tmp3_line = "package "; //13:1
            if (!string.IsNullOrEmpty(__tmp3_line))
            {
                __out.Append(__tmp3_line);
                __tmp2_outputWritten = true;
            }
            StringBuilder __tmp4 = new StringBuilder();
            __tmp4.Append(e.Namespace.Name.ToLower());
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
            string __tmp5_line = ";"; //13:37
            if (!string.IsNullOrEmpty(__tmp5_line))
            {
                __out.Append(__tmp5_line);
                __tmp2_outputWritten = true;
            }
            if (__tmp2_outputWritten) __out.AppendLine(true);
            if (__tmp2_outputWritten)
            {
                __out.AppendLine(false); //13:38
            }
            __out.AppendLine(true); //14:1
            __out.Append("import java.io.Serializable;"); //15:1
            __out.AppendLine(false); //15:29
            __out.Append("import javax.persistence.Column;"); //16:1
            __out.AppendLine(false); //16:33
            __out.Append("import javax.persistence.Entity;"); //17:1
            __out.AppendLine(false); //17:33
            __out.Append("import javax.persistence.GeneratedValue;"); //18:1
            __out.AppendLine(false); //18:41
            __out.Append("import javax.persistence.GenerationType;"); //19:1
            __out.AppendLine(false); //19:41
            __out.Append("import javax.persistence.Id;"); //20:1
            __out.AppendLine(false); //20:29
            __out.Append("import javax.persistence.Temporal;"); //21:1
            __out.AppendLine(false); //21:35
            __out.Append("import javax.persistence.TemporalType;"); //22:1
            __out.AppendLine(false); //22:39
            __out.Append("import javax.persistence.JoinColumn;"); //23:1
            __out.AppendLine(false); //23:37
            __out.Append("import javax.persistence.OneToOne;"); //24:1
            __out.AppendLine(false); //24:35
            __out.Append("import javax.persistence.OneToMany;"); //25:1
            __out.AppendLine(false); //25:36
            __out.Append("import javax.persistence.ManyToOne;"); //26:1
            __out.AppendLine(false); //26:36
            __out.Append("import javax.persistence.ManyToMany;"); //27:1
            __out.AppendLine(false); //27:37
            __out.Append("import java.util.List;"); //28:1
            __out.AppendLine(false); //28:23
            __out.Append("import java.util.Date;"); //29:1
            __out.AppendLine(false); //29:23
            bool __tmp7_outputWritten = false;
            StringBuilder __tmp8 = new StringBuilder();
            __tmp8.Append(addAttributeImports(e));
            using(StreamReader __tmp8Reader = new StreamReader(this.__ToStream(__tmp8.ToString())))
            {
                bool __tmp8_last = __tmp8Reader.EndOfStream;
                while(!__tmp8_last)
                {
                    string __tmp8_line = __tmp8Reader.ReadLine();
                    __tmp8_last = __tmp8Reader.EndOfStream;
                    if ((__tmp8_last && !string.IsNullOrEmpty(__tmp8_line)) || (!__tmp8_last && __tmp8_line != null))
                    {
                        __out.Append(__tmp8_line);
                        __tmp7_outputWritten = true;
                    }
                    if (!__tmp8_last || __tmp7_outputWritten) __out.AppendLine(true);
                }
            }
            if (__tmp7_outputWritten)
            {
                __out.AppendLine(false); //30:25
            }
            __out.AppendLine(true); //31:1
            __out.Append("@Entity"); //32:1
            __out.AppendLine(false); //32:8
            bool __tmp10_outputWritten = false;
            string __tmp11_line = "public class "; //33:1
            if (!string.IsNullOrEmpty(__tmp11_line))
            {
                __out.Append(__tmp11_line);
                __tmp10_outputWritten = true;
            }
            StringBuilder __tmp12 = new StringBuilder();
            __tmp12.Append(e.Name);
            using(StreamReader __tmp12Reader = new StreamReader(this.__ToStream(__tmp12.ToString())))
            {
                bool __tmp12_last = __tmp12Reader.EndOfStream;
                while(!__tmp12_last)
                {
                    string __tmp12_line = __tmp12Reader.ReadLine();
                    __tmp12_last = __tmp12Reader.EndOfStream;
                    if ((__tmp12_last && !string.IsNullOrEmpty(__tmp12_line)) || (!__tmp12_last && __tmp12_line != null))
                    {
                        __out.Append(__tmp12_line);
                        __tmp10_outputWritten = true;
                    }
                    if (!__tmp12_last) __out.AppendLine(true);
                }
            }
            string __tmp13_line = " implements Serializable {"; //33:22
            if (!string.IsNullOrEmpty(__tmp13_line))
            {
                __out.Append(__tmp13_line);
                __tmp10_outputWritten = true;
            }
            if (__tmp10_outputWritten) __out.AppendLine(true);
            if (__tmp10_outputWritten)
            {
                __out.AppendLine(false); //33:48
            }
            __out.Append("    private static final long serialVersionUID = 1L;"); //34:1
            __out.AppendLine(false); //34:53
            __out.Append("    @Id"); //35:1
            __out.AppendLine(false); //35:8
            __out.Append("    @GeneratedValue(strategy = GenerationType.AUTO)"); //36:1
            __out.AppendLine(false); //36:52
            __out.Append("    private Long id;"); //37:1
            __out.AppendLine(false); //37:21
            __out.AppendLine(true); //38:1
            var __loop1_results = 
                (from iprop in __Enumerate((e.Properties).GetEnumerator()) //39:8
                select new { iprop = iprop}
                ).ToList(); //39:3
            for (int __loop1_iteration = 0; __loop1_iteration < __loop1_results.Count; ++__loop1_iteration)
            {
                var __tmp14 = __loop1_results[__loop1_iteration];
                var iprop = __tmp14.iprop;
                bool __tmp16_outputWritten = false;
                string __tmp15Prefix = "	"; //41:1
                StringBuilder __tmp17 = new StringBuilder();
                __tmp17.Append(addAttributeAnnotations(e, iprop, entities));
                using(StreamReader __tmp17Reader = new StreamReader(this.__ToStream(__tmp17.ToString())))
                {
                    bool __tmp17_last = __tmp17Reader.EndOfStream;
                    while(!__tmp17_last)
                    {
                        string __tmp17_line = __tmp17Reader.ReadLine();
                        __tmp17_last = __tmp17Reader.EndOfStream;
                        if (!string.IsNullOrEmpty(__tmp15Prefix))
                        {
                            __out.Append(__tmp15Prefix);
                            __tmp16_outputWritten = true;
                        }
                        if ((__tmp17_last && !string.IsNullOrEmpty(__tmp17_line)) || (!__tmp17_last && __tmp17_line != null))
                        {
                            __out.Append(__tmp17_line);
                            __tmp16_outputWritten = true;
                        }
                        if (!__tmp17_last || __tmp16_outputWritten) __out.AppendLine(true);
                    }
                }
                if (__tmp16_outputWritten)
                {
                    __out.AppendLine(false); //41:47
                }
                bool __tmp19_outputWritten = false;
                string __tmp20_line = "	private "; //42:1
                if (!string.IsNullOrEmpty(__tmp20_line))
                {
                    __out.Append(__tmp20_line);
                    __tmp19_outputWritten = true;
                }
                StringBuilder __tmp21 = new StringBuilder();
                __tmp21.Append(tC(iprop.Type));
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
                            __tmp19_outputWritten = true;
                        }
                        if (!__tmp21_last) __out.AppendLine(true);
                    }
                }
                string __tmp22_line = " "; //42:26
                if (!string.IsNullOrEmpty(__tmp22_line))
                {
                    __out.Append(__tmp22_line);
                    __tmp19_outputWritten = true;
                }
                StringBuilder __tmp23 = new StringBuilder();
                __tmp23.Append(aC(iprop.Name));
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
                            __tmp19_outputWritten = true;
                        }
                        if (!__tmp23_last) __out.AppendLine(true);
                    }
                }
                string __tmp24_line = ";"; //42:43
                if (!string.IsNullOrEmpty(__tmp24_line))
                {
                    __out.Append(__tmp24_line);
                    __tmp19_outputWritten = true;
                }
                if (__tmp19_outputWritten) __out.AppendLine(true);
                if (__tmp19_outputWritten)
                {
                    __out.AppendLine(false); //42:44
                }
            }
            __out.AppendLine(true); //44:1
            __out.AppendLine(true); //45:1
            var __loop2_results = 
                (from iprop in __Enumerate((e.Properties).GetEnumerator()) //46:8
                select new { iprop = iprop}
                ).ToList(); //46:3
            for (int __loop2_iteration = 0; __loop2_iteration < __loop2_results.Count; ++__loop2_iteration)
            {
                var __tmp25 = __loop2_results[__loop2_iteration];
                var iprop = __tmp25.iprop;
                bool __tmp27_outputWritten = false;
                string __tmp28_line = "	public "; //47:1
                if (!string.IsNullOrEmpty(__tmp28_line))
                {
                    __out.Append(__tmp28_line);
                    __tmp27_outputWritten = true;
                }
                StringBuilder __tmp29 = new StringBuilder();
                __tmp29.Append(tC(iprop.Type));
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
                            __tmp27_outputWritten = true;
                        }
                        if (!__tmp29_last) __out.AppendLine(true);
                    }
                }
                string __tmp30_line = " get"; //47:25
                if (!string.IsNullOrEmpty(__tmp30_line))
                {
                    __out.Append(__tmp30_line);
                    __tmp27_outputWritten = true;
                }
                StringBuilder __tmp31 = new StringBuilder();
                __tmp31.Append(iprop.Name);
                using(StreamReader __tmp31Reader = new StreamReader(this.__ToStream(__tmp31.ToString())))
                {
                    bool __tmp31_last = __tmp31Reader.EndOfStream;
                    while(!__tmp31_last)
                    {
                        string __tmp31_line = __tmp31Reader.ReadLine();
                        __tmp31_last = __tmp31Reader.EndOfStream;
                        if ((__tmp31_last && !string.IsNullOrEmpty(__tmp31_line)) || (!__tmp31_last && __tmp31_line != null))
                        {
                            __out.Append(__tmp31_line);
                            __tmp27_outputWritten = true;
                        }
                        if (!__tmp31_last) __out.AppendLine(true);
                    }
                }
                string __tmp32_line = "() {"; //47:41
                if (!string.IsNullOrEmpty(__tmp32_line))
                {
                    __out.Append(__tmp32_line);
                    __tmp27_outputWritten = true;
                }
                if (__tmp27_outputWritten) __out.AppendLine(true);
                if (__tmp27_outputWritten)
                {
                    __out.AppendLine(false); //47:45
                }
                bool __tmp34_outputWritten = false;
                string __tmp35_line = "		return "; //48:1
                if (!string.IsNullOrEmpty(__tmp35_line))
                {
                    __out.Append(__tmp35_line);
                    __tmp34_outputWritten = true;
                }
                StringBuilder __tmp36 = new StringBuilder();
                __tmp36.Append(aC(iprop.Name));
                using(StreamReader __tmp36Reader = new StreamReader(this.__ToStream(__tmp36.ToString())))
                {
                    bool __tmp36_last = __tmp36Reader.EndOfStream;
                    while(!__tmp36_last)
                    {
                        string __tmp36_line = __tmp36Reader.ReadLine();
                        __tmp36_last = __tmp36Reader.EndOfStream;
                        if ((__tmp36_last && !string.IsNullOrEmpty(__tmp36_line)) || (!__tmp36_last && __tmp36_line != null))
                        {
                            __out.Append(__tmp36_line);
                            __tmp34_outputWritten = true;
                        }
                        if (!__tmp36_last) __out.AppendLine(true);
                    }
                }
                string __tmp37_line = ";"; //48:26
                if (!string.IsNullOrEmpty(__tmp37_line))
                {
                    __out.Append(__tmp37_line);
                    __tmp34_outputWritten = true;
                }
                if (__tmp34_outputWritten) __out.AppendLine(true);
                if (__tmp34_outputWritten)
                {
                    __out.AppendLine(false); //48:27
                }
                __out.Append("	}"); //49:1
                __out.AppendLine(false); //49:3
                bool __tmp39_outputWritten = false;
                string __tmp40_line = "	public void set"; //50:1
                if (!string.IsNullOrEmpty(__tmp40_line))
                {
                    __out.Append(__tmp40_line);
                    __tmp39_outputWritten = true;
                }
                StringBuilder __tmp41 = new StringBuilder();
                __tmp41.Append(iprop.Name);
                using(StreamReader __tmp41Reader = new StreamReader(this.__ToStream(__tmp41.ToString())))
                {
                    bool __tmp41_last = __tmp41Reader.EndOfStream;
                    while(!__tmp41_last)
                    {
                        string __tmp41_line = __tmp41Reader.ReadLine();
                        __tmp41_last = __tmp41Reader.EndOfStream;
                        if ((__tmp41_last && !string.IsNullOrEmpty(__tmp41_line)) || (!__tmp41_last && __tmp41_line != null))
                        {
                            __out.Append(__tmp41_line);
                            __tmp39_outputWritten = true;
                        }
                        if (!__tmp41_last) __out.AppendLine(true);
                    }
                }
                string __tmp42_line = "("; //50:29
                if (!string.IsNullOrEmpty(__tmp42_line))
                {
                    __out.Append(__tmp42_line);
                    __tmp39_outputWritten = true;
                }
                StringBuilder __tmp43 = new StringBuilder();
                __tmp43.Append(tC(iprop.Type));
                using(StreamReader __tmp43Reader = new StreamReader(this.__ToStream(__tmp43.ToString())))
                {
                    bool __tmp43_last = __tmp43Reader.EndOfStream;
                    while(!__tmp43_last)
                    {
                        string __tmp43_line = __tmp43Reader.ReadLine();
                        __tmp43_last = __tmp43Reader.EndOfStream;
                        if ((__tmp43_last && !string.IsNullOrEmpty(__tmp43_line)) || (!__tmp43_last && __tmp43_line != null))
                        {
                            __out.Append(__tmp43_line);
                            __tmp39_outputWritten = true;
                        }
                        if (!__tmp43_last) __out.AppendLine(true);
                    }
                }
                string __tmp44_line = " "; //50:46
                if (!string.IsNullOrEmpty(__tmp44_line))
                {
                    __out.Append(__tmp44_line);
                    __tmp39_outputWritten = true;
                }
                StringBuilder __tmp45 = new StringBuilder();
                __tmp45.Append(aC(iprop.Name));
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
                            __tmp39_outputWritten = true;
                        }
                        if (!__tmp45_last) __out.AppendLine(true);
                    }
                }
                string __tmp46_line = ") {"; //50:63
                if (!string.IsNullOrEmpty(__tmp46_line))
                {
                    __out.Append(__tmp46_line);
                    __tmp39_outputWritten = true;
                }
                if (__tmp39_outputWritten) __out.AppendLine(true);
                if (__tmp39_outputWritten)
                {
                    __out.AppendLine(false); //50:66
                }
                bool __tmp48_outputWritten = false;
                string __tmp49_line = "		this."; //51:1
                if (!string.IsNullOrEmpty(__tmp49_line))
                {
                    __out.Append(__tmp49_line);
                    __tmp48_outputWritten = true;
                }
                StringBuilder __tmp50 = new StringBuilder();
                __tmp50.Append(aC(iprop.Name));
                using(StreamReader __tmp50Reader = new StreamReader(this.__ToStream(__tmp50.ToString())))
                {
                    bool __tmp50_last = __tmp50Reader.EndOfStream;
                    while(!__tmp50_last)
                    {
                        string __tmp50_line = __tmp50Reader.ReadLine();
                        __tmp50_last = __tmp50Reader.EndOfStream;
                        if ((__tmp50_last && !string.IsNullOrEmpty(__tmp50_line)) || (!__tmp50_last && __tmp50_line != null))
                        {
                            __out.Append(__tmp50_line);
                            __tmp48_outputWritten = true;
                        }
                        if (!__tmp50_last) __out.AppendLine(true);
                    }
                }
                string __tmp51_line = " = "; //51:24
                if (!string.IsNullOrEmpty(__tmp51_line))
                {
                    __out.Append(__tmp51_line);
                    __tmp48_outputWritten = true;
                }
                StringBuilder __tmp52 = new StringBuilder();
                __tmp52.Append(aC(iprop.Name));
                using(StreamReader __tmp52Reader = new StreamReader(this.__ToStream(__tmp52.ToString())))
                {
                    bool __tmp52_last = __tmp52Reader.EndOfStream;
                    while(!__tmp52_last)
                    {
                        string __tmp52_line = __tmp52Reader.ReadLine();
                        __tmp52_last = __tmp52Reader.EndOfStream;
                        if ((__tmp52_last && !string.IsNullOrEmpty(__tmp52_line)) || (!__tmp52_last && __tmp52_line != null))
                        {
                            __out.Append(__tmp52_line);
                            __tmp48_outputWritten = true;
                        }
                        if (!__tmp52_last) __out.AppendLine(true);
                    }
                }
                string __tmp53_line = ";"; //51:43
                if (!string.IsNullOrEmpty(__tmp53_line))
                {
                    __out.Append(__tmp53_line);
                    __tmp48_outputWritten = true;
                }
                if (__tmp48_outputWritten) __out.AppendLine(true);
                if (__tmp48_outputWritten)
                {
                    __out.AppendLine(false); //51:44
                }
                __out.Append("	}"); //52:1
                __out.AppendLine(false); //52:3
                __out.AppendLine(true); //53:1
            }
            __out.AppendLine(true); //55:1
            __out.Append("    @Override"); //56:1
            __out.AppendLine(false); //56:14
            __out.Append("    public int hashCode() {"); //57:1
            __out.AppendLine(false); //57:28
            __out.Append("        int hash = 0;"); //58:1
            __out.AppendLine(false); //58:22
            __out.Append("        hash += (id != null ? id.hashCode() : 0);"); //59:1
            __out.AppendLine(false); //59:50
            __out.Append("        return hash;"); //60:1
            __out.AppendLine(false); //60:21
            __out.Append("    }"); //61:1
            __out.AppendLine(false); //61:6
            __out.AppendLine(true); //62:1
            __out.Append("    @Override"); //63:1
            __out.AppendLine(false); //63:14
            __out.Append("    public boolean equals(Object object) {"); //64:1
            __out.AppendLine(false); //64:43
            __out.Append("        // TODO: Warning - this method won't work in the case the id fields are not set"); //65:1
            __out.AppendLine(false); //65:88
            bool __tmp55_outputWritten = false;
            string __tmp56_line = "        if (!(object instanceof "; //66:1
            if (!string.IsNullOrEmpty(__tmp56_line))
            {
                __out.Append(__tmp56_line);
                __tmp55_outputWritten = true;
            }
            StringBuilder __tmp57 = new StringBuilder();
            __tmp57.Append(e.Name);
            using(StreamReader __tmp57Reader = new StreamReader(this.__ToStream(__tmp57.ToString())))
            {
                bool __tmp57_last = __tmp57Reader.EndOfStream;
                while(!__tmp57_last)
                {
                    string __tmp57_line = __tmp57Reader.ReadLine();
                    __tmp57_last = __tmp57Reader.EndOfStream;
                    if ((__tmp57_last && !string.IsNullOrEmpty(__tmp57_line)) || (!__tmp57_last && __tmp57_line != null))
                    {
                        __out.Append(__tmp57_line);
                        __tmp55_outputWritten = true;
                    }
                    if (!__tmp57_last) __out.AppendLine(true);
                }
            }
            string __tmp58_line = ")) {"; //66:41
            if (!string.IsNullOrEmpty(__tmp58_line))
            {
                __out.Append(__tmp58_line);
                __tmp55_outputWritten = true;
            }
            if (__tmp55_outputWritten) __out.AppendLine(true);
            if (__tmp55_outputWritten)
            {
                __out.AppendLine(false); //66:45
            }
            __out.Append("            return false;"); //67:1
            __out.AppendLine(false); //67:26
            __out.Append("        }"); //68:1
            __out.AppendLine(false); //68:10
            bool __tmp60_outputWritten = false;
            string __tmp59Prefix = "        "; //69:1
            StringBuilder __tmp61 = new StringBuilder();
            __tmp61.Append(e.Name);
            using(StreamReader __tmp61Reader = new StreamReader(this.__ToStream(__tmp61.ToString())))
            {
                bool __tmp61_last = __tmp61Reader.EndOfStream;
                while(!__tmp61_last)
                {
                    string __tmp61_line = __tmp61Reader.ReadLine();
                    __tmp61_last = __tmp61Reader.EndOfStream;
                    if (!string.IsNullOrEmpty(__tmp59Prefix))
                    {
                        __out.Append(__tmp59Prefix);
                        __tmp60_outputWritten = true;
                    }
                    if ((__tmp61_last && !string.IsNullOrEmpty(__tmp61_line)) || (!__tmp61_last && __tmp61_line != null))
                    {
                        __out.Append(__tmp61_line);
                        __tmp60_outputWritten = true;
                    }
                    if (!__tmp61_last) __out.AppendLine(true);
                }
            }
            string __tmp62_line = " other = ("; //69:17
            if (!string.IsNullOrEmpty(__tmp62_line))
            {
                __out.Append(__tmp62_line);
                __tmp60_outputWritten = true;
            }
            StringBuilder __tmp63 = new StringBuilder();
            __tmp63.Append(e.Name);
            using(StreamReader __tmp63Reader = new StreamReader(this.__ToStream(__tmp63.ToString())))
            {
                bool __tmp63_last = __tmp63Reader.EndOfStream;
                while(!__tmp63_last)
                {
                    string __tmp63_line = __tmp63Reader.ReadLine();
                    __tmp63_last = __tmp63Reader.EndOfStream;
                    if ((__tmp63_last && !string.IsNullOrEmpty(__tmp63_line)) || (!__tmp63_last && __tmp63_line != null))
                    {
                        __out.Append(__tmp63_line);
                        __tmp60_outputWritten = true;
                    }
                    if (!__tmp63_last) __out.AppendLine(true);
                }
            }
            string __tmp64_line = ") object;"; //69:35
            if (!string.IsNullOrEmpty(__tmp64_line))
            {
                __out.Append(__tmp64_line);
                __tmp60_outputWritten = true;
            }
            if (__tmp60_outputWritten) __out.AppendLine(true);
            if (__tmp60_outputWritten)
            {
                __out.AppendLine(false); //69:44
            }
            __out.Append("        if ((this.id == null && other.id != null) || (this.id != null && !this.id.equals(other.id))) {"); //70:1
            __out.AppendLine(false); //70:103
            __out.Append("            return false;"); //71:1
            __out.AppendLine(false); //71:26
            __out.Append("        }"); //72:1
            __out.AppendLine(false); //72:10
            __out.Append("        return true;"); //73:1
            __out.AppendLine(false); //73:21
            __out.Append("    }"); //74:1
            __out.AppendLine(false); //74:6
            __out.AppendLine(true); //75:1
            __out.Append("    @Override"); //76:1
            __out.AppendLine(false); //76:14
            __out.Append("    public String toString() {"); //77:1
            __out.AppendLine(false); //77:31
            bool __tmp66_outputWritten = false;
            string __tmp67_line = "        return \""; //78:1
            if (!string.IsNullOrEmpty(__tmp67_line))
            {
                __out.Append(__tmp67_line);
                __tmp66_outputWritten = true;
            }
            StringBuilder __tmp68 = new StringBuilder();
            __tmp68.Append(e.Namespace.Name);
            using(StreamReader __tmp68Reader = new StreamReader(this.__ToStream(__tmp68.ToString())))
            {
                bool __tmp68_last = __tmp68Reader.EndOfStream;
                while(!__tmp68_last)
                {
                    string __tmp68_line = __tmp68Reader.ReadLine();
                    __tmp68_last = __tmp68Reader.EndOfStream;
                    if ((__tmp68_last && !string.IsNullOrEmpty(__tmp68_line)) || (!__tmp68_last && __tmp68_line != null))
                    {
                        __out.Append(__tmp68_line);
                        __tmp66_outputWritten = true;
                    }
                    if (!__tmp68_last) __out.AppendLine(true);
                }
            }
            string __tmp69_line = "."; //78:35
            if (!string.IsNullOrEmpty(__tmp69_line))
            {
                __out.Append(__tmp69_line);
                __tmp66_outputWritten = true;
            }
            StringBuilder __tmp70 = new StringBuilder();
            __tmp70.Append(e.Name);
            using(StreamReader __tmp70Reader = new StreamReader(this.__ToStream(__tmp70.ToString())))
            {
                bool __tmp70_last = __tmp70Reader.EndOfStream;
                while(!__tmp70_last)
                {
                    string __tmp70_line = __tmp70Reader.ReadLine();
                    __tmp70_last = __tmp70Reader.EndOfStream;
                    if ((__tmp70_last && !string.IsNullOrEmpty(__tmp70_line)) || (!__tmp70_last && __tmp70_line != null))
                    {
                        __out.Append(__tmp70_line);
                        __tmp66_outputWritten = true;
                    }
                    if (!__tmp70_last) __out.AppendLine(true);
                }
            }
            StringBuilder __tmp71 = new StringBuilder();
            __tmp71.Append("[");
            using(StreamReader __tmp71Reader = new StreamReader(this.__ToStream(__tmp71.ToString())))
            {
                bool __tmp71_last = __tmp71Reader.EndOfStream;
                while(!__tmp71_last)
                {
                    string __tmp71_line = __tmp71Reader.ReadLine();
                    __tmp71_last = __tmp71Reader.EndOfStream;
                    if ((__tmp71_last && !string.IsNullOrEmpty(__tmp71_line)) || (!__tmp71_last && __tmp71_line != null))
                    {
                        __out.Append(__tmp71_line);
                        __tmp66_outputWritten = true;
                    }
                    if (!__tmp71_last) __out.AppendLine(true);
                }
            }
            string __tmp72_line = " id=\" + id + \" "; //78:49
            if (!string.IsNullOrEmpty(__tmp72_line))
            {
                __out.Append(__tmp72_line);
                __tmp66_outputWritten = true;
            }
            StringBuilder __tmp73 = new StringBuilder();
            __tmp73.Append("]");
            using(StreamReader __tmp73Reader = new StreamReader(this.__ToStream(__tmp73.ToString())))
            {
                bool __tmp73_last = __tmp73Reader.EndOfStream;
                while(!__tmp73_last)
                {
                    string __tmp73_line = __tmp73Reader.ReadLine();
                    __tmp73_last = __tmp73Reader.EndOfStream;
                    if ((__tmp73_last && !string.IsNullOrEmpty(__tmp73_line)) || (!__tmp73_last && __tmp73_line != null))
                    {
                        __out.Append(__tmp73_line);
                        __tmp66_outputWritten = true;
                    }
                    if (!__tmp73_last) __out.AppendLine(true);
                }
            }
            string __tmp74_line = "\";"; //78:69
            if (!string.IsNullOrEmpty(__tmp74_line))
            {
                __out.Append(__tmp74_line);
                __tmp66_outputWritten = true;
            }
            if (__tmp66_outputWritten) __out.AppendLine(true);
            if (__tmp66_outputWritten)
            {
                __out.AppendLine(false); //78:71
            }
            __out.Append("    }"); //79:1
            __out.AppendLine(false); //79:6
            __out.AppendLine(true); //80:1
            __out.Append("}"); //81:1
            __out.AppendLine(false); //81:2
            return __out.ToString();
        }

        public string GenerateEnum(MetaDslx.Languages.Soal.Symbols.Enum en) //84:1
        {
            StringBuilder __out = new StringBuilder();
            bool __tmp2_outputWritten = false;
            string __tmp3_line = "package "; //85:1
            if (!string.IsNullOrEmpty(__tmp3_line))
            {
                __out.Append(__tmp3_line);
                __tmp2_outputWritten = true;
            }
            StringBuilder __tmp4 = new StringBuilder();
            __tmp4.Append(en.Namespace.Name.ToLower());
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
            string __tmp5_line = ";"; //85:38
            if (!string.IsNullOrEmpty(__tmp5_line))
            {
                __out.Append(__tmp5_line);
                __tmp2_outputWritten = true;
            }
            if (__tmp2_outputWritten) __out.AppendLine(true);
            if (__tmp2_outputWritten)
            {
                __out.AppendLine(false); //85:39
            }
            __out.AppendLine(true); //86:1
            bool __tmp7_outputWritten = false;
            string __tmp8_line = "public enum "; //87:1
            if (!string.IsNullOrEmpty(__tmp8_line))
            {
                __out.Append(__tmp8_line);
                __tmp7_outputWritten = true;
            }
            StringBuilder __tmp9 = new StringBuilder();
            __tmp9.Append(en.Name);
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
            string __tmp10_line = " {"; //87:22
            if (!string.IsNullOrEmpty(__tmp10_line))
            {
                __out.Append(__tmp10_line);
                __tmp7_outputWritten = true;
            }
            if (__tmp7_outputWritten) __out.AppendLine(true);
            if (__tmp7_outputWritten)
            {
                __out.AppendLine(false); //87:24
            }
            int i = 0; //88:3
            var __loop3_results = 
                (from iprop in __Enumerate((en.MChildren).GetEnumerator()) //89:8
                select new { iprop = iprop}
                ).ToList(); //89:3
            for (int __loop3_iteration = 0; __loop3_iteration < __loop3_results.Count; ++__loop3_iteration)
            {
                var __tmp11 = __loop3_results[__loop3_iteration];
                var iprop = __tmp11.iprop;
                bool __tmp13_outputWritten = false;
                string __tmp12Prefix = "	"; //90:1
                StringBuilder __tmp14 = new StringBuilder();
                __tmp14.Append(aC(iprop.MName));
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
                            __tmp13_outputWritten = true;
                        }
                        if ((__tmp14_last && !string.IsNullOrEmpty(__tmp14_line)) || (!__tmp14_last && __tmp14_line != null))
                        {
                            __out.Append(__tmp14_line);
                            __tmp13_outputWritten = true;
                        }
                        if (!__tmp14_last) __out.AppendLine(true);
                    }
                }
                if (i != en.MChildren.Count - 1) //90:20
                {
                    string __tmp16_line = ","; //90:48
                    if (!string.IsNullOrEmpty(__tmp16_line))
                    {
                        __out.Append(__tmp16_line);
                        __tmp13_outputWritten = true;
                    }
                }
                if (__tmp13_outputWritten) __out.AppendLine(true);
                if (__tmp13_outputWritten)
                {
                    __out.AppendLine(false); //90:57
                }
                i = i + 1;
            }
            __out.Append("}"); //93:1
            __out.AppendLine(false); //93:2
            return __out.ToString();
        }

        public string addAttributeImports(Struct e) //96:1
        {
            StringBuilder __out = new StringBuilder();
            var __loop4_results = 
                (from iprop in __Enumerate((e.Properties).GetEnumerator()) //97:8
                select new { iprop = iprop}
                ).ToList(); //97:3
            for (int __loop4_iteration = 0; __loop4_iteration < __loop4_results.Count; ++__loop4_iteration)
            {
                var __tmp1 = __loop4_results[__loop4_iteration];
                var iprop = __tmp1.iprop;
                if (iprop.Type.MMetaClass.Name.Equals("ArrayType")) //98:4
                {
                }
                else if (iprop.Type.MName.Equals("TimeSpan")) //100:4
                {
                }
            }
            return __out.ToString();
        }

        public string addAttributeAnnotations(Struct mainEntity, Property mainProp, ImmutableModelList<Struct> allEntities) //107:1
        {
            StringBuilder __out = new StringBuilder();
            bool fromOne = false; //108:3
            bool fromMany = false; //109:3
            bool toOne = false; //110:3
            bool toMany = false; //111:3
            bool isJoin = false; //112:3
            String mappedBy = null; //113:3
            if (mainProp.Type != null) //114:3
            {
                if (mainProp.Type.MName != null) //115:4
                {
                    if (mainProp.Type.MName.Equals("TimeSpan")) //116:5
                    {
                        __out.Append("@Temporal(TemporalType.TIMESTAMP)"); //117:1
                        __out.AppendLine(false); //117:34
                    }
                    else if (false) //118:5
                    {
                    }
                    else //119:5
                    {
                    }
                }
                if (true) //123:4
                {
                    var __loop5_results = 
                        (from entity in __Enumerate((allEntities).GetEnumerator()) //124:10
                        select new { entity = entity}
                        ).ToList(); //124:5
                    for (int __loop5_iteration = 0; __loop5_iteration < __loop5_results.Count; ++__loop5_iteration)
                    {
                        var __tmp1 = __loop5_results[__loop5_iteration];
                        var entity = __tmp1.entity;
                        var __loop6_results = 
                            (from eprop in __Enumerate((entity.Properties).GetEnumerator()) //125:11
                            select new { eprop = eprop}
                            ).ToList(); //125:6
                        for (int __loop6_iteration = 0; __loop6_iteration < __loop6_results.Count; ++__loop6_iteration)
                        {
                            var __tmp2 = __loop6_results[__loop6_iteration];
                            var eprop = __tmp2.eprop;
                            if (eprop.Type.MName != null && eprop.Type.MName.Equals(mainEntity.Name)) //126:7
                            {
                                fromOne = true;
                                mappedBy = eprop.Name;
                            }
                            else if (eprop.Type.MMetaClass.MName.Equals("ArrayType")) //129:7
                            {
                                ArrayType at = (ArrayType)eprop.Type; //130:8
                                if (at.InnerType != null && at.InnerType.MName.Equals(mainEntity.Name)) //131:8
                                {
                                    fromMany = true;
                                    mappedBy = eprop.Name;
                                }
                            }
                        }
                    }
                    var __loop7_results = 
                        (from entity in __Enumerate((allEntities).GetEnumerator()) //138:10
                        select new { entity = entity}
                        ).ToList(); //138:5
                    for (int __loop7_iteration = 0; __loop7_iteration < __loop7_results.Count; ++__loop7_iteration)
                    {
                        var __tmp3 = __loop7_results[__loop7_iteration];
                        var entity = __tmp3.entity;
                        if (mainProp.Type.MName != null && mainProp.Type.MName.Equals(entity.Name)) //139:6
                        {
                            toOne = true;
                        }
                        else if (mainProp.Type.MMetaClass.MName.Equals("ArrayType")) //141:6
                        {
                            ArrayType at = (ArrayType)mainProp.Type; //142:7
                            if (at.InnerType != null && at.InnerType.MName.Equals(entity.Name)) //143:7
                            {
                                toMany = true;
                            }
                        }
                    }
                    isJoin = (toOne || toMany || fromOne || fromMany);
                    if (false) //149:5
                    {
                    }
                    else if (fromOne && toOne) //150:5
                    {
                        __out.Append("@OneToOne"); //151:1
                        __out.AppendLine(false); //151:10
                        bool __tmp5_outputWritten = false;
                        string __tmp6_line = "@JoinColumn(name = \""; //152:1
                        if (!string.IsNullOrEmpty(__tmp6_line))
                        {
                            __out.Append(__tmp6_line);
                            __tmp5_outputWritten = true;
                        }
                        StringBuilder __tmp7 = new StringBuilder();
                        __tmp7.Append(cC(mainProp.Name));
                        using(StreamReader __tmp7Reader = new StreamReader(this.__ToStream(__tmp7.ToString())))
                        {
                            bool __tmp7_last = __tmp7Reader.EndOfStream;
                            while(!__tmp7_last)
                            {
                                string __tmp7_line = __tmp7Reader.ReadLine();
                                __tmp7_last = __tmp7Reader.EndOfStream;
                                if ((__tmp7_last && !string.IsNullOrEmpty(__tmp7_line)) || (!__tmp7_last && __tmp7_line != null))
                                {
                                    __out.Append(__tmp7_line);
                                    __tmp5_outputWritten = true;
                                }
                                if (!__tmp7_last) __out.AppendLine(true);
                            }
                        }
                        string __tmp8_line = "\")"; //152:40
                        if (!string.IsNullOrEmpty(__tmp8_line))
                        {
                            __out.Append(__tmp8_line);
                            __tmp5_outputWritten = true;
                        }
                        if (__tmp5_outputWritten) __out.AppendLine(true);
                        if (__tmp5_outputWritten)
                        {
                            __out.AppendLine(false); //152:42
                        }
                    }
                    else if (fromOne && toMany) //153:5
                    {
                        bool __tmp10_outputWritten = false;
                        string __tmp11_line = "@OneToMany(mappedBy=\""; //154:1
                        if (!string.IsNullOrEmpty(__tmp11_line))
                        {
                            __out.Append(__tmp11_line);
                            __tmp10_outputWritten = true;
                        }
                        StringBuilder __tmp12 = new StringBuilder();
                        __tmp12.Append(mappedBy);
                        using(StreamReader __tmp12Reader = new StreamReader(this.__ToStream(__tmp12.ToString())))
                        {
                            bool __tmp12_last = __tmp12Reader.EndOfStream;
                            while(!__tmp12_last)
                            {
                                string __tmp12_line = __tmp12Reader.ReadLine();
                                __tmp12_last = __tmp12Reader.EndOfStream;
                                if ((__tmp12_last && !string.IsNullOrEmpty(__tmp12_line)) || (!__tmp12_last && __tmp12_line != null))
                                {
                                    __out.Append(__tmp12_line);
                                    __tmp10_outputWritten = true;
                                }
                                if (!__tmp12_last) __out.AppendLine(true);
                            }
                        }
                        string __tmp13_line = "\")"; //154:32
                        if (!string.IsNullOrEmpty(__tmp13_line))
                        {
                            __out.Append(__tmp13_line);
                            __tmp10_outputWritten = true;
                        }
                        if (__tmp10_outputWritten) __out.AppendLine(true);
                        if (__tmp10_outputWritten)
                        {
                            __out.AppendLine(false); //154:34
                        }
                        bool __tmp15_outputWritten = false;
                        string __tmp16_line = "@JoinColumn(name = \""; //155:1
                        if (!string.IsNullOrEmpty(__tmp16_line))
                        {
                            __out.Append(__tmp16_line);
                            __tmp15_outputWritten = true;
                        }
                        StringBuilder __tmp17 = new StringBuilder();
                        __tmp17.Append(cC(mainProp.Name));
                        using(StreamReader __tmp17Reader = new StreamReader(this.__ToStream(__tmp17.ToString())))
                        {
                            bool __tmp17_last = __tmp17Reader.EndOfStream;
                            while(!__tmp17_last)
                            {
                                string __tmp17_line = __tmp17Reader.ReadLine();
                                __tmp17_last = __tmp17Reader.EndOfStream;
                                if ((__tmp17_last && !string.IsNullOrEmpty(__tmp17_line)) || (!__tmp17_last && __tmp17_line != null))
                                {
                                    __out.Append(__tmp17_line);
                                    __tmp15_outputWritten = true;
                                }
                                if (!__tmp17_last) __out.AppendLine(true);
                            }
                        }
                        string __tmp18_line = "\")"; //155:40
                        if (!string.IsNullOrEmpty(__tmp18_line))
                        {
                            __out.Append(__tmp18_line);
                            __tmp15_outputWritten = true;
                        }
                        if (__tmp15_outputWritten) __out.AppendLine(true);
                        if (__tmp15_outputWritten)
                        {
                            __out.AppendLine(false); //155:42
                        }
                    }
                    else if (fromMany && toOne) //156:5
                    {
                        __out.Append("@ManyToOne"); //157:1
                        __out.AppendLine(false); //157:11
                        bool __tmp20_outputWritten = false;
                        string __tmp21_line = "@JoinColumn(name = \""; //158:1
                        if (!string.IsNullOrEmpty(__tmp21_line))
                        {
                            __out.Append(__tmp21_line);
                            __tmp20_outputWritten = true;
                        }
                        StringBuilder __tmp22 = new StringBuilder();
                        __tmp22.Append(cC(mainProp.Name));
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
                                    __tmp20_outputWritten = true;
                                }
                                if (!__tmp22_last) __out.AppendLine(true);
                            }
                        }
                        string __tmp23_line = "\")"; //158:40
                        if (!string.IsNullOrEmpty(__tmp23_line))
                        {
                            __out.Append(__tmp23_line);
                            __tmp20_outputWritten = true;
                        }
                        if (__tmp20_outputWritten) __out.AppendLine(true);
                        if (__tmp20_outputWritten)
                        {
                            __out.AppendLine(false); //158:42
                        }
                    }
                    else if (fromMany && toMany) //159:5
                    {
                        __out.Append("@ManyToMany"); //160:1
                        __out.AppendLine(false); //160:12
                        bool __tmp25_outputWritten = false;
                        string __tmp26_line = "@JoinColumn(name = \""; //161:1
                        if (!string.IsNullOrEmpty(__tmp26_line))
                        {
                            __out.Append(__tmp26_line);
                            __tmp25_outputWritten = true;
                        }
                        StringBuilder __tmp27 = new StringBuilder();
                        __tmp27.Append(cC(mainProp.Name));
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
                                    __tmp25_outputWritten = true;
                                }
                                if (!__tmp27_last) __out.AppendLine(true);
                            }
                        }
                        string __tmp28_line = "\")"; //161:40
                        if (!string.IsNullOrEmpty(__tmp28_line))
                        {
                            __out.Append(__tmp28_line);
                            __tmp25_outputWritten = true;
                        }
                        if (__tmp25_outputWritten) __out.AppendLine(true);
                        if (__tmp25_outputWritten)
                        {
                            __out.AppendLine(false); //161:42
                        }
                    }
                    else //162:5
                    {
                        bool __tmp30_outputWritten = false;
                        string __tmp31_line = "@Column(name = \""; //163:1
                        if (!string.IsNullOrEmpty(__tmp31_line))
                        {
                            __out.Append(__tmp31_line);
                            __tmp30_outputWritten = true;
                        }
                        StringBuilder __tmp32 = new StringBuilder();
                        __tmp32.Append(cC(mainProp.Name));
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
                                    __tmp30_outputWritten = true;
                                }
                                if (!__tmp32_last) __out.AppendLine(true);
                            }
                        }
                        string __tmp33_line = "\")"; //163:36
                        if (!string.IsNullOrEmpty(__tmp33_line))
                        {
                            __out.Append(__tmp33_line);
                            __tmp30_outputWritten = true;
                        }
                        if (__tmp30_outputWritten) __out.AppendLine(true);
                        if (__tmp30_outputWritten)
                        {
                            __out.AppendLine(false); //163:38
                        }
                    }
                }
            }
            return __out.ToString();
        }

        public string aC(String attributeName) //170:1
        {
            return Char.ToLowerInvariant(attributeName[0]) + attributeName.Substring(1); //171:2
        }

        public string cC(String columnName) //174:1
        {
            return columnName.ToUpper(); //175:2
        }

        public string tC(SoalType t) //178:1
        {
            if (t.MName != null) //179:2
            {
                if (t.MName.Equals("TimeSpan")) //180:3
                {
                    return "Date"; //181:4
                }
                else if (t.MName.Equals("string")) //182:3
                {
                    return "String"; //183:4
                }
                else if (t.MName.Equals("DateTime")) //184:3
                {
                    return "Date"; //185:4
                }
                else //186:3
                {
                    return t.MName; //187:4
                }
            }
            else //189:2
            {
                ArrayType at = (ArrayType)t; //190:3
                if (at.MMetaClass.Name.Equals("ArrayType") && at.InnerType != null && !at.InnerType.MName.Equals(null)) //191:3
                {
                    return "List<" + at.InnerType.MName + ">"; //192:4
                }
                else //193:3
                {
                    return ""; //194:4
                }
            }
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
