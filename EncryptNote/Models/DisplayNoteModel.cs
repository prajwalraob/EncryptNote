using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using EncryptNote.ViewModels;
using System.Xml;

namespace EncryptNote.Models
{
    public class DisplayNoteModel : NotifyPropertyChanged, IDisplayNoteModel
    {
        private string _uniqueID;
        public string UniqueID { 
            get => _uniqueID;
            set
            {
                _uniqueID = value;
                OnPropertyChanged();
            }
        }
        
        private XmlDocument _noteDocument;
        public XmlDocument NoteDocument
        { 
            get => _noteDocument;
            set 
            {
                _noteDocument = value;
                OnPropertyChanged();
            }
        }

        //public DisplayNoteModel(string uniqueID, string noteAsJson)
        //{
        //    this.UniqueID = uniqueID;
        //    this.NoteAsJson = noteAsJson;
        //}
    }
}
