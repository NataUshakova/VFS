using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImpl
{
    /// <summary>
    /// Логгер для исключительных ситуаций
    /// </summary>
    public class Log
    {
        StreamWriter m_sw;

        public Log(string filePath)
        {
            m_sw = new StreamWriter(filePath, true);
        }

        public void add(string text)
        {
            m_sw.WriteLine("{0}: {1}", DateTime.Now, text);
        }

        public void save()
        {
            m_sw.Close();
        }
    }
}
