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

        //public NoteItemModel(string uniqueID, string displayName, DateTime lastUpdated, DateTime created)
        //{
        //    this.UniqueID = uniqueID;
        //    this.DisplayName = displayName;
        //    this.LastUpdated = lastUpdated;
        //    this.Created = created;
        //}
    }
}
