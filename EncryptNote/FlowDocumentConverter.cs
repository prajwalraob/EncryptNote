using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace EncryptNote
{
    public class FlowDocumentConverter
    {
        public XmlDocument Convert(FlowDocument flowDocument)
        {
            if(flowDocument != null)
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(XamlWriter.Save(flowDocument));
                return xmlDocument;
            }
            return null;

        }

        public FlowDocument ConvertBack(XmlDocument xmlDocument)
        {
            if(xmlDocument != null)
            {
                XmlReader xmlReader = new XmlNodeReader(xmlDocument);
                XamlReader xamlReader = new XamlReader();
                FlowDocument flowDocument = xamlReader.LoadAsync(xmlReader) as FlowDocument;
                return flowDocument;
            }
            return null;
        }
    }
}
