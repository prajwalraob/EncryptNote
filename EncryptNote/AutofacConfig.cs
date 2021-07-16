using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;


namespace EncryptNote
{
    internal static class AutofacConfig
    {
        internal static bool Config()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Models.NoteItemModel>().As<Models.INoteItemModel>();
            builder.RegisterType<Models.DisplayNoteModel>().As<Models.IDisplayNoteModel>();
            builder.RegisterType<NotesAction>().As<INotesAction>();

            GlobalVariables.Container = builder.Build();
            return true;
        }
    }
}
