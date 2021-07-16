using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using EncryptNote.Models;
using System.Collections.ObjectModel;

namespace EncryptNote
{
    static class GlobalVariables
    {
        internal static IContainer Container { get; set; }

        internal static string NotesDirectory { get; set; } = Path.Combine(Environment.CurrentDirectory, "notes");

        internal static IList<INoteItemModel> NotesList { get; set; } = new List<INoteItemModel>();


        internal static ObservableCollection<INoteItemModel> GetNotesFromFolder()
        {
            return new ObservableCollection<INoteItemModel>(NotesList);
        }

    }
}
