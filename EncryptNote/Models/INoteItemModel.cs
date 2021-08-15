using System;

namespace EncryptNote.Models
{
    interface INoteItemModel
    {
        string UniqueID { get; set; }
        DateTime Created { get; set; }
        string DisplayName { get; set; }
        DateTime LastUpdated { get; set; }
        string TagLine { get; set; }
    }
}