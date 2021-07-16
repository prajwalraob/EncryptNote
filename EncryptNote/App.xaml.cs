using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using EncryptNote.Views;
using EncryptNote.Models;
using Autofac;
using System.Xml.Serialization;

namespace EncryptNote
{
    public partial class App : System.Windows.Application
    {
        private void StartApplication(object sender, StartupEventArgs e)
        {
            AutofacConfig.Config();

            using (var scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                Type T = scope.Resolve<INoteItemModel>().GetType();
                var notes = Directory.GetFiles(GlobalVariables.NotesDirectory, "*.noteinfo");

                foreach (var item in notes)
                {                    
                    using(StreamReader streamReader = new StreamReader(item))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(T);
                        var noteItem = xmlSerializer.Deserialize(streamReader) ;
                        if (noteItem != null)
                        {
                            GlobalVariables.NotesList.Add((noteItem as INoteItemModel));
                        }
                    }

                    //string jsonString = File.ReadAllText(item);
                    //var noteItem = JsonConvert.DeserializeObject(jsonString, T);
                    //if (noteItem != null)
                    //{
                    //    GlobalVariables.NotesList.Add((noteItem as INoteItemModel));
                    //}
                }
            }

            MainWindow wnd = new MainWindow();
            wnd.ShowDialog();
        }
    }

}
