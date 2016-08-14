

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using System.Diagnostics;


namespace ServicesImpl
{
    public class VFS : IVirtualFileSystem
    {
        //корневая папка
        Folder m_root = new Folder("root");

        //Разделитель для строки пути
        public static char Separator = '/';

        //получить путь до узла
        public string GetPath(string path)
        {
            //поиск последнего вхождения '/' для разделения на путь и имя файла для источника
            int index = path.LastIndexOf(Separator);
            if (index == -1)
            {
                //если ни один '/' не найден
                //logExcep.add("Неверно заданный путь!", "GetPath", "INFO");
                throw new ArgumentException("Неверно заданный путь!");
            }
            //путь до папки
            path = path.Substring(0, index);
            //если только один '/' в исходном пути
            if (path.Length == 0)
                path = "/";
            return path;
        }

        //получить имя узла
        public string GetName(string path)
        {
            //поиск последнего вхождения '/' для разделения на путь и имя файла для источника
            int index = path.LastIndexOf(Separator);
            if (index == -1)
                //если ни один '/' не найден
                throw new ArgumentException("Неверно заданный путь");
            //имя папки
            string name = path.Substring(index + 1);
            return name;
        }

        /// <summary>
        /// Копирование узла
        /// </summary>
        /// <param name="src">Путь копируемого файла</param>
        /// <param name="dst">Путь конечного файла</param>
        public void Copy(string src, string dst)
        {
            Trace.TraceInformation(DateTime.Now + " Метод: " + nameof(Copy) + " Путь копируемого файла " + src + " Путь конечного файла " + dst);
            Node n = GetTree(src);
            if (n is Folder)
            {
                CreateFolder(dst);
                Node newNode = GetTree(dst);
                Folder oldFolder = (Folder)n;
                Folder newFolder = (Folder)newNode;
                newFolder.Children = oldFolder.Children;
            }
            else
            {
                CreateFile(dst);
                Node newNode = GetTree(dst);
                File oldFile = (File)n;
                File newFile = (File)newNode;
                newFile.Data = oldFile.Data;
            }
        }

        /// <summary>
        /// Создание файла
        /// </summary>
        /// <param name="path">Путь до нового файла</param>
        public void CreateFile(string path)
        {
            Trace.TraceInformation(DateTime.Now + " Метод: " + nameof(CreateFile) + " создание файла с путем " + path);
            //путь до родительской папки
            string parentPath = GetPath(path);
            //имя создаваемого файла
            string name = GetName(path);
            //получить узел 
            Node n = GetTree(parentPath);
            Folder f = n as Folder;
            if (f == null)
                throw new ArgumentException("Неверно задан путь!(Невозможно создать файл в файле)");
            //выбор первого потомка с заданным именем для данной папки
            if (f.Children.FirstOrDefault((Node node) => { return node.Name == name; }) != null)
                throw new ArgumentException("Файл уже существует!");
            f.Children.Add(new File(name));
        }

        /// <summary>
        /// Создание папки
        /// </summary>
        /// <param name="path">Путь до новой папки</param>
        public void CreateFolder(string path)
        {
            Trace.TraceInformation(DateTime.Now + " Метод: " + nameof(CreateFolder) + " создание папки с путем " + path);
            //путь до родительской папки
            string parentPath = GetPath(path);
            //имя создаваемой папки
            string name = GetName(path);
            //получить узел 
            Node n = GetTree(parentPath);
            Folder f = n as Folder;
            if (f == null)
                throw new ArgumentException("Неверно задан путь!(Невозможно создать папку в файле)");
            //выбор первого потомка с заданным именем для данной папки
            if (f.Children.FirstOrDefault((Node node) => { return node.Name == name; }) != null)
                throw new ArgumentException("Папка уже существует!");
            f.Children.Add(new Folder(name));
        }

        /// <summary>
        /// Удаление узла
        /// </summary>
        /// <param name="path">Путь удаляемого узла</param>
        public void Delete(string path)
        {
            Trace.TraceInformation(DateTime.Now + " Метод: " + nameof(Delete) + " удаление узла с путем " + path);
            //путь до родительской папки
            string parentPath = GetPath(path);
            //имя удаляемого узла
            string nameNode = GetName(path);
            //родительская папка
            Node parent = GetTree(parentPath);
            Folder f = parent as Folder;
            if (f == null)
                throw new ArgumentException("Неверно задан путь!");
            if (f.Children.FirstOrDefault((Node node) => { return node.Name == nameNode; }) == null)
                throw new ArgumentException("Папка/файл не существует!");
            for (int i = 0; i < f.Children.Count; i++)
            {
                if (f.Children[i].Name == nameNode)
                {
                    f.Children.RemoveAt(i);
                }
            }
            //f.Children.RemoveAll(x => x.Name == nameNode)
        }

        /// <summary>
        /// Получение дерева котологов
        /// </summary>
        /// <param name="path">Заданный путь</param>
        /// <returns>Узел</returns>
        public Node GetTree(string path)
        {
            Trace.TraceInformation(DateTime.Now + " Метод: " + nameof(GetTree) + " получение дерева катологов по пути " + path);
            //если путь пустой или не начинается с '/'
            if (path.Length == 0 || path[0] != Separator)
                throw new ArgumentException("Неверно заданный путь!");
            if (path == "/")
                return m_root;
            //имена всех папок в пути
            string[] names = path.Split(Separator);
            //начиная с корневой папки
            Node n = m_root;
            //проход по всем именам в пути
            for (int i = 1; i < names.Length; ++i)
            {
                string name = names[i];
                //узел n должен быть папкой
                Folder f = n as Folder;
                if (f == null)
                    throw new ArgumentException("Неверно заданный путь!");
                //null если не найдет 
                //n - первый потомок в пути после n текущей
                n = f.Children.FirstOrDefault((Node node) => { return node.Name == name; });
            }
            if (n == null)
                throw new ArgumentException("Неверно заданный путь!");
            return n;
        }

        /// <summary>
        /// Перемещение узла
        /// </summary>
        /// <param name="src">Начальный путь узла</param>
        /// <param name="dst">Конечный путь узла</param>
        public void Move(string src, string dst)
        {
            Trace.TraceInformation(DateTime.Now + " Метод: " + nameof(Move) + " начальный путь узла " + src + " конечный путь узла " + dst);
            Copy(src, dst);
            Delete(src);
        }
    }
}
