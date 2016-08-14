using System;

namespace Services
{
    /// <summary>
    /// Базовый класс для класса Folder и File
    /// </summary>
    public class Node
    {
        string m_name;
        
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                value = value.Trim();
                //проверка длины имени и проверка на содержание запрещенных симолов в имени
                if (value.Length == 0 || value.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) != -1)
                    throw new ArgumentException();
                m_name = value;
            }
        }

        protected Node(string name)
        {
            Name = name;
        }


    }
}
