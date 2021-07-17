using System.Xml;

namespace EncryptNote
{
    public class SerializationNote
    {
        public XmlDocument NoteDocument { get; set; }
        public string UniqueID { get; set; }
    }
}
