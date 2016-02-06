﻿using MetaDslx.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MetaDslx.Soal
{
    internal class WsdlMessagePart
    {
        public string Name { get; set; }
        public SoalType Type { get; set; }
        public SoalType OriginalType { get; set; }
    }

    internal class WsdlMessage
    {
        public string Name { get; set; }
        public List<WsdlMessagePart> Parts { get; private set; }
        public bool Wrapped { get; set; }
        public bool Rpc { get; set; }
        public bool Document { get; set; }
        public string ElementName { get; set; }

        public WsdlMessage()
        {
            this.Parts = new List<WsdlMessagePart>();
        }
    }



    internal class WsdlReader : XmlReader
    {
        public const int PhaseCount = 4;

        public const string WsdlNamespace = "http://schemas.xmlsoap.org/wsdl/";
        public const string WsdlSoap11Namespace = "http://schemas.xmlsoap.org/wsdl/soap/";
        public const string WsdlSoap12Namespace = "http://schemas.xmlsoap.org/wsdl/soap12/";
        public const string WspNamespace = "http://www.w3.org/ns/ws-policy";
        public const string WsuNamespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
        public const string HttpTransportUri = "http://schemas.xmlsoap.org/soap/http";

        public const string WsawNamespace = "http://www.w3.org/2006/05/addressing/wsdl";
        public const string WsamNamespace = "http://www.w3.org/2007/05/addressing/metadata";
        public const string WsomaNamespace = "http://schemas.xmlsoap.org/ws/2004/09/policy/optimizedmimeserialization";
        public const string Sp1Namespace = "http://schemas.xmlsoap.org/ws/2005/07/securitypolicy";
        public const string Sp2Namespace = "http://docs.oasis-open.org/ws-sx/ws-securitypolicy/200702";

        private XNamespace xsd;
        private XNamespace wsdl;
        private XNamespace wsp;
        private XNamespace wsu;
        private XNamespace soap11;
        private XNamespace soap12;
        private XNamespace tns;

        private XNamespace wsaw;
        private XNamespace wsam;
        private XNamespace wsoma;
        private XNamespace sp1;
        private XNamespace sp2;

        public WsdlReader(SoalImporter importer, XElement root, string uri)
            : base(importer, root, uri)
        {
        }

        public override void CollectImportedFiles()
        {
            xsd = XsdReader.XsdNamespace;
            wsdl = WsdlReader.WsdlNamespace;
            wsp = WsdlReader.WspNamespace;
            wsu = WsdlReader.WsuNamespace;
            soap11 = WsdlReader.WsdlSoap11Namespace;
            soap12 = WsdlReader.WsdlSoap12Namespace;

            wsaw = WsdlReader.WsawNamespace;
            wsam = WsdlReader.WsamNamespace;
            wsoma = WsdlReader.WsomaNamespace;
            sp1 = WsdlReader.Sp1Namespace;
            sp2 = WsdlReader.Sp2Namespace;

            XAttribute tnsAttr = this.Root.Attribute("targetNamespace");
            if (tnsAttr == null)
            {
                this.Importer.Diagnostics.AddError("The attribute 'targetNamespace' is missing from the root node of the XML.", this.Uri, this.GetTextSpan(this.Root));
                return;
            }
            tns = tnsAttr.Value;
            this.Namespace = this.Importer.CreateNamespace(this, tnsAttr.Value, null, null);
            foreach (var elem in this.Root.Elements())
            {
                if (elem.Name.NamespaceName == WsdlReader.WsdlNamespace)
                {
                    if (elem.Name.LocalName == "import")
                    {
                        XAttribute locAttribute = elem.Attribute("location");
                        if (locAttribute != null)
                        {
                            string location = locAttribute.Value;
                            this.Importer.ImportRelativeFile(this.Uri, location);
                        }
                        else
                        {
                            this.Importer.Diagnostics.AddError("Attribute 'location' is missing from import.", this.Uri, this.GetTextSpan(elem));
                        }
                    }
                    else if (elem.Name.LocalName == "types")
                    {
                        foreach (var schema in elem.Elements(xsd + "schema"))
                        {
                            this.Importer.ImportXml(schema, this.Uri);
                        }
                    }
                }
                else
                {
                    // ignore
                }
            }
        }

        public override void LoadWsdlFile(int phase)
        {
            //if (this.Importer.Diagnostics.HasErrors()) return;
            switch (phase)
            {
                case 0:
                    this.ImportPhase5();
                    break;
                case 1:
                    this.ImportPhase6();
                    break;
                case 2:
                    this.ImportPhase7();
                    break;
                case 3:
                    this.ImportPhase8();
                    break;
                default:
                    break;
            }
        }

        private void ImportPhase5()
        {
            foreach (var elem in this.Root.Elements())
            {
                if (elem.Name.NamespaceName == WsdlReader.WsdlNamespace)
                {
                    if (elem.Name.LocalName == "message")
                    {
                        XAttribute nameAttr = elem.Attribute("name");
                        if (nameAttr != null)
                        {
                            WsdlMessage msg = new WsdlMessage();
                            msg.Name = nameAttr.Value;
                            if (this.Importer.WsdlMessages.Register(this, tns + msg.Name, elem, msg) != null)
                            {
                                foreach (var partElem in elem.Elements(wsdl+"part"))
                                {
                                    SoalType partType = null;
                                    XAttribute partNameAttr = partElem.Attribute("name");
                                    XAttribute partXsdTypeAttr = partElem.Attribute("type");
                                    XAttribute partXsdElemAttr = partElem.Attribute("element");
                                    if (partNameAttr != null)
                                    {
                                        if (partXsdElemAttr != null)
                                        {
                                            msg.Document = true;
                                            XName partTypeElemName = this.GetXName(partElem, partXsdElemAttr.Value);
                                            if (msg.ElementName == null)
                                            {
                                                msg.ElementName = partTypeElemName.LocalName;
                                                msg.Wrapped = true;
                                            }
                                            else
                                            {
                                                msg.Wrapped = false;
                                            }
                                            partType = this.Importer.WsdlElements.Get(this.GetXName(partElem, partXsdElemAttr.Value));
                                        }
                                        else if (partXsdTypeAttr != null)
                                        {
                                            msg.Rpc = true;
                                            XName typeRefName = this.GetXName(partElem, partXsdTypeAttr.Value);
                                            if (typeRefName != null)
                                            {
                                                //partType = this.Importer.ResolveXsdType(typeRefName.NamespaceName, typeRefName.LocalName);
                                                partType = this.Importer.WsdlTypes.Get(typeRefName);
                                            }
                                            if (partType == null)
                                            {
                                                partType = this.Importer.ResolveXsdPrimitiveType(typeRefName);
                                            }
                                        }
                                        if (partType == null)
                                        {
                                            this.Importer.Diagnostics.AddError("Could not resolve the type of the message part.", this.Uri, this.GetTextSpan(partElem));
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        this.Importer.Diagnostics.AddError("Message part has no 'name' attribute.", this.Uri, this.GetTextSpan(partElem));
                                        continue;
                                    }
                                    WsdlMessagePart part = new WsdlMessagePart();
                                    part.Name = partNameAttr.Value;
                                    part.OriginalType = partType;
                                    part.Type = this.Importer.ResolveXsdReplacementType(partType);
                                    msg.Parts.Add(part);
                                }
                            }
                            if (msg.Parts.Count != 1)
                            {
                                msg.Wrapped = false;
                            }
                        }
                        else
                        {
                            this.Importer.Diagnostics.AddError("The message has no 'name' attribute.", this.Uri, this.GetTextSpan(elem));
                            continue;
                        }
                    }
                }
                if (elem.Name.NamespaceName == WsdlReader.WspNamespace)
                {
                    if (elem.Name.LocalName == "Policy")
                    {
                        Binding policy = SoalFactory.Instance.CreateBinding();
                        this.ImportPolicy(elem, policy);
                    }
                }
            }
        }

        private void ImportPhase6()
        {
            foreach (var elem in this.Root.Elements())
            {
                if (elem.Name.NamespaceName == WsdlReader.WsdlNamespace)
                {
                    if (elem.Name.LocalName == "portType")
                    {
                        XAttribute nameAttr = elem.Attribute("name");
                        if (nameAttr != null)
                        {
                            string name = nameAttr.Value;
                            Interface intf = SoalFactory.Instance.CreateInterface();
                            intf.Name = name;
                            intf.Namespace = this.Namespace;
                            this.Importer.WsdlPortTypes.Register(this, this.tns + name, elem, intf);
                            int count = 0;
                            int rpc = 0;
                            int document = 0;
                            int wrapped = 0;
                            foreach (var opElem in elem.Elements(wsdl + "operation"))
                            {
                                XAttribute opNameAttr = opElem.Attribute("name");
                                if (opNameAttr != null)
                                {
                                    ++count;
                                    string opName = opNameAttr.Value;
                                    bool opRpc = false;
                                    bool opDocument = false;
                                    bool opWrapped = false;
                                    bool opNotWrapped = false;
                                    XElement inputElem = opElem.Element(wsdl + "input");
                                    XElement outputElem = opElem.Element(wsdl + "output");
                                    List<XElement> faultElems = opElem.Elements(wsdl + "fault").ToList();
                                    WsdlMessage inputMsg = null;
                                    WsdlMessage outputMsg = null;
                                    List<WsdlMessage> faultMsgs = new List<WsdlMessage>();
                                    if (inputElem != null)
                                    {
                                        XAttribute msgAttr = inputElem.Attribute("message");
                                        if (msgAttr != null)
                                        {
                                            inputMsg = this.Importer.WsdlMessages.Get(this.GetXName(inputElem, msgAttr.Value));
                                        }
                                        if (inputMsg != null)
                                        {
                                            if (inputMsg.Document) opDocument = true;
                                            if (inputMsg.Rpc) opRpc = true;
                                            if (inputMsg.Wrapped && inputMsg.ElementName == opName) opWrapped = true;
                                            else opNotWrapped = true;
                                        }
                                        else
                                        {
                                            this.Importer.Diagnostics.AddError("Could not resolve the input message.", this.Uri, this.GetTextSpan(inputElem));
                                            continue;
                                        }
                                    }
                                    if (outputElem != null)
                                    {
                                        XAttribute msgAttr = outputElem.Attribute("message");
                                        if (msgAttr != null)
                                        {
                                            outputMsg = this.Importer.WsdlMessages.Get(this.GetXName(outputElem, msgAttr.Value));
                                        }
                                        if (outputMsg != null)
                                        {
                                            if (outputMsg.Document) opDocument = true;
                                            if (outputMsg.Rpc) opRpc = true;
                                            if (outputMsg.Wrapped && outputMsg.ElementName == opName+"Response") opWrapped = true;
                                            else opNotWrapped = true;
                                            if (outputMsg.Parts.Count == 1 && outputMsg.Parts[0].Type is Struct && ((Struct)outputMsg.Parts[0].Type).Properties.Count > 1)
                                            {
                                                opNotWrapped = true;
                                            }
                                            if (outputMsg.Parts.Count == 1 && outputMsg.Parts[0].Type is Struct && ((Struct)outputMsg.Parts[0].Type).Properties.Count == 1 && ((Struct)outputMsg.Parts[0].Type).Properties[0].Name != opName+"Result")
                                            {
                                                opNotWrapped = true;
                                            }
                                        }
                                        else
                                        {
                                            this.Importer.Diagnostics.AddError("Could not resolve the output message.", this.Uri, this.GetTextSpan(outputElem));
                                            continue;
                                        }
                                    }
                                    foreach (var faultElem in faultElems)
                                    {
                                        XAttribute msgAttr = faultElem.Attribute("message");
                                        WsdlMessage faultMsg = null;
                                        if (msgAttr != null)
                                        {
                                            faultMsg = this.Importer.WsdlMessages.Get(this.GetXName(faultElem, msgAttr.Value));
                                        }
                                        if (faultMsg != null)
                                        {
                                            if (faultMsg.Document) opDocument = true;
                                            if (faultMsg.Rpc) opRpc = true;
                                            if (faultMsg.Wrapped) opWrapped = true;
                                            else opNotWrapped = true;
                                        }
                                        else
                                        {
                                            this.Importer.Diagnostics.AddError("Could not resolve the fault message.", this.Uri, this.GetTextSpan(faultElem));
                                        }
                                        faultMsgs.Add(faultMsg);
                                    }
                                    if (opWrapped && opNotWrapped)
                                    {
                                        opWrapped = false;
                                    }
                                    if (opDocument && opRpc)
                                    {
                                        opDocument = false;
                                        //this.Importer.Diagnostics.AddError("The operation has both document and RPC style messages. Use either of the styles but not both.", this.Uri, this.GetTextSpan(opElem));
                                        //continue;
                                    }
                                    Operation op = SoalFactory.Instance.CreateOperation();
                                    op.Name = opName;
                                    intf.Operations.Add(op);
                                    if (opDocument)
                                    {
                                        ++document;
                                        if (opWrapped)
                                        {
                                            ++wrapped;
                                            WsdlMessagePart part;
                                            Struct st;
                                            if (inputMsg != null)
                                            {
                                                part = null;
                                                st = null;
                                                if (inputMsg.Parts.Count == 1)
                                                {
                                                    part = inputMsg.Parts[0];
                                                    if (part != null)
                                                    {
                                                        st = part.OriginalType as Struct;
                                                    }
                                                }
                                                if (st != null)
                                                {
                                                    foreach (var prop in st.Properties)
                                                    {
                                                        Parameter param = SoalFactory.Instance.CreateParameter();
                                                        param.Name = prop.Name;
                                                        param.Type = prop.Type;
                                                        this.Importer.Reference(param.Type);
                                                        SoalImporter.CopyAnnotations(prop, param);
                                                        op.Parameters.Add(param);
                                                    }
                                                    this.Importer.RemoveType(st);
                                                }
                                                else
                                                {
                                                    this.Importer.Diagnostics.AddError("The input message part should be of 'complexType'.", this.Uri, this.GetTextSpan(opElem));
                                                    op.ReturnType = SoalInstance.Void;
                                                    continue;
                                                }
                                            }
                                            if (outputMsg != null)
                                            {
                                                part = null;
                                                st = null;
                                                if (outputMsg.Parts.Count == 1)
                                                {
                                                    part = outputMsg.Parts[0];
                                                    if (part != null)
                                                    {
                                                        st = part.OriginalType as Struct;
                                                    }
                                                }
                                                if (st != null && st.Properties.Count > 0)
                                                {
                                                    if (st.Properties.Count > 1)
                                                    {
                                                        this.Importer.Diagnostics.AddError("The output message should have a single '" + op.Name + "Result' element under the '" + op.Name + "Response' element.", this.Uri, this.GetTextSpan(opElem));
                                                        op.ReturnType = SoalInstance.Void;
                                                        continue;
                                                    }
                                                    Property prop = st.Properties[0];
                                                    if (prop.Name != op.Name + "Result")
                                                    {
                                                        this.Importer.Diagnostics.AddWarning("The output message should have a single '" + op.Name + "Result' element under the '" + op.Name + "Response' element.", this.Uri, this.GetTextSpan(opElem));
                                                    }
                                                    op.ReturnType = prop.Type;
                                                    this.Importer.Reference(op.ReturnType);
                                                    Annotation elemAnnot = prop.GetAnnotation(SoalAnnotations.Element);
                                                    if (elemAnnot != null)
                                                    {
                                                        op.ReturnAnnotations.Add(SoalImporter.CloneAnnotation(elemAnnot));
                                                    }
                                                    this.Importer.RemoveType(st);
                                                }
                                                else if (st != null)
                                                {
                                                    op.ReturnType = SoalInstance.Void;
                                                    this.Importer.RemoveType(st);
                                                }
                                                else
                                                {
                                                    this.Importer.Diagnostics.AddError("The output message part should be of 'complexType'.", this.Uri, this.GetTextSpan(opElem));
                                                    op.ReturnType = SoalInstance.Void;
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                op.IsOneway = true;
                                            }
                                            foreach (var faultMsg in faultMsgs)
                                            {
                                                part = null;
                                                st = null;
                                                if (faultMsg.Parts.Count == 1)
                                                {
                                                    part = faultMsg.Parts[0];
                                                    if (part != null)
                                                    {
                                                        st = part.OriginalType as Struct;
                                                    }
                                                }
                                                if (st != null)
                                                {
                                                    op.Exceptions.Add(st);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (inputMsg != null)
                                            {
                                                foreach (var part in inputMsg.Parts)
                                                {
                                                    Parameter param = SoalFactory.Instance.CreateParameter();
                                                    param.Name = part.Name;
                                                    param.Type = part.Type;
                                                    this.Importer.Reference(param.Type);
                                                    op.Parameters.Add(param);
                                                    if (part.OriginalType is Struct)
                                                    {
                                                        object origWrapped = ((Struct)part.OriginalType).GetAnnotationPropertyValue(SoalAnnotations.Type, SoalAnnotationProperties.Wrapped) ?? false;
                                                        if ((bool)origWrapped)
                                                        {
                                                            SoalImporter.CopyAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Wrapped, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Wrapped, param);
                                                            SoalImporter.CopyAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Items, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Items, param);
                                                            SoalImporter.CopyAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Sap, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Sap, param);
                                                        }
                                                    }
                                                }
                                            }
                                            if (outputMsg != null)
                                            {
                                                if (outputMsg.Parts.Count == 1)
                                                {
                                                    WsdlMessagePart part = outputMsg.Parts[0];
                                                    op.ReturnType = part.Type;
                                                    this.Importer.Reference(op.ReturnType);
                                                    if (part.OriginalType is Struct)
                                                    {
                                                        object origWrapped = ((Struct)part.OriginalType).GetAnnotationPropertyValue(SoalAnnotations.Type, SoalAnnotationProperties.Wrapped) ?? false;
                                                        if ((bool)origWrapped)
                                                        {
                                                            SoalImporter.CopyReturnAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Wrapped, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Wrapped, op);
                                                            SoalImporter.CopyReturnAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Items, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Items, op);
                                                            SoalImporter.CopyReturnAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Sap, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Sap, op);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (outputMsg.Parts.Count > 1)
                                                    {
                                                        this.Importer.Diagnostics.AddError("The output message should have a single part.", this.Uri, this.GetTextSpan(opElem));
                                                    }
                                                    op.ReturnType = SoalInstance.Void;
                                                }
                                            }
                                            else
                                            {
                                                op.IsOneway = true;
                                            }
                                            foreach (var faultMsg in faultMsgs)
                                            {
                                                if (faultMsg.Parts.Count == 1)
                                                {
                                                    WsdlMessagePart part = faultMsg.Parts[0];
                                                    Struct st = null;
                                                    if (part != null)
                                                    {
                                                        st = part.Type as Struct;
                                                    }
                                                    else
                                                    {
                                                        this.Importer.Diagnostics.AddError("The fault message should have a complex type.", this.Uri, this.GetTextSpan(opElem));
                                                    }
                                                    if (st != null)
                                                    {
                                                        op.Exceptions.Add(st);
                                                    }
                                                }
                                                else
                                                {
                                                    if (faultMsg.Parts.Count > 1)
                                                    {
                                                        this.Importer.Diagnostics.AddError("The fault message should have a single part.", this.Uri, this.GetTextSpan(opElem));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (opRpc)
                                    {
                                        ++rpc;
                                        if (inputMsg != null)
                                        {
                                            foreach (var part in inputMsg.Parts)
                                            {
                                                Parameter param = SoalFactory.Instance.CreateParameter();
                                                param.Name = part.Name;
                                                param.Type = part.Type;
                                                this.Importer.Reference(param.Type);
                                                op.Parameters.Add(param);
                                                if (part.OriginalType is Struct)
                                                {
                                                    object origWrapped = ((Struct)part.OriginalType).GetAnnotationPropertyValue(SoalAnnotations.Type, SoalAnnotationProperties.Wrapped) ?? false;
                                                    string origItems = ((Struct)part.OriginalType).GetAnnotationPropertyValue(SoalAnnotations.Type, SoalAnnotationProperties.Items) as string;
                                                    SoalType coreType = part.Type.GetCoreType();
                                                    string coreTypeName = coreType is NamedElement ? ((NamedElement)coreType).Name : null;
                                                    object origSap = ((Struct)part.OriginalType).GetAnnotationPropertyValue(SoalAnnotations.Type, SoalAnnotationProperties.Sap) ?? false;
                                                    if ((bool)origWrapped && ((bool)origSap || (origItems != null && coreTypeName != origItems)))
                                                    {
                                                        SoalImporter.CopyAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Wrapped, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Wrapped, param);
                                                        SoalImporter.CopyAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Items, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Items, param);
                                                        SoalImporter.CopyAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Sap, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Sap, param);
                                                    }
                                                }
                                            }
                                        }
                                        if (outputMsg != null)
                                        {
                                            if (outputMsg.Parts.Count == 1)
                                            {
                                                WsdlMessagePart part = outputMsg.Parts[0];
                                                op.ReturnType = part.Type;
                                                this.Importer.Reference(op.ReturnType);
                                                if (part.OriginalType is Struct)
                                                {
                                                    object origWrapped = ((Struct)part.OriginalType).GetAnnotationPropertyValue(SoalAnnotations.Type, SoalAnnotationProperties.Wrapped) ?? false;
                                                    string origItems = ((Struct)part.OriginalType).GetAnnotationPropertyValue(SoalAnnotations.Type, SoalAnnotationProperties.Items) as string;
                                                    SoalType coreType = part.Type.GetCoreType();
                                                    string coreTypeName = coreType is NamedElement ? ((NamedElement)coreType).Name : null;
                                                    object origSap = ((Struct)part.OriginalType).GetAnnotationPropertyValue(SoalAnnotations.Type, SoalAnnotationProperties.Sap) ?? false;
                                                    if ((bool)origWrapped && ((bool)origSap || (origItems != null && coreTypeName != origItems)))
                                                    {
                                                        SoalImporter.CopyReturnAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Wrapped, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Wrapped, op);
                                                        SoalImporter.CopyReturnAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Items, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Items, op);
                                                        SoalImporter.CopyReturnAnnotationProperty(SoalAnnotations.Type, SoalAnnotationProperties.Sap, ((AnnotatedElement)part.OriginalType), SoalAnnotations.Element, SoalAnnotationProperties.Sap, op);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (outputMsg.Parts.Count > 1)
                                                {
                                                    this.Importer.Diagnostics.AddError("The output message should have a single part.", this.Uri, this.GetTextSpan(opElem));
                                                }
                                                op.ReturnType = SoalInstance.Void;
                                            }
                                        }
                                        else
                                        {
                                            op.IsOneway = true;
                                        }
                                    }
                                    foreach (var faultMsg in faultMsgs)
                                    {
                                        if (faultMsg.Parts.Count == 1)
                                        {
                                            WsdlMessagePart part = faultMsg.Parts[0];
                                            Struct st = null;
                                            if (part != null)
                                            {
                                                st = part.Type as Struct;
                                            }
                                            if (st != null)
                                            {
                                                op.Exceptions.Add(st);
                                            }
                                        }
                                        else
                                        {
                                            if (faultMsg.Parts.Count > 1)
                                            {
                                                this.Importer.Diagnostics.AddError("The fault message should have a single part.", this.Uri, this.GetTextSpan(opElem));
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.Importer.Diagnostics.AddError("The operation has no 'name' attribute.", this.Uri, this.GetTextSpan(opElem));
                                    continue;
                                }
                            }
                            if (document > 0 && rpc > 0)
                            {
                                this.Importer.Diagnostics.AddError("The portType has both document and RPC style operations. Use either of the styles on all operations but not both.", this.Uri, this.GetTextSpan(elem));
                                continue;
                            }
                            else if (document > 0)
                            {
                                if (wrapped < count)
                                {
                                    if (wrapped > 0)
                                    {
                                        this.Importer.Diagnostics.AddWarning("The portType has both wrapped and unwrapped document style operations. Use either of the styles on all operations but not both.", this.Uri, this.GetTextSpan(elem));
                                        wrapped = 0;
                                    }
                                    Annotation annot = SoalFactory.Instance.CreateAnnotation();
                                    annot.Name = SoalAnnotations.NoWrap;
                                    intf.Annotations.Add(annot);
                                }
                            }
                            else
                            {
                                Annotation annot = SoalFactory.Instance.CreateAnnotation();
                                annot.Name = SoalAnnotations.Rpc;
                                intf.Annotations.Add(annot);
                            }
                        }
                        else
                        {
                            this.Importer.Diagnostics.AddError("The portType has no 'name' attribute.", this.Uri, this.GetTextSpan(elem));
                            continue;
                        }
                    }
                }
            }
        }

        private void ImportPhase7()
        {
            foreach (var elem in this.Root.Elements())
            {
                if (elem.Name.NamespaceName == WsdlReader.WsdlNamespace)
                {
                    if (elem.Name.LocalName == "binding")
                    {
                        XAttribute nameAttr = elem.Attribute("name");
                        XAttribute typeAttr = elem.Attribute("type");
                        if (nameAttr == null)
                        {
                            this.Importer.Diagnostics.AddError("The binding has no 'name' attribute.", this.Uri, this.GetTextSpan(elem));
                            continue;
                        }
                        if (typeAttr == null)
                        {
                            this.Importer.Diagnostics.AddError("The binding has no 'type' attribute.", this.Uri, this.GetTextSpan(elem));
                            continue;
                        }
                        string name = nameAttr.Value;
                        Interface intf = this.Importer.WsdlPortTypes.Get(this.GetXName(elem, typeAttr.Value)) as Interface;
                        if (intf == null)
                        {
                            this.Importer.Diagnostics.AddError("Could not resolve portType.", this.Uri, this.GetTextSpan(typeAttr));
                            continue;
                        }
                        Binding binding = SoalFactory.Instance.CreateBinding();
                        binding.Name = name;
                        binding.Namespace = this.Namespace;
                        if (binding.Name.StartsWith(intf.Name+"_") && binding.Name.Length > intf.Name.Length+1)
                        {
                            binding.Name = name.Substring(intf.Name.Length + 1);
                        }
                        if (name.ToLower().EndsWith("_binding") && name.Length > 8)
                        {
                            binding.Name = binding.Name.Substring(0, binding.Name.Length - 8);
                        }
                        else if (name.ToLower().EndsWith("binding") && name.Length > 7)
                        {
                            binding.Name = binding.Name.Substring(0, binding.Name.Length - 7);
                        }
                        if (binding.Name.EndsWith("_" + intf.Name) && binding.Name.Length > intf.Name.Length + 1)
                        {
                            binding.Name = binding.Name.Substring(0, binding.Name.Length - (intf.Name.Length + 1));
                        }
                        this.Importer.WsdlBindings.Register(this, this.tns + name, elem, binding);
                        XElement soap11BindingElem = elem.Element(this.soap11 + "binding");
                        XElement soap12BindingElem = elem.Element(this.soap12 + "binding");
                        bool rpc = false;
                        bool document = false;
                        bool http = false;
                        if (soap11BindingElem != null)
                        {
                            XAttribute styleAttr = soap11BindingElem.Attribute("style");
                            if (styleAttr != null)
                            {
                                if (styleAttr.Value == "document") document = true;
                                if (styleAttr.Value == "rpc") rpc = true;
                            }
                            else
                            {
                                this.Importer.Diagnostics.AddWarning("The soap binding has no 'style' attribute.", this.Uri, this.GetTextSpan(soap11BindingElem));
                                document = true;
                                //continue;
                            }
                            XAttribute transportAttr = soap11BindingElem.Attribute("transport");
                            if (transportAttr != null)
                            {
                                if (transportAttr.Value == WsdlReader.HttpTransportUri)
                                {
                                    http = true;
                                }
                                else
                                {
                                    this.Importer.Diagnostics.AddWarning("Unknown transport: '"+transportAttr.Value+"'.", this.Uri, this.GetTextSpan(transportAttr));
                                    http = true;
                                }
                            }
                            else
                            {
                                this.Importer.Diagnostics.AddWarning("The soap binding has no 'transport' attribute.", this.Uri, this.GetTextSpan(soap11BindingElem));
                                http = true;
                                //continue;
                            }
                        }
                        else if (soap12BindingElem != null)
                        {
                            XAttribute styleAttr = soap12BindingElem.Attribute("style");
                            if (styleAttr != null)
                            {
                                if (styleAttr.Value == "document") document = true;
                                if (styleAttr.Value == "rpc") rpc = true;
                            }
                            else
                            {
                                this.Importer.Diagnostics.AddWarning("The soap binding has no 'style' attribute.", this.Uri, this.GetTextSpan(soap12BindingElem));
                                document = true;
                                //continue;
                            }
                            XAttribute transportAttr = soap12BindingElem.Attribute("transport");
                            if (transportAttr != null)
                            {
                                if (transportAttr.Value == WsdlReader.HttpTransportUri)
                                {
                                    http = true;
                                }
                                else
                                {
                                    this.Importer.Diagnostics.AddWarning("Unknown transport: '" + transportAttr.Value + "'.", this.Uri, this.GetTextSpan(transportAttr));
                                    http = true;
                                }
                            }
                            else
                            {
                                this.Importer.Diagnostics.AddWarning("The soap binding has no 'transport' attribute.", this.Uri, this.GetTextSpan(soap12BindingElem));
                                http = true;
                                //continue;
                            }
                        }
                        bool errors = false;
                        bool soap11 = soap11BindingElem != null;
                        bool soap12 = soap12BindingElem != null;
                        bool opLiteral = false;
                        bool opEncoded = false;
                        List<Operation> unboundOps = new List<Operation>(intf.Operations);
                        foreach (var opElem in elem.Elements(wsdl+"operation"))
                        {
                            XAttribute opNameAttr = opElem.Attribute("name");
                            if (opNameAttr == null)
                            {
                                this.Importer.Diagnostics.AddError("The operation has no 'name' attribute.", this.Uri, this.GetTextSpan(opElem));
                                errors = true;
                                continue;
                            }
                            string opName = opNameAttr.Value;
                            string action = null;
                            XElement soap11OperationElem = elem.Element(this.soap11 + "operation");
                            XElement soap12OperationElem = elem.Element(this.soap12 + "operation");
                            if (soap11OperationElem != null)
                            {
                                soap11 = true;
                                XAttribute styleAttr = soap11BindingElem.Attribute("style");
                                if (styleAttr != null)
                                {
                                    if (styleAttr.Value == "document") document = true;
                                    if (styleAttr.Value == "rpc") rpc = true;
                                }
                                XAttribute actionAttr = soap11BindingElem.Attribute("soapAction");
                                if (actionAttr != null)
                                {
                                    action = actionAttr.Value;
                                }
                            }
                            if (soap12OperationElem != null)
                            {
                                soap12 = true;
                                XAttribute styleAttr = soap12BindingElem.Attribute("style");
                                if (styleAttr != null)
                                {
                                    if (styleAttr.Value == "document") document = true;
                                    if (styleAttr.Value == "rpc") rpc = true;
                                }
                                XAttribute actionAttr = soap12BindingElem.Attribute("soapAction");
                                if (actionAttr != null)
                                {
                                    action = actionAttr.Value;
                                }
                            }
                            bool found = false;
                            foreach (var op in intf.Operations)
                            {
                                if (op.Name == opName)
                                {
                                    found = true;
                                    unboundOps.Remove(op);
                                    op.Action = action;
                                    XElement inputElem = opElem.Element(wsdl + "input");
                                    XElement outputElem = opElem.Element(wsdl + "output");
                                    if (inputElem != null)
                                    {
                                        XElement soap11Elem = inputElem.Element(this.soap11 + "body");
                                        XElement soap12Elem = inputElem.Element(this.soap12 + "body");
                                        if (soap11Elem != null)
                                        {
                                            soap11 = true;
                                            XAttribute useAttr = soap11Elem.Attribute("use");
                                            if (useAttr != null)
                                            {
                                                if (useAttr.Value == "literal") opLiteral = true;
                                                if (useAttr.Value == "encoded") opEncoded = true;
                                            }
                                        }
                                        if (soap12Elem != null)
                                        {
                                            soap12 = true;
                                            XAttribute useAttr = soap12Elem.Attribute("use");
                                            if (useAttr != null)
                                            {
                                                if (useAttr.Value == "literal") opLiteral = true;
                                                if (useAttr.Value == "encoded") opEncoded = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.Importer.Diagnostics.AddError("The operation has no input defined in the binding.", this.Uri, this.GetTextSpan(opElem));
                                        errors = true;
                                    }
                                    if (!op.IsOneway)
                                    {
                                        if (outputElem != null)
                                        {
                                            XElement soap11Elem = inputElem.Element(this.soap11 + "body");
                                            XElement soap12Elem = inputElem.Element(this.soap12 + "body");
                                            if (soap11Elem != null)
                                            {
                                                soap11 = true;
                                                XAttribute useAttr = soap11Elem.Attribute("use");
                                                if (useAttr != null)
                                                {
                                                    if (useAttr.Value == "literal") opLiteral = true;
                                                    if (useAttr.Value == "encoded") opEncoded = true;
                                                }
                                            }
                                            if (soap12Elem != null)
                                            {
                                                soap12 = true;
                                                XAttribute useAttr = soap12Elem.Attribute("use");
                                                if (useAttr != null)
                                                {
                                                    if (useAttr.Value == "literal") opLiteral = true;
                                                    if (useAttr.Value == "encoded") opEncoded = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            this.Importer.Diagnostics.AddError("The operation has no output defined in the binding.", this.Uri, this.GetTextSpan(opElem));
                                            errors = true;
                                        }
                                    }
                                    else if (outputElem != null)
                                    {
                                        this.Importer.Diagnostics.AddError("A oneway operation cannot have an output.", this.Uri, this.GetTextSpan(outputElem));
                                        errors = true;
                                    }
                                    // TODO: faults
                                    if (errors)
                                    {
                                        break;
                                    }
                                    break;
                                }
                            }
                            if (!found)
                            {
                                this.Importer.Diagnostics.AddError("The portType has no operation named '"+opName+"' attribute.", this.Uri, this.GetTextSpan(opNameAttr));
                                errors = true;
                            }
                        }
                        foreach (var op in unboundOps)
                        {
                            this.Importer.Diagnostics.AddError("The operation named '" + op.Name + "' from the portType '" + intf.Name + "' is not defined in the binding.", this.Uri, this.GetTextSpan(elem));
                            errors = true;
                        }
                        if (errors)
                        {
                            continue;
                        }
                        if (document && rpc)
                        {
                            this.Importer.Diagnostics.AddError("The binding has mixed document and RPC styles.", this.Uri, this.GetTextSpan(elem));
                            continue;
                        }
                        if (opLiteral && opEncoded)
                        {
                            this.Importer.Diagnostics.AddError("The binding has mixed literal and encoded styles.", this.Uri, this.GetTextSpan(elem));
                            continue;
                        }
                        if (soap11 && soap12)
                        {
                            this.Importer.Diagnostics.AddError("The binding has mixed SOAP 1.1 and SOAP 1.2 protocols.", this.Uri, this.GetTextSpan(elem));
                            continue;
                        }
                        if (document && intf.HasAnnotation(SoalAnnotations.Rpc))
                        {
                            this.Importer.Diagnostics.AddError("The binding is document style but the portType is RPC style.", this.Uri, this.GetTextSpan(elem));
                            continue;
                        }
                        if (rpc && !intf.HasAnnotation(SoalAnnotations.Rpc))
                        {
                            this.Importer.Diagnostics.AddError("The binding is RPC style but the portType is document style.", this.Uri, this.GetTextSpan(elem));
                            continue;
                        }
                        if (http)
                        {
                            binding.Transport = SoalFactory.Instance.CreateHttpTransportBindingElement();
                        }
                        if (soap11 || soap12)
                        {
                            SoapEncodingBindingElement sebe = SoalFactory.Instance.CreateSoapEncodingBindingElement();
                            binding.Encodings.Add(sebe);
                            sebe.Version = soap12 ? SoapVersion.Soap12 : SoapVersion.Soap11;
                            if (rpc && opEncoded) sebe.Style = SoapEncodingStyle.RpcEncoded;
                            else if (rpc && opLiteral) sebe.Style = SoapEncodingStyle.RpcLiteral;
                            else if (document)
                            {
                                if (intf.HasAnnotation(SoalAnnotations.NoWrap)) sebe.Style = SoapEncodingStyle.DocumentLiteral;
                                else sebe.Style = SoapEncodingStyle.DocumentWrapped;
                            }
                        }
                        this.ImportAllPolicies(elem, binding);
                    }
                }
            }
        }

        private void ImportPhase8()
        {
            foreach (var elem in this.Root.Elements())
            {
                if (elem.Name.NamespaceName == WsdlReader.WsdlNamespace)
                {
                    if (elem.Name.LocalName == "service")
                    {
                        XAttribute serviceNameAttr = elem.Attribute("name");
                        string serviceName = null;
                        if (serviceNameAttr != null)
                        {
                            serviceName = serviceNameAttr.Value;
                        }
                        List<XElement> policyElems = elem.Elements(this.wsp + "Policy").ToList();
                        List<XElement> policyReferenceElems = elem.Elements(this.wsp + "PolicyReference").ToList();
                        Binding serviceBinding = null;
                        if (policyElems.Count > 0 || policyReferenceElems.Count > 0)
                        {
                            serviceBinding = SoalFactory.Instance.CreateBinding();
                            this.ImportAllPolicies(elem, serviceBinding);
                        }
                        List<XElement> portElems = elem.Elements(wsdl + "port").ToList();
                        bool singlePort = portElems.Count == 1;
                        foreach (var portElem in portElems)
                        {
                            XAttribute portNameAttr = portElem.Attribute("name");
                            string portName = null;
                            if (portNameAttr != null)
                            {
                                portName = portNameAttr.Value;
                            }
                            if (singlePort && serviceName != null)
                            {
                                portName = serviceName;
                            }
                            XAttribute bindingAttr = portElem.Attribute("binding");
                            if (bindingAttr != null)
                            {
                                Binding binding = this.Importer.WsdlBindings.Get(this.GetXName(portElem, bindingAttr.Value));
                                XElement bindingElem = this.Importer.WsdlBindings.GetX(this.GetXName(portElem, bindingAttr.Value));
                                if (binding == null || bindingElem == null)
                                {
                                    this.Importer.Diagnostics.AddError("The referenced binding could not be resolved.", this.Uri, this.GetTextSpan(bindingAttr));
                                    continue;
                                }
                                List<XElement> portPolicyElems = portElem.Elements(this.wsp + "Policy").ToList();
                                List<XElement> portPolicyReferenceElems = portElem.Elements(this.wsp + "PolicyReference").ToList();
                                Binding portBinding = null;
                                if (portPolicyElems.Count > 0 || portPolicyReferenceElems.Count > 0)
                                {
                                    portBinding = SoalFactory.Instance.CreateBinding();
                                    this.ImportAllPolicies(portElem, portBinding);
                                }
                                if (portBinding != null || serviceBinding != null)
                                {
                                    Binding finalBinding = this.CloneBinding(binding);
                                    this.ApplyPolicy(finalBinding, portBinding);
                                    this.ApplyPolicy(finalBinding, serviceBinding);
                                    finalBinding.Name = portName + "_Binding";
                                    finalBinding.Namespace = this.Namespace;
                                    binding = finalBinding;
                                }
                                string address = null;
                                XElement soap11AddressElem = portElem.Element(soap11 + "address");
                                XElement soap12AddressElem = portElem.Element(soap12 + "address");
                                XElement soapAddressElem = null;
                                if (soap11AddressElem != null) soapAddressElem = soap11AddressElem;
                                else if (soap12AddressElem != null) soapAddressElem = soap12AddressElem;
                                if (soapAddressElem != null)
                                {
                                    XAttribute locationAttr = soapAddressElem.Attribute("location");
                                    if (locationAttr != null)
                                    {
                                        address = locationAttr.Value;
                                    }
                                }
                                Interface intf = null;
                                XAttribute bindingTypeAttr = bindingElem.Attribute("type");
                                if (bindingTypeAttr != null)
                                {
                                    intf = this.Importer.WsdlPortTypes.Get(this.GetXName(bindingElem, bindingTypeAttr.Value));
                                }
                                if (intf != null)
                                {
                                    Endpoint endp = SoalFactory.Instance.CreateEndpoint();
                                    endp.Name = portName;
                                    endp.Interface = intf;
                                    endp.Binding = binding;
                                    endp.Address = address;
                                    endp.Namespace = this.Namespace;
                                }
                                else
                                {
                                    this.Importer.Diagnostics.AddError("The interface referenced by the binding could not be resolved.", this.Uri, this.GetTextSpan(bindingTypeAttr));
                                    continue;
                                }
                            }
                            else
                            {
                                this.Importer.Diagnostics.AddError("The port has no 'binding' attribute.", this.Uri, this.GetTextSpan(portElem));
                                continue;
                            }
                        }
                    }
                }
            }
        }

        private void ImportAllPolicies(XElement elem, Binding binding)
        {
            List<XElement> policyElems = elem.Elements(this.wsp + "Policy").ToList();
            List<XElement> policyReferenceElems = elem.Elements(this.wsp + "PolicyReference").ToList();
            foreach (var policyElem in policyElems)
            {
                this.ImportPolicy(policyElem, binding);
            }
            foreach (var policyReferenceElem in policyReferenceElems)
            {
                this.ImportPolicyReference(policyReferenceElem, binding);
            }
        }

        private void ImportPolicy(XElement elem, Binding policy)
        {
            XAttribute id = elem.Attribute(wsu + "Id");
            if (id != null)
            {
                string name = id.Value;
                policy.Name = name;
                XNamespace uri = this.Uri;
                this.Importer.WsdlPolicies.Register(this, uri + name, elem, policy);
            }
            List<XElement> items = new List<XElement>();
            if (!this.GetChildPolicies(elem, items)) return;
            foreach (var item in items)
            {
                if (item.Name == wsaw + "UsingAddressing")
                {
                    this.ImportAddressingPolicy(item, wsaw, policy);
                }
                else if (item.Name == wsam + "Addressing")
                {
                    this.ImportAddressingPolicy(item, wsam, policy);
                }
                else if (item.Name == wsoma + "OptimizedMimeSerialization")
                {
                    this.ImportMtomPolicy(item, wsoma, policy);
                }
                else if (item.Name == sp1 + "TransportBinding")
                {
                    this.ImportTransportBindingPolicy(item, sp1, policy);
                }
                else if (item.Name == sp2 + "TransportBinding")
                {
                    this.ImportTransportBindingPolicy(item, sp2, policy);
                }
                else
                {
                    this.Importer.Diagnostics.AddWarning("The policy is not supported by the SOAL importer.", this.Uri, this.GetTextSpan(item));
                }
            }
        }

        private void ImportPolicyReference(XElement elem, Binding binding)
        {
            XAttribute uriAttr = elem.Attribute("URI");
            if (uriAttr != null)
            {
                string refName = uriAttr.Value;
                string[] refParts = refName.Split('#');
                if (refParts.Length == 2)
                {
                    string refDoc = refParts[0];
                    string refId = refParts[1];
                    XNamespace uri = null;
                    if (string.IsNullOrWhiteSpace(refDoc))
                    {
                        uri = this.Uri;
                    }
                    else
                    {
                        uri = refDoc;
                    }
                    Binding policy = this.Importer.WsdlPolicies.Get(uri + refId);
                    if (policy == null)
                    {
                        this.Importer.Diagnostics.AddError("Could not resolve policy reference: '" + refName + "'.", this.Uri, this.GetTextSpan(elem));
                        return;
                    }
                    this.ApplyPolicy(binding, policy);
                }
                else
                {
                    this.Importer.Diagnostics.AddError("Invalid policy reference: '" + refName + "'.", this.Uri, this.GetTextSpan(elem));
                    return;
                }
            }
            else
            {
                this.Importer.Diagnostics.AddError("The PolicyReference has no 'URI' attribute.", this.Uri, this.GetTextSpan(elem));
                return;
            }
        }

        private bool GetChildPolicies(XElement elem, List<XElement> children)
        {
            bool result = true;
            foreach (var child in elem.Elements())
            {
                if (child.Name.NamespaceName == WsdlReader.WspNamespace)
                {
                    if (child.Name.LocalName == "Policy")
                    {
                        result &= this.GetChildPolicies(child, children);
                    }
                    if (child.Name.LocalName == "All")
                    {
                        result &= this.GetChildPolicies(child, children);
                    }
                    if (child.Name.LocalName == "ExactlyOne")
                    {
                        if (child.Elements().Count() > 1)
                        {
                            this.Importer.Diagnostics.AddError("Policy alternatives are not supported by the SOAL importer.", this.Uri, this.GetTextSpan(child));
                            return false;
                        }
                        else
                        {
                            result &= this.GetChildPolicies(child, children);
                        }
                    }
                }
                else
                {
                    children.Add(child);
                }
            }
            return result;
        }

        private void ImportAddressingPolicy(XElement elem, XNamespace wsa, Binding policy)
        {
            WsAddressingBindingElement wabe = SoalFactory.Instance.CreateWsAddressingBindingElement();
            policy.Protocols.Add(wabe);
        }

        private void ImportMtomPolicy(XElement elem, XNamespace wsoma, Binding policy)
        {
            SoapEncodingBindingElement sebe = null; 
            foreach (var enc in policy.Encodings)
            {
                if (enc is SoapEncodingBindingElement)
                {
                    sebe = (SoapEncodingBindingElement)enc;
                    break;
                }
            }
            if (sebe == null)
            {
                sebe = SoalFactory.Instance.CreateSoapEncodingBindingElement();
            }
            sebe.Mtom = true;
            policy.Encodings.Add(sebe);
        }

        private void ImportTransportBindingPolicy(XElement elem, XNamespace sp, Binding policy)
        {
            bool https = false;
            bool clientCert = false;
            List<XElement> items = new List<XElement>();
            if (!this.GetChildPolicies(elem, items)) return;
            foreach (var item in items)
            {
                if (item.Name == sp + "TransportToken")
                {
                    List<XElement> tokenItems = new List<XElement>();
                    this.GetChildPolicies(item, tokenItems);
                    foreach (var tokenItem in tokenItems)
                    {
                        if (tokenItem.Name == sp + "HttpsToken")
                        {
                            https = true;
                            XAttribute clientCertAttr = tokenItem.Attribute("RequireClientCertificate");
                            if (clientCertAttr != null)
                            {
                                clientCert = clientCertAttr.Value == "true" || clientCertAttr.Value == "1";
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            if (https)
            {
                HttpTransportBindingElement htbe = SoalFactory.Instance.CreateHttpTransportBindingElement();
                policy.Transport = htbe;
                htbe.Ssl = true;
                htbe.ClientAuthentication = clientCert;
            }
        }

        private void ApplyPolicy(Binding binding, Binding policy)
        {
            if (binding == null) return;
            if (policy == null) return;
            HttpTransportBindingElement bhtbe = binding.Transport as HttpTransportBindingElement;
            HttpTransportBindingElement phtbe = policy.Transport as HttpTransportBindingElement;
            if (bhtbe != null && phtbe != null)
            {
                if (phtbe.Ssl)
                {
                    bhtbe.Ssl = phtbe.Ssl;
                    bhtbe.ClientAuthentication = phtbe.ClientAuthentication;
                }
            }
            binding.Protocols.Clear();
            foreach (var benc in binding.Encodings)
            {
                if (benc is SoapEncodingBindingElement)
                {
                    SoapEncodingBindingElement bsebe = (SoapEncodingBindingElement)benc;
                    foreach (var penc in policy.Encodings)
                    {
                        if (penc is SoapEncodingBindingElement)
                        {
                            SoapEncodingBindingElement psebe = (SoapEncodingBindingElement)penc;
                            if (psebe.Mtom)
                            {
                                bsebe.Mtom = true;
                            }
                        }
                    }
                }
            }
            foreach (var protocol in policy.Protocols)
            {
                if (protocol is WsAddressingBindingElement)
                {
                    WsAddressingBindingElement wabe = SoalFactory.Instance.CreateWsAddressingBindingElement();
                    binding.Protocols.Add(wabe);
                }
            }
        }

        private Binding CloneBinding(Binding binding)
        {
            if (binding == null) return null;
            Binding result = SoalFactory.Instance.CreateBinding();
            if (binding.Transport is HttpTransportBindingElement)
            {
                result.Transport = SoalFactory.Instance.CreateHttpTransportBindingElement();
            }
            foreach (var enc in binding.Encodings)
            {
                if (enc is SoapEncodingBindingElement)
                {
                    SoapEncodingBindingElement benc = ((SoapEncodingBindingElement)enc);
                    SoapEncodingBindingElement renc = SoalFactory.Instance.CreateSoapEncodingBindingElement();
                    result.Encodings.Add(renc);
                    renc.Version = benc.Version;
                    renc.Style = benc.Style;
                }
            }
            this.ApplyPolicy(result, binding);
            return result;
        }

    }

}


/*
List<XElement> faultElems = opElem.Elements(wsdl + "fault").ToList();
List<Exception> unboundExcs = new List<Exception>(op.Exceptions);
foreach (var faultElem in faultElems)
{
    XAttribute faultNameAttr = faultElem.Attribute("name");
    if (faultNameAttr == null)
    {
        this.Importer.Diagnostics.AddError("The fault has no 'name' attribute.", this.Uri, this.GetTextSpan(faultNameAttr));
        errors = true;
        continue;
    }
    string faultName = faultNameAttr.Value;
    bool faultFound = false;
    foreach (var ex in op.Exceptions)
    {
        if (ex.Name == faultName)
        {
            faultFound = true;
            unboundExcs.Remove(ex);
            break;
        }
    }
    if (!faultFound)
    {
        this.Importer.Diagnostics.AddError("The operation has no fault defined in the binding for '"+faultName+"'.", this.Uri, this.GetTextSpan(opElem));
        errors = true;
    }
}
foreach (var ex in unboundExcs)
{
    this.Importer.Diagnostics.AddError("The fault '"+ex.Name+"' of the operation '" + op.Name + "' from the portType '" + intf.Name + "' is not defined in the binding.", this.Uri, this.GetTextSpan(opElem));
    errors = true;
}
*/
