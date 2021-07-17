using EncryptNote.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace EncryptNote
{
    class SerializeNote : ISerializeNote
    {
        public void SerializeNoteinfo(INoteItemModel noteItemModel)
        {
            XmlSerializer noteItemSerialize = new XmlSerializer(noteItemModel.GetType());
            using (FileStream SourceStream = File.Open(Path.Combine(GlobalVariables.NotesDirectory, $"{noteItemModel.UniqueID}.noteinfo"), FileMode.OpenOrCreate))
            {
                noteItemSerialize.Serialize(SourceStream, noteItemModel);
            }
        }

        public void SerializeNoteDocument(IDisplayNoteModel displayNoteModel)
        {
            SerializationNote serializeNote = new SerializationNote()
            {
                UniqueID = displayNoteModel.UniqueID,
                NoteDocument = displayNoteModel.NoteDocument as XmlDocument
            };

            XmlSerializer displayNoteSerialize = new XmlSerializer(serializeNote.GetType());
            using (FileStream SourceStream = File.Open(Path.Combine(GlobalVariables.NotesDirectory, $"{serializeNote.UniqueID}.note"), FileMode.OpenOrCreate))
            {
                displayNoteSerialize.Serialize(SourceStream, serializeNote);
            }

        }

    }
}
