using Notebook.Domain;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Notes.Application.Interfaces
{
    /// <summary>
    /// реализуется в notesPersistance
    /// 
    /// </summary>
    public interface INotesDBContext
    {
        /// <summary>
        /// набор сущностей
        /// </summary>
        DbSet<Note> Notes { get; set; }

        /// <summary>
        /// сохраняет изменения контекста в БД
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
