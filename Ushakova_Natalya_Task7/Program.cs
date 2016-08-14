using System;
using Services;
using ServicesImpl;
using System.IO;
using System.Diagnostics;


namespace Ushakova_Natalya_Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            //трассировочное логирование методов
            Trace.Listeners.Add(new TextWriterTraceListener("info.txt"));

            //логирование исключительных ситуаций
            Log logger = new Log("exc.txt");
            
            IVirtualFileSystem vfs = new VFS();

            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Что сделать?");
                Console.WriteLine("1.Создать папку \n2.Создать файл \n3.Копировать \n4.Переместить");
                Console.WriteLine("5.Удалить \n6.Вывести дерево\n7.Получить имя\n8.Получить путь");
                try
                {
                    int n;
                    while (true)
                    {
                        //проверка вводимого числа
                        if (Int32.TryParse(Console.ReadLine(), out n) && n >= 1 && n <= 8)
                            break;
                        Console.WriteLine("Введите число от 1 до 8");
                    }
                    string path, src, dst;
                    switch (n)
                    {
                        case 1:
                            Console.WriteLine("Введите путь до папки");
                            path = Console.ReadLine();
                            vfs.CreateFolder(path);
                            // myTextListener.WriteLine(" Метод:" + DateTime.Now + "CreateFolder - Создание папки с путем" + path);


                            break;
                        case 2:
                            Console.WriteLine("Введите путь до файла");
                            path = Console.ReadLine();
                            vfs.CreateFile(path);
                            //  myTextListener.Write("Info Метод:" + DateTime.Now + "CreateFile - Создание файла с путем" + path);
                            break;
                        case 3:
                            Console.WriteLine("Введите путь до копируемого файла/папки");
                            src = Console.ReadLine();
                            Console.WriteLine("Введите путь нового файла");
                            dst = Console.ReadLine();
                            vfs.Copy(src, dst);
                            break;
                        case 4:
                            Console.WriteLine("Введите путь до файла/папки");
                            src = Console.ReadLine();
                            Console.WriteLine("Введите путь куда переместить (включая имя файла/папки)");
                            dst = Console.ReadLine();
                            vfs.Move(src, dst);
                            break;
                        case 5:
                            Console.WriteLine("Введите путь до файла/папки");
                            path = Console.ReadLine();
                            vfs.Delete(path);
                            break;
                        case 6:
                            Console.WriteLine("Введите путь до файла/папки");
                            path = Console.ReadLine();
                            Print(vfs.GetTree(path));
                            break;
                        default:
                            Console.WriteLine("Введено некорректное число");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.add(ex.Message);
                }
                Console.WriteLine("Повторить? (y/n)");
                string chr = Console.ReadLine();
                if (chr == "n")
                {
                    flag = false;
                }
                // Console.WriteLine();

            }
            // Flush the output.
            Trace.Flush();
            logger.save();

            Console.ReadKey();
        }

        /// <summary>
        /// Вывод дерева каталогов для заданного узла
        /// </summary>
        /// <param name="n">Узел</param>
        /// <param name="offset">Смещение</param>
        static void Print(Node n, int offset = 0)
        {
            for (int i = 0; i < offset; ++i)
                Console.Write(' ');
            Console.WriteLine(n.Name);
            Folder f = n as Folder;
            if (f != null)
                foreach (Node child in f.Children)
                    Print(child, offset + 2);
        }

    }
}
