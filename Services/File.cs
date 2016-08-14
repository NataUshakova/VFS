
namespace Services
{
    /// <summary>
    /// Класс файл
    /// </summary>
    public class File : Node
    {
        public File(string name) : base(name) { }
        public string Data { get; set; } 
    }
}
