using System;

namespace Notes.Application.Common.Exceptions
{
    /// <summary>
    /// Исключение  "не найдено"
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            :base ($"Entity \"{name}\" ({key}) not found.") { }
    }
}
