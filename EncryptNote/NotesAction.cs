using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


using EncryptNote.Models;
using Autofac;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Documents;
using System.Windows.Markup;

namespace EncryptNote
{
    class NotesAction : INotesAction
    {
        public INoteItemModel CreateNote()
        {
            using (var scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                INoteItemModel noteItem = scope.Resolve<INoteItemModel>();
                noteItem.Created = DateTime.Now;
                noteItem.DisplayName = "New Note";
                noteItem.LastUpdated = DateTime.Now;
                noteItem.UniqueID = GetUniqueId(16);

                IDisplayNoteModel displayNote = scope.Resolve<IDisplayNoteModel>();
                displayNote.UniqueID = noteItem.UniqueID;
                displayNote.NoteDocument = new XmlDocument();

                XmlSerializer displayNoteSerialize = new XmlSerializer(displayNote.GetType());
                using (FileStream SourceStream = File.Open(Path.Combine(GlobalVariables.NotesDirectory, $"{displayNote.UniqueID}.note"), FileMode.OpenOrCreate))
                {
                    displayNoteSerialize.Serialize(SourceStream, displayNote);
                }

                XmlSerializer noteItemSerialize = new XmlSerializer(noteItem.GetType());
                using (FileStream SourceStream = File.Open(Path.Combine(GlobalVariables.NotesDirectory, $"{noteItem.UniqueID}.noteinfo"), FileMode.OpenOrCreate))
                {
                    noteItemSerialize.Serialize(SourceStream, noteItem);
                }

                return noteItem;
            }
        }

        public ICollection<INoteItemModel> DeleteNotes(IList<INoteItemModel> deleteItems, ICollection<INoteItemModel> notes)
        {
            using (var scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                IDictionary<string, INoteItemModel> dict = new Dictionary<string, INoteItemModel>();

                foreach (var note in notes)
                {
                    dict.Add(note.UniqueID, note);
                }

                foreach(var item in deleteItems)
                {
                    if(dict.ContainsKey(item.UniqueID))
                    {
                        dict.Remove(item.UniqueID);
                    }

                    File.Delete(Path.Combine(GlobalVariables.NotesDirectory, $"{item.UniqueID}.noteinfo"));
                    File.Delete(Path.Combine(GlobalVariables.NotesDirectory, $"{item.UniqueID}.note"));
                }

                return new ObservableCollection<INoteItemModel> (dict.Select(q => q.Value));
            }

        }

        public object ReadNote(INoteItemModel note)
        {

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Path.Combine(GlobalVariables.NotesDirectory, $"{ note.UniqueID }.note"));
            XmlNode documentNode = xmlDocument.DocumentElement.SelectSingleNode("/DisplayNoteModel/NoteDocument");
            
            if(documentNode != null)
            {
                XmlDocument newXmlDoc = new XmlDocument();
                newXmlDoc.LoadXml(documentNode.InnerXml);
                return newXmlDoc;
            }

            return null;
        }

        public INoteItemModel UpdateNote(INoteItemModel note, object noteDocument)
        {
            using (var scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                IDisplayNoteModel displayNote = scope.Resolve<IDisplayNoteModel>();
                displayNote.UniqueID = note.UniqueID;
                displayNote.NoteDocument = noteDocument as XmlDocument;

                File.Delete(Path.Combine(GlobalVariables.NotesDirectory, $"{displayNote.UniqueID}.note"));
                
                XmlSerializer displayNoteSerialize = new XmlSerializer(displayNote.GetType());
                using (FileStream SourceStream = File.Open(Path.Combine(GlobalVariables.NotesDirectory, $"{displayNote.UniqueID}.note"), FileMode.OpenOrCreate))
                {
                    displayNoteSerialize.Serialize(SourceStream, displayNote);
                }
                return null;
            }
        }

        private string GetUniqueId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random((int)DateTime.Now.Ticks);
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
