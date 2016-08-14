using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Содержит методы виртуальной файловой системы
    /// </summary>
    public interface IVirtualFileSystem
    {
        /// <summary>
        /// Создание файла
        /// </summary>
        /// <param name="path">Путь до нового файла</param>
        void CreateFile(string path);

        /// <summary>
        /// Создание папки
        /// </summary>
        /// <param name="path">Путь до новой папки</param>
        void CreateFolder(string path);

        /// <summary>
        /// Копирование узла
        /// </summary>
        /// <param name="src">Путь копируемого файла</param>
        /// <param name="dst">Путь конечного файла</param>
        void Copy(string src, string dst);

        /// <summary>
        /// Перемещение узла
        /// </summary>
        /// <param name="src">Начальный путь узла</param>
        /// <param name="dst">Конечный путь узла</param>
        void Move(string src, string dst);

        /// <summary>
        /// Удаление узла
        /// </summary>
        /// <param name="path">Путь удаляемого узла</param>
        void Delete(string path);

        /// <summary>
        /// Получение дерева котолога
        /// </summary>
        /// <param name="path">Заданный путь</param>
        /// <returns>Узел</returns>
        Node GetTree(string path);
    }
}
