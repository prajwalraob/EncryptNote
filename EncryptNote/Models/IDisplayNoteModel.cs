using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml;

namespace EncryptNote.Models
{
    interface IDisplayNoteModel
    {
        string UniqueID { get; set; }
        object NoteDocument { get; set; }
    }
}
