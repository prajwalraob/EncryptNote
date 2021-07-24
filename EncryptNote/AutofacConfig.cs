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
            builder.RegisterType<SerializeNote>().As<ISerializeNote>();
            //builder.RegisterType<GenerateMD5Hash>().As<IGenerateEncryptedHash>();
            builder.RegisterType<RijndaleEncryption>().As<IEncrypt>();
            builder.RegisterType<Base64Converter>().As<IBytearrayToTextConverter>();

            GlobalVariables.Container = builder.Build();
            return true;
        }
    }
}
