using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EncryptNote.ViewModels;

namespace EncryptNote.Models
{
    public class NoteItemModel : NotifyPropertyChanged, INoteItemModel
    {
        private string _uniqueID;
        public string UniqueID
        {
            get => _uniqueID;
            set
            {
                _uniqueID = value;
                OnPropertyChanged();
            }
        }

        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set
            {
                _displayName = value;
                OnPropertyChanged();
            }
        }

        private DateTime _lastUpdated;
        public DateTime LastUpdated
        {
            get => _lastUpdated;
            set
            {
                _lastUpdated = value;
                OnPropertyChanged();
            }
        }

        private DateTime _created;
        public DateTime Created
        {
            get => _created;
            set
            {
                _created = value;
                OnPropertyChanged();
            }
        }

        private string _tagLine;

        public string TagLine
        {
            get => _tagLine;
            set
            {
                _tagLine = value;
                OnPropertyChanged();
            }
        }


    }
}
