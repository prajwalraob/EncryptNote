using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EncryptNote.Models;
using Autofac;
using EncryptNote.Views;
using System.Windows.Documents;
using System.Xml;
using System.Windows.Markup;

namespace EncryptNote.ViewModels
{
    class MainWindowViewModel : NotifyPropertyChanged
    {
        public Command MenuInfoClickedCommand { get; set; }
        public Command AddClickedCommand { get; set; }
        public Command DeleteClickedCommand { get; set; }
        public Command SelectionChangedCommand { get; set; }
        public Command ListBoxClickedCommand { get; set; }


        private ObservableCollection<INoteItemModel> _notesDisplay;
        public ObservableCollection<INoteItemModel> NotesDisplay
        {
            get => _notesDisplay;
            set
            {
                _notesDisplay = value;
                OnPropertyChanged();
            }
        }

        private INoteItemModel _lastSelectedNote;
        public INoteItemModel LastSelectedNote
        {
            get => _lastSelectedNote;
            set
            {
                _lastSelectedNote = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            MenuInfoClickedCommand = new Command(MenuInfoClicked);
            AddClickedCommand = new Command(AddClicked);
            DeleteClickedCommand = new Command(DeleteClicked);
            SelectionChangedCommand = new Command(SelectionChanged);
            ListBoxClickedCommand = new Command(ListBoxClicked);
            NotesDisplay = GlobalVariables.GetNotesFromFolder();
        }

        public void MenuInfoClicked(object wnd)
        {

        }
        public void ListBoxClicked(object wnd)
        {
            MainWindow mWnd = wnd as MainWindow;
            INoteItemModel noteInfo = mWnd.notesListBox.SelectedItem as INoteItemModel;
        }

        public void SelectionChanged(object wnd)
        {
            using (var scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                MainWindow mWnd = wnd as MainWindow;
                INotesAction notesAction = scope.Resolve<INotesAction>();

                if (LastSelectedNote != null)
                {
                    XmlDocument noteDocument = FlowDocumentConverter.Convert(mWnd.richTextBox.Document);
                    notesAction.UpdateNote(LastSelectedNote, noteDocument);
                }

                INoteItemModel noteInfo = mWnd.notesListBox.SelectedItem as INoteItemModel;
                FlowDocument displayNoteDocument = FlowDocumentConverter.ConvertBack(notesAction.ReadNote(noteInfo) as XmlDocument);

                if(displayNoteDocument != null) mWnd.richTextBox.Document = displayNoteDocument;

                LastSelectedNote = noteInfo;
            }
        }

        public void AddClicked(object wnd)
        {
            using (var scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                INotesAction notesAction = scope.Resolve<INotesAction>();
                INoteItemModel noteItem = notesAction.CreateNote();
                NotesDisplay.Add(noteItem);
            }
        }

        public void DeleteClicked(object wnd)
        {
            using (var scope = GlobalVariables.Container.BeginLifetimeScope())
            {
                var items = (wnd as MainWindow).notesListBox.SelectedItems;
                INotesAction notesAction = scope.Resolve<INotesAction>();
                var noteItem = notesAction.DeleteNotes(items.Cast<INoteItemModel>().ToList(), NotesDisplay);
                NotesDisplay = noteItem as ObservableCollection<INoteItemModel>;
            }
        }

        public bool DeleteEnabled(object wnd)
        {
            return false;
        }

    }
}
