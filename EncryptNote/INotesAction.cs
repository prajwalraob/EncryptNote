using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EncryptNote.Models;


namespace EncryptNote
{
    interface INotesAction
    {
        INoteItemModel CreateNote();
        object ReadNote(INoteItemModel note);
        INoteItemModel UpdateNote(INoteItemModel note, object noteDocument);
        ICollection<INoteItemModel> DeleteNotes(IList<INoteItemModel> deleteItems, ICollection<INoteItemModel> notes);
    }
}

