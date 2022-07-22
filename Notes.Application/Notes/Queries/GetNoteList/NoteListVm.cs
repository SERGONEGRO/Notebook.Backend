using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    /// <summary>
    /// список заметок
    /// </summary>
    public class NoteListVm
    {
        public IList<NoteLookupDto> Notes { get; set; }
    }
}
