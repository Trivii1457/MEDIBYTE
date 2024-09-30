using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Blazor.FE.Entities
{
    public partial class Invoice : InvoiceType
    {
        public InvoiceControl InvoiceControl { get; set; }

        public string SerializeToXml()
        {
            UTF8Encoding encoding = new UTF8Encoding(false);

            var xmlAttributes = new XmlAttributes();
            xmlAttributes.Xmlns = false;
            xmlAttributes.XmlType = new XmlTypeAttribute() { Namespace = "" };

            var xmlAttributeOverrides = new XmlAttributeOverrides();

            var types = this.GetType().Assembly.GetTypes().Where(t => string.Equals(t.Namespace, this.GetType().Namespace, StringComparison.Ordinal));
            foreach (var t in types) xmlAttributeOverrides.Add(t, xmlAttributes);

            XmlSerializer sr = new XmlSerializer(this.GetType(), xmlAttributeOverrides);

            MemoryStream memoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(memoryStream, encoding);

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            // get the stream from the writer
            memoryStream = writer.BaseStream as MemoryStream;

            sr.Serialize(writer, this, namespaces);

            // apply encoding to the stream 
            return (encoding.GetString(memoryStream.ToArray()).Trim()).Replace(@" xmlns=""urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2""","").Replace(@"xmlns=""urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2""","");
        }

        //public string ToXml()
        //{
        //    //this avoids xml document declaration
        //    XmlWriterSettings settings = new XmlWriterSettings()
        //    {
        //        Indent = false,
        //        OmitXmlDeclaration = true
        //    };

        //    var stream = new MemoryStream();
        //    using (XmlWriter xw = XmlWriter.Create(stream, settings))
        //    {
        //        //this avoids xml namespace declaration
        //        //XmlSerializerNamespaces ns = new XmlSerializerNamespaces(
        //        //                   new[] { XmlQualifiedName.Empty });

        //        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        //        ns.Add(string.Empty, string.Empty);

        //        XmlSerializer x = new XmlSerializer(GetType(), "");
        //        x.Serialize(xw, this, ns);
        //    }

        //    return Encoding.UTF8.GetString(stream.ToArray()).Replace(@" xmlns=""urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2""", "").Replace(@"xmlns=""urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2""", "");

        //}

    }

    public partial class AuthrorizedInvoices
    {
        public string TechnicalKey { get; set; }

    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }

    
}
