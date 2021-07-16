using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EncryptNote.Models;

namespace EncryptNote
{
    interface ISerializeNote
    {
        void SerializeNote(IDisplayNoteModel displayNoteModel);
        void SerializeNoteinfo(INoteItemModel noteItemModel);        
    }
}
