using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Persistance
{
    public class DbInitializer
    {
        /// <summary>
        /// Проверяет, создана ли БД. Если нет, то она будет создана на основе контекста
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(NotesDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
