using System.Collections.Generic;

namespace Services
{
    /// <summary>
    /// Класс папка
    /// </summary>
    public class Folder : Node
    {
        /// <summary>
        /// список потомков данной папки
        /// </summary>
        public IList<Node> Children { get; set; }

        public Folder(string name) : base(name)
        {
            Children = new List<Node>();
        }
    }
}
